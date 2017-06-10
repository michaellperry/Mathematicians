using System;
using System.Collections.Generic;

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

        public static Mathematician Create(Guid unique)
        {
            return new Mathematician
            {
                Unique = unique
            };
        }
    }
}
