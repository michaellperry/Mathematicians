﻿using Mathematicians.DAL;
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
        [Route("new", Name = "GetNewMathematician")]
        public IHttpActionResult GetNewMathematician()
        {
            return Ok(CreateRepresentation(Mathematician.Create(Guid.NewGuid())));
        }

        [Route(Name = "GetMathematicians")]
        public IHttpActionResult GetMathematicians()
        {
            using (var context = GetContext())
            {
                return Ok(context.Mathematicians
                    .ToList()
                    .Select(x => CreateRepresentation(x)));
            }
        }

        [Route("{unique}", Name = "GetMathematician")]
        public IHttpActionResult GetMathematician(string unique)
        {
            Guid uniqueGuid = Guid.Empty;
            if (!Guid.TryParse(unique, out uniqueGuid))
                return NotFound();

            using (var context = GetContext())
            {
                var mathematician = context.Mathematicians
                    .SingleOrDefault(x => x.Unique == uniqueGuid);

                if (mathematician == null)
                    return NotFound();

                return Ok(CreateRepresentation(mathematician));
            }
        }

        [HttpPost]
        [Route(Name = "CreateMathematician")]
        public IHttpActionResult CreateMathematician(MathematicianRepresentation representation)
        {
            Guid uniqueGuid = Guid.Empty;
            if (!Guid.TryParse(representation.unique, out uniqueGuid))
                return BadRequest();

            using (var context = GetContext())
            {
                var mathematician = context.Mathematicians
                    .SingleOrDefault(x => x.Unique == uniqueGuid);

                if (mathematician == null)
                {
                    mathematician = context.Mathematicians.Add(Mathematician.Create(uniqueGuid));
                    context.SaveChanges();
                }

                return Created(
                    Url.GetMathematician(mathematician.Unique),
                    CreateRepresentation(mathematician));
            }
        }

        private static Mathematician LoadMathematician(MathematicianContext context, string unique)
        {
            Guid uniqueGuid = Guid.Empty;
            if (Guid.TryParse(unique, out uniqueGuid))
            {
                return context.Mathematicians
                    .SingleOrDefault(x => x.Unique == uniqueGuid);
            }

            return null;
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
