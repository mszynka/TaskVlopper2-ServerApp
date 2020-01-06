using System;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace ServerApp.Infrastructure.Persistence
{
    public class ServerAppAutomappingConfiguration : DefaultAutomappingConfiguration
    {
        public override bool ShouldMap(Type type)
        {
            return type?.Namespace?.EndsWith(".Records") == true
                   && type?.Name?.EndsWith("Record") == true;
        }
    }

    public class SessionFactoryBuilder
    {
        public static ISessionFactory BuildSessionFactory(bool create = false, bool update = false)
        {
            return Fluently.Configure()
                .Database(SQLiteConfiguration.Standard.UsingFile("ServerApp.db").ShowSql())
                .Mappings(m =>
                {
                    m.FluentMappings.Conventions.Add(new DateTimeOffsetTypeConvention());
                    m.FluentMappings.AddFromAssemblyOf<NHibernate.Cfg.Mappings>();
                })
                .CurrentSessionContext("call")
                .ExposeConfiguration(cfg => BuildSchema(cfg, create, update))
                .BuildSessionFactory();
        }

        public static ISessionFactory BuildTestSessionFactory<TRecord>()
        {
            return Fluently.Configure()
                .Database(SQLiteConfiguration.Standard.UsingFile("ServerApp.Test.db").ShowSql())
                .Mappings(m =>
                {
                    m.AutoMappings.Add(
                        AutoMap
                            .AssemblyOf<TRecord>(new ServerAppAutomappingConfiguration())
                            .Conventions.Add(new DateTimeOffsetTypeConvention())
                    );
                })
                //.CurrentSessionContext("call")
                .ExposeConfiguration(cfg => BuildSchema(cfg, true, true))
                .BuildSessionFactory();
        }

        public static void BuildSchema(Configuration config, bool create = false, bool update = false)
        {
            if (create)
                new SchemaExport(config).Create(true, true);

            if (update)
                new SchemaUpdate(config).Execute(true, update);
        }
    }
}