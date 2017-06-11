using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Mathematicians.Domain
{
    public class MathematicianName
    {
        private Lazy<BigInteger> _hashCodeInt;

        private MathematicianName()
        {
            _hashCodeInt = new Lazy<BigInteger>(ComputeHashCodeInt);
        }

        internal MathematicianName(IEnumerable<MathematicianName> priorNames, string firstName, string lastName)
        {
            Prior = priorNames.ToList();
            FirstName = firstName;
            LastName = lastName;
            HashCode = FirstName.Sha256Hash()
                .Concatenate(LastName.Sha256Hash())
                .Concatenate(Prior.Select(x => x.HashCodeInt).ToArray())
                .ToByteArray();
            _hashCodeInt = new Lazy<BigInteger>(ComputeHashCodeInt);
        }

        public int MathematicianNameId { get; private set; }

        public int MathematicianId { get; private set; }
        public virtual Mathematician Mathematician { get; private set; }

        public byte[] HashCode { get; private set; }
        public BigInteger HashCodeInt => _hashCodeInt.Value;

        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public virtual ICollection<MathematicianName> Prior { get; } =
            new List<MathematicianName>();
        public virtual ICollection<MathematicianName> Next { get; } =
            new List<MathematicianName>();

        private BigInteger ComputeHashCodeInt()
        {
            return new BigInteger(HashCode);
        }
    }
}