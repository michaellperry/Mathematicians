using MergableMigrations.Specification;

namespace Mathematicians.API
{
    public class Migrations : IMigrations
    {
        public void AddMigrations(DatabaseSpecification db)
        {
            var dbo = db.UseSchema("dbo");

            CreateMathematician(db, dbo);
        }

        private void CreateMathematician(DatabaseSpecification db, SchemaSpecification dbo)
        {
            var table = dbo.CreateTable("Mathematician");

            var id = table.CreateIdentityColumn("MathematicianId");
            var unique = table.CreateGuidColumn("Unique");

            table.CreatePrimaryKey(id);

            var generateIds = db
                .After(table, unique)
                .Execute(@"
                    UPDATE dbo.Mathematician
                    SET [Unique] = NEWID()
                    WHERE [Unique] = '00000000-0000-0000-0000-000000000000'");

            table
                .After(generateIds)
                .CreateUniqueIndex(unique);
        }
    }
}
