using Mathematicians.Domain;
using System.Collections.Generic;
using System.Linq;

namespace Mathematicians.Representations
{
    public class NameRepresentation
    {
        private NameRepresentation()
        {
        }

        private NameRepresentation(List<string> ids, string firstName, string lastName)
        {
            this.ids = ids;
            this.firstName = firstName;
            this.lastName = lastName;
        }

        public List<string> ids { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }

        public static NameRepresentation FromEntities(IEnumerable<MathematicianName> names)
        {
            var ids = names
                .Select(x => x.HashCodeInt.ToBase64String())
                .ToList();
            var firstName = names
                .OrderBy(x => x.HashCodeInt)
                .Select(x => x.FirstName)
                .FirstOrDefault();
            var lastName = names
                .OrderBy(x => x.HashCodeInt)
                .Select(x => x.LastName)
                .FirstOrDefault();
            return new NameRepresentation(
                ids,
                firstName,
                lastName);
        }
    }
}
