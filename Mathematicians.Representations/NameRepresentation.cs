using Mathematicians.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Mathematicians.Representations
{
    public class NameRepresentation
    {
        private NameRepresentation()
        {
        }

        private NameRepresentation(List<string> prior, string firstName, string lastName)
        {
            this.prior = prior;
            this.firstName = firstName;
            this.lastName = lastName;
        }

        public List<string> prior { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }

        public static NameRepresentation FromEntities(IEnumerable<MathematicianName> names)
        {
            var prior = names
                .Select(x => x.HashCode.ToString())
                .ToList();
            var firstName = names
                .OrderBy(x => x.HashCode)
                .Select(x => x.FirstName)
                .FirstOrDefault();
            var lastName = names
                .OrderBy(x => x.HashCode)
                .Select(x => x.LastName)
                .FirstOrDefault();
            return new NameRepresentation(
                prior,
                firstName,
                lastName);
        }
    }
}
