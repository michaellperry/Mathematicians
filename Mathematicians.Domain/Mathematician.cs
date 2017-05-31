using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
