using System.Reflection;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;

namespace MmorpgClassLibrary;

internal static class DataLayer {
    private static ISessionFactory? _factory;
    private static readonly object LockObj = new();

    public static ISession? GetSession() {
        if (_factory == null) {
            lock (LockObj) {
                if (_factory == null) {
                    _factory = CreateSessionFactory();
                }
            }
        }
        return _factory?.OpenSession();
    }

    private static ISessionFactory? CreateSessionFactory()
    {
        try {
            var cfg = OracleManagedDataClientConfiguration.Oracle10
                .ConnectionString(c =>
                    c.Is("Data Source=gislab-oracle.elfak.ni.ac.rs:1521/SBP_PDB;User Id=S18630;Password=S18630"));

            return Fluently.Configure()
                .Database(cfg)
                .Mappings(m => m.FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly()))
                .BuildSessionFactory();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }
}