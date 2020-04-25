using System;
using System.Data;
using System.Reflection;
using DbUp;
using WebAnalytics.Abstraction;

namespace WebAnalytics.Store.Postgres
{
    public class PostgresInitializer : IInitializer
    {
        private readonly IDbConnection _connection;

        public PostgresInitializer(IDbConnection connection)
        {
            _connection = connection;
        }

        public void EnsureInitialized()
        {
            var migrator = DeployChanges.To
                                        .PostgresqlDatabase(_connection.ConnectionString)
                                        .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                                        .LogScriptOutput().Build();

            var result = migrator.PerformUpgrade();

            if (!result.Successful)
            {
                throw new Exception($"Failed to initialize database! Error: {result.Error}");
            }
        }
    }
}