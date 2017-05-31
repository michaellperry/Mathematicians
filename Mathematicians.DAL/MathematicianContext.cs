using Mathematicians.Domain;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Mathematicians.DAL
{
    public class MathematicianContext : DbContext
    {
        public MathematicianContext(string nameOrConnectionString) :
            base(nameOrConnectionString)
        {
        }

        public DbSet<Mathematician> Mathematicians { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
