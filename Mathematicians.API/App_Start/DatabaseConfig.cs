using Schemavolution.EF6;
using System.Data.SqlClient;
using System.Web;

namespace Mathematicians.API
{
    public static class DatabaseConfig
    {
        public static void Configure(HttpServerUtility server)
        {
            var master = new SqlConnectionStringBuilder
            {
                DataSource = @"(LocalDB)\MSSQLLocalDB",
                InitialCatalog = "master",
                IntegratedSecurity = true
            };

            string fileName = server.MapPath("~/App_Data/Mathematicians.mdf");
            string databaseName = "Mathematicians";
            var evolver = new DatabaseEvolver(
                databaseName,
                fileName,
                master.ConnectionString,
                new Genome());
            evolver.DestroyDatabase();
            evolver.EvolveDatabase();
        }
    }
}