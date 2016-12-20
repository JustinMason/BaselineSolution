using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Domain.Models;
using DbUp.Engine;
using Infrastructure;
using Infrastructure.DataAccess;
using Infrastructure.DataAccess.DataConnection;
using Newtonsoft.Json;

namespace Database.DatabaseName.db.upCodedScripts
{
    public class Script201612081315_JsonUpgradeDocmentStructure : IScript
    {
        public string ProvideScript(Func<IDbCommand> dbCommandFactory)
        {
            var command = dbCommandFactory();


            return "";
        }

        private IEnumerable<dynamic> GetExisingJsonBlobs(IDbCommand command)
        {
            List<dynamic> result = new List<dynamic>();

            var sql = "SELECT [Data] From [dbo].[RatingContracts]";

            command.CommandText = sql;
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    try
                    {
                        result.Add( SerlaizationStradegy.DeserializeObject<dynamic>(reader.GetString(reader.GetOrdinal("Data"))));
                    }
                    catch (JsonReaderException)
                    {
                      //Not Json      
                    }
                    

                }
            }

            return result;
        }

        public static string SqlConnectionToConnectionString(IDbConnection conn)
        {
            System.Reflection.PropertyInfo property = conn.GetType().GetProperty("ConnectionOptions", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            object optionsObject = property.GetValue(conn, null);
            System.Reflection.MethodInfo method = optionsObject.GetType().GetMethod("UsersConnectionString");
            string connStr = method.Invoke(optionsObject, new object[] { false }) as string; // argument is "hidePassword" so we set it to false
            return connStr;
        }

    }
}
