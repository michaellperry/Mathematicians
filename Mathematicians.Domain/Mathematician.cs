using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Mathematicians.Domain
{
    public class Mathematician
    {
        private Mathematician()
        {
        }

        public int MathematicianId { get; private set; }

        public Guid Unique { get; private set; }

        public virtual ICollection<MathematicianName> Names { get; } =
            new List<MathematicianName>();
        public IEnumerable<MathematicianName> CurrentNames =>
            Names.Where(x => !x.Next.Any());

        public void SetName(IEnumerable<BigInteger> prior, string firstName, string lastName)
        {
            var priorNames = Names.Where(n => prior.Contains(n.HashCode));
            Names.Add(new MathematicianName(priorNames, firstName, lastName));
        }

        public static Mathematician Create(Guid unique)
        {
            return new Mathematician
            {
                Unique = unique
            };
        }
    }
}
