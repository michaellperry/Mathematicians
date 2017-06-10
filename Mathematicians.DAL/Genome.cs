using Schemavolution.Specification;
using System;

namespace Mathematicians.API
{
    public class Genome : IGenome
    {
        public void AddGenes(DatabaseSpecification db)
        {
            var dbo = db.UseSchema("dbo");

            CreateMathematician(dbo);
        }

        private void CreateMathematician(SchemaSpecification dbo)
        {
            var table = dbo.CreateTable("Mathematician");

            var id = table.CreateIdentityColumn("MathematicianId");
            var unique = table.CreateGuidColumn("Unique");

            var pk = table.CreatePrimaryKey(id);
            table.CreateUniqueIndex(unique);

            CreateMathematicianName(dbo, pk);
        }

        private void CreateMathematicianName(SchemaSpecification dbo, PrimaryKeySpecification mathematicianPk)
        {
            var table = dbo.CreateTable("MathematicianName");

            var id = table.CreateIdentityColumn("MathematicianNameId");
            var mathematicianId = table.CreateIntColumn("MathematicianId");
            var firstName = table.CreateStringColumn("FirstName", 50);
            var lastName = table.CreateStringColumn("LastName", 50);

            var index = table.CreateIndex(mathematicianId);
            var fk = index.CreateForeignKey(mathematicianPk);

            var pk = table.CreatePrimaryKey(id);

            CreateMathematicianNamePrior(dbo, pk);
        }

        private static void CreateMathematicianNamePrior(SchemaSpecification dbo, PrimaryKeySpecification mathematicianPk)
        {
            var table = dbo.CreateTable("MathematicianNamePrior");

            var currentId = table.CreateIntColumn("MathematicianNameId");
            var priorId = table.CreateIntColumn("PriorMathematicianNameId");

            var currentIndex = table.CreateIndex(currentId);
            var currentFk = currentIndex.CreateForeignKey(mathematicianPk);

            var priorIndex = table.CreateIndex(priorId);
            var priorFk = priorIndex.CreateForeignKey(mathematicianPk);

            var pk = table.CreatePrimaryKey(currentId, priorId);
        }
    }
}
