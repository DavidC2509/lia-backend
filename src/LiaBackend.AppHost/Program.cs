var builder = DistributedApplication.CreateBuilder(args);

#region Postbres Db

var serverPotgsres = builder.ExecutionContext.IsRunMode ? builder.AddPostgres("lia-database-server",
    password: builder.AddParameter("PostgresPassword", secret: true)).WithDataVolume().WithPgAdmin(c => c.WithHostPort(5050)) : null;

var postgresDbNext = builder.ExecutionContext.IsRunMode ? serverPotgsres
    .AddDatabase("lia-database") : builder.AddConnectionString("lia-database");

#endregion

//builder.AddProject<DatabaseMigration>("lia-database-migration")
//    .WithReference(postgresDbNext).WaitFor(postgresDbNext);

builder.AddProject<Projects.Api>("api-lia");

builder.Build().Run();
