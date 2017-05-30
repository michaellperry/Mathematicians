﻿using Mathematicians.Domain;
using System.Collections.Generic;

namespace Mathematicians.Representations
{
    public class MathematicianRepresentation
    {
        public MathematicianRepresentation()
        {
        }

        public Dictionary<string, LinkReference> _links { get; set; }

        public static MathematicianRepresentation FromEntity(Mathematician mathematician)
        {
            return new MathematicianRepresentation();
        }
    }
}
