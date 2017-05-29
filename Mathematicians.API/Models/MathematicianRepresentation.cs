using System;
using Mathematicians.Domain;
using System.Collections.Generic;
using System.Web.Http.Routing;
using Mathematicians.API.Controllers;

namespace Mathematicians.API.Models
{
    public class MathematicianRepresentation
    {
        public MathematicianRepresentation()
        {
        }

        public Dictionary<string, LinkReference> _links { get; set; }

        public static MathematicianRepresentation FromEntity(Mathematician mathematician, UrlHelper url)
        {
            return new MathematicianRepresentation
            {
                _links = new Dictionary<string, LinkReference>
                {
                    ["self"] = new LinkReference
                    {
                        href = url.GetMathematician(mathematician.Unique)
                    }
                }
            };
        }
    }
}
