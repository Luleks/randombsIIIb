using NHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Proj2.Mapiranja;

namespace Proj2;

public static class DataLayer {
    private static ISessionFactory? _factory;
    private static readonly object LockObj = new();

    private const string ConnectionString =
        "Data Source=gislab-oracle.elfak.ni.ac.rs:1521/SBP_PDB;User Id=S18630;Password=S18630";

    public static ISession GetSession() {
        if (_factory != null) return _factory!.OpenSession();
        
        lock (LockObj) {
            _factory ??= CreateSessionFactory();
        }

        return _factory!.OpenSession();
    }

    private static ISessionFactory? CreateSessionFactory() {
        try {
            var cfg = OracleManagedDataClientConfiguration.Oracle10
                .ShowSql()
                .ConnectionString(c => c.Is(ConnectionString));

            return Fluently.Configure().Database(cfg).Mappings(m => m.FluentMappings.AddFromAssemblyOf<RasaMapiranja>())
                .BuildSessionFactory();
        }
        catch (Exception ec) {
            return null;
        }
    }

}