using System;
using System.Configuration;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using SBP2.Models.Mapiranja;

namespace SBP2;

public static class DataLayer {
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
        try
        {
            string cs = ConfigurationManager.ConnectionStrings["OracleCS"].ConnectionString;
            var cfg = OracleManagedDataClientConfiguration.Oracle10
                .ShowSql()
                .ConnectionString(c => c.Is(cs));

            return Fluently.Configure()
                .Database(cfg)
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<IgracMapiranja>())
                .BuildSessionFactory();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

}