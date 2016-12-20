# PostDeploy.ps1
#
$DatabaseServer = $OctopusParameters["DatabaseServer"]
$DatabaseName = $OctopusParameters["DatabaseName"]
$DatabaseUsername = $OctopusParameters["DatabaseUsername"]
$DatabasePassword = $OctopusParameters["DatabasePassword"]
$DropDatabase = $OctopusParameters["DropDatabase"]
$MigrationSilent = $OctopusParameters["MigrationSilent"]
$PackageName = $OctopusParameters["Octopus.Action.Package.NuGetpackageId"]
$Migrator = ".\Database.exe"
$targetEnv = $OctopusParameters["targetEnv"]

$connection_string = "Server=tcp:$DatabaseServer;Database=$DatabaseName;Persist Security Info=False;User ID=$DatabaseUsername@$DatabaseServer;Password=$DatabasePassword;Pooling=False;MultipleActiveResultSets=True;Encrypt=True;TrustServerCertificate=False;";

Write-Host "Using connection string: $connection_string";

if ($DropDatabase -eq "True") {
    Write-Host "Dropping database $DatabaseName" -BackgroundColor Red
    #note the database is Master
    $adminConnectionString = "Server=tcp:$DatabaseServer;Database=Master;Persist Security Info=False;User ID=$DatabaseUsername@$DatabaseServer;Password=$DatabasePassword;Pooling=False;MultipleActiveResultSets=True;Encrypt=True;TrustServerCertificate=False;"
    $sqlConnection = New-Object System.Data.SqlClient.SqlConnection;
    $sqlConnection.ConnectionString = $adminConnectionString;
    $sqlConnection.Open();
    $sqlCommand = New-Object System.Data.SqlClient.SqlCommand;
    $sqlCommand.Connection = $sqlConnection;
    $sqlCommand.CommandText = "IF EXISTS(select * from sys.databases where name='$DatabaseName') DROP DATABASE $DatabaseName";
    $sqlCommand.ExecuteNonquery();
    $sqlConnection.Close();
}

& $Migrator --vf=Database.dll /d $DatabaseName -c="$connection_string" --silent=True --files=.\scripts\db --wt --env=$targetEnv;
