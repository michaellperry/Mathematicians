using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Mathematicians.Domain
{
    public class MathematicianName
    {
        internal MathematicianName(IEnumerable<MathematicianName> priorNames, string firstName, string lastName)
        {
            Prior = priorNames.ToList();
            FirstName = firstName;
            LastName = lastName;
            HashCode = FirstName.Sha256Hash()
                .Concatenate(LastName.Sha256Hash())
                .Concatenate(Prior.Select(x => x.HashCode).ToArray());
        }

        public int MathematicianNameId { get; private set; }

        public int MathematicianId { get; private set; }
        public virtual Mathematician Mathematician { get; private set; }

        public BigInteger HashCode { get; private set; }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public virtual ICollection<MathematicianName> Prior { get; }
        public virtual ICollection<MathematicianName> Next { get; } =
            new List<MathematicianName>();
    }
}