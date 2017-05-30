using Mathematicians.DAL;
using Mathematicians.Domain;
using Mathematicians.Representations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Routing;

namespace Mathematicians.API.Controllers
{
    public static class MathematiciansControllerHelper
    {
        public static string GetMathematician(this UrlHelper url, Guid unique)
        {
            return url.Link("GetMathematician", new { unique = unique.ToString() });
        }
    }

    [RoutePrefix("api/mathematicians")]
    public class MathematiciansController : ApiController
    {
        [Route(Name = "GetMathematicians")]
        public IHttpActionResult Get()
        {
            using (var context = GetContext())
            {
                return Ok(context.Mathematicians
                    .ToList()
                    .Select(x => CreateRepresentation(x)));
            }
        }

        [Route(Name = "GetMathematician")]
        public IHttpActionResult Get(string unique)
        {
            Guid uniqueGuid = Guid.Empty;
            if (!Guid.TryParse(unique, out uniqueGuid))
                return NotFound();

            using (var context = GetContext())
            {
                var mathematician = context.Mathematicians.SingleOrDefault(x => x.Unique == uniqueGuid);
                if (mathematician == null)
                    return NotFound();

                return Ok(CreateRepresentation(mathematician));
            }
        }

        private static MathematicianContext GetContext()
        {
            return new MathematicianContext("Mathematicians");
        }

        private MathematicianRepresentation CreateRepresentation(Mathematician mathematician)
        {
            var representation = MathematicianRepresentation.FromEntity(mathematician);
            representation._links = new Dictionary<string, LinkReference>
            {
                ["self"] = new LinkReference
                {
                    href = Url.GetMathematician(mathematician.Unique)
                }
            };
            return representation;
        }
    }
}
