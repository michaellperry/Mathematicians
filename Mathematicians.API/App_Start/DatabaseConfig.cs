using MergableMigrations.EF6;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
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
            var migrator = new DatabaseMigrator(
                databaseName,
                fileName,
                master.ConnectionString,
                new Migrations());
            migrator.RollbackDatabase();
            migrator.MigrateDatabase();
        }
    }
}