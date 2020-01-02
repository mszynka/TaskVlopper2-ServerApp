using FluentNHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;

using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace ServerApp.Infrastructure.Persistence
{
    public class SessionFactoryBuilder
    {
        public static ISessionFactory BuildSessionFactory(bool create = false, bool update = false)
        {
            return Fluently.Configure()
                .Database(SQLiteConfiguration.Standard.InMemory().ShowSql())
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<NHibernate.Cfg.Mappings>())
                .CurrentSessionContext("call")
                .ExposeConfiguration(cfg => BuildSchema(cfg, create, update))
                .BuildSessionFactory();
        }

        public static void BuildSchema(Configuration config, bool create = false, bool update = false)
        {
            if (create) new SchemaExport(config).Create(false, true);
            else new SchemaUpdate(config).Execute(false, update);
        }
    }
}
