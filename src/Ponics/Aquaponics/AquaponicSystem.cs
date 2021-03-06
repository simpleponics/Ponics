﻿using System.Collections.Generic;
using Ponics.Components.Commands;
using ServiceStack;

namespace Ponics.Aquaponics
{
    public class AquaponicSystem : PonicsSystem
    {
        [ApiMember(Name = "Closed", Description = "Indicates if the system is closed",
            ParameterType = "body", DataType = "boolean")]
        public bool Closed { get; set; }

        [ApiMember(ExcludeInSchema = true)]
        public List<ComponentConnection> ComponentConnections { get; set; }

        public AquaponicSystem()
        {
            Closed = true;
            ComponentConnections = new List<ComponentConnection>();
        }
    }
}
