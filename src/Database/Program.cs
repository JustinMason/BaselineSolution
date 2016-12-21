using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using DbUp;

namespace Database
{
    public class Program
    {
        public static int Main(string[] args)
        {
            var connectionString = GetConnectionString(args);

            EnsureDatabase.For.SqlDatabase(connectionString, 120);

            if (!DropDatabase(args, connectionString))
            {

                var upgradeEngine = DeployChanges.To
                    .SqlDatabase(connectionString)
                    .WithScriptsAndCodeEmbeddedInAssembly(Assembly.GetExecutingAssembly(),
                        (s) => GetEnvironmentFilter(s, args, connectionString))
                    .LogToConsole()
                    .Build();
                

                var result = upgradeEngine.PerformUpgrade();

                if (!result.Successful)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(result.Error);
                    Console.ResetColor();
#if DEBUG
                    Console.ReadLine();
#endif
                    return -1;
                }

            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Success!");
            Console.ResetColor();

            return 0;
        }

        private static string GetConnectionString(string[] args)
        {
            var connString = args.FirstOrDefault((s) => s.IndexOf("-c", StringComparison.InvariantCultureIgnoreCase) >= 0);

            if (connString == null)
            {
                connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            }
            else
            {
                var i = connString.IndexOf("=", StringComparison.Ordinal) + 1;
                if (i > -1)
                {
                    connString = connString.Substring(i);
                }
            }

            return connString;
        }

        private static bool DropDatabase(string[] args, string connectionString)
        {
            var drop = args.FirstOrDefault((s) => s.IndexOf("-drop", StringComparison.InvariantCultureIgnoreCase) >= 0);

            if (drop != null)
            {
                var databaseIndex = System.Array.FindIndex(args, x => x == "-d");
               
                if (databaseIndex > -1)
                {
                    var databaseName = args[databaseIndex +1] ;
                    string dropSql = $"DROP DATABASE [{databaseName}]";

                    var masterconnectionString = connectionString.Replace(databaseName,"master");
                    using (var conn = new SqlConnection(masterconnectionString))
                    {
                        conn.Open();

                        using (var command = new SqlCommand(dropSql, conn))
                            command.ExecuteNonQuery();
                     
                        return true;
                    }
                }
            }

            return false;
        }

        private static bool GetEnvironmentFilter(string arg, string[] args, string connectionString)
        {
            var result = !WasExecutedByRoundHouse(arg, connectionString) && arg.StartsWith("Database.ServiceGroupPortal.db.up."); //Enforce Up Directory

            if (result)
            {
                var env = args.FirstOrDefault((s) => s.IndexOf("ENV", StringComparison.InvariantCultureIgnoreCase) >= 0);
                if (env != null)
                {
                    var i = env.IndexOf("=", StringComparison.Ordinal) + 1;
                    if (i > -1)
                    {
                        var value = env.Substring(i);
                        result = arg.StartsWith($"{value}.") || arg.Contains($".{value}.") || !arg.Contains(".ENV.");
                    }
                }
            }

            return result;
        }

        private static bool WasExecutedByRoundHouse(string scriptName, string connectionString)
        {
            var result = false;
            var sql =
                $@"
IF (EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'RoundhousE' 
                 AND  TABLE_NAME = 'ScriptsRun'))
BEGIN

SELECT [id]
                          FROM [RoundhousE].[ScriptsRun]
                          where script_name like '%' + REPLACE ('{scriptName}','Database.ServiceGroupPortal.db.up.','') + '%'
                          and one_time_script = 1
END
                        ";

            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();

                using (var command = new SqlCommand(sql, conn))
                {
                    var reader = command.ExecuteReader();
                    result = reader.HasRows;
                }
            }

            return result;
        }
    }
}
