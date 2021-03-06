using System;
using System.Data;
using System.Reflection;
using System.Threading.Tasks;
using Dapper;
using DbUp;
using WebAnalytics.Abstraction;
using WebAnalytics.Core.Entities;
using WebAnalytics.Core.ValueTypes;

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
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
            SqlMapper.AddTypeHandler(typeof(DeviceInfo), new JsonTypeHandler());
            SqlMapper.AddTypeHandler(typeof(FrameSet), new JsonTypeHandler());
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