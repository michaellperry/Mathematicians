using System;

namespace Mathematicians.Domain
{
    public class Mathematician
    {
        private Mathematician()
        {
        }

        public int MathematicianId { get; private set; }

        public Guid Unique { get; private set; }

        public static Mathematician Create(Guid unique)
        {
            return new Mathematician
            {
                Unique = unique
            };
        }
    }
}
