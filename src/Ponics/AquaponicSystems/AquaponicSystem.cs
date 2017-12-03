﻿using System;
using System.Collections.Generic;
using System.Linq;
using Ponics.Components;
using ServiceStack;

namespace Ponics.AquaponicSystems
{
    public class AquaponicSystem
    {
        [ApiMember(ExcludeInSchema = true)]
        public Guid Id { get; set; }

        [ApiMember(Name = "Closed", Description = "Indicates if the system is closed",
            ParameterType = "body", DataType = "boolean")]
        public bool Closed { get; set; }

        [ApiMember(Name = "Name", Description = "The name of the aquaponic system",
            ParameterType = "body", DataType = "string", IsRequired = true)]
        public string Name { get; set; }

        [ApiMember(ExcludeInSchema = true)]
        public IList<Component> Components { get; set; }

        [ApiMember(ExcludeInSchema = true)]
        public IList<ComponentConnection> ComponentConnections { get; set; }

        public AquaponicSystem()
        {
            Closed = true;
            Components = new List<Component>();
            ComponentConnections = new List<ComponentConnection>();
        }

        public void AddComponents(params Component[] components)
        {
            for (var i = 0; i < components.Length; i++)
            {
                Components.Add(components[i]);
                if (i > 0)
                {
                    ComponentConnections.Add(new ComponentConnection
                    {
                        SourceId = components[i - 1].Id,
                        TargetId = components[i].Id
                    });
                }
            }

            if (Closed)
            {
                ComponentConnections.Add(
                    new ComponentConnection
                    {
                        SourceId = components.Last().Id,
                        TargetId = components.First().Id
                    });
            }
        }
    }
}
