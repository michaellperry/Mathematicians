using Mathematicians.API.Models;
using Mathematicians.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
                    .Select(x => MathematicianRepresentation.FromEntity(x, Url)));
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

                return Ok(MathematicianRepresentation.FromEntity(mathematician, Url));
            }
        }

        private static MathematicianContext GetContext()
        {
            return new MathematicianContext("Mathematicians");
        }
    }
}
