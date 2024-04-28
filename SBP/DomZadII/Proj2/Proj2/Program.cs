using NHibernate;
using Proj2.Entiteti;

namespace Proj2;

public static class Program {
    public static void Main(string[] args) {
        try {
            ISession? s = DataLayer.GetSession();
            if (s == null) {
                Console.WriteLine("Session dud");
                return;
            }

            var tim = new Tim() {
                MinIgraca = 5,
                MaxIgraca = 10,
                Naziv = "MocniMomci",
                Plasman = 2
            };
            s.Save(tim);

            s.Flush();
            s.Close();
        }
        catch (Exception ec) {
            Console.WriteLine(ec.FormatExceptionMessage());
        }
    }
}

