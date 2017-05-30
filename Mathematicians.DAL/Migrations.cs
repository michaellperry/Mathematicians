using MergableMigrations.Specification;

namespace Mathematicians.API
{
    public class Migrations : IMigrations
    {
        public void AddMigrations(DatabaseSpecification db)
        {
            var dbo = db.UseSchema("dbo");

            CreateMathematician(dbo);
        }

        private void CreateMathematician(SchemaSpecification dbo)
        {
            var table = dbo.CreateTable("Mathematician");

            var id = table.CreateIdentityColumn("MathematicianId");
            var unique = table.CreateGuidColumn("Unique");

            table.CreatePrimaryKey(id);
            table.CreateUniqueIndex(unique);
        }
    }
}
