using System.Collections.Generic;

namespace Mathematicians.Domain
{
    public class MathematicianName
    {
        private MathematicianName()
        {
        }

        public int MathematicianNameId { get; private set; }

        public int MathematicianId { get; private set; }
        public virtual Mathematician Mathematician { get; private set; }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public virtual ICollection<MathematicianName> Prior { get; } =
            new List<MathematicianName>();
    }
}