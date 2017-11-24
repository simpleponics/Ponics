﻿using System;
using System.Collections.Generic;
using System.Linq;
using Auto.Aquaponics.Components;

namespace Auto.Aquaponics.AquaponicSystems
{
    public class AquaponicSystem
    {
        private readonly bool _closed;
        public Guid Id { get; }
        public string Name { get; }
        public ICollection<Component> Components { get; }
        public ICollection<ComponentConnection> ComponentConnections { get; }

        public AquaponicSystem(Guid id, string name, bool closed = true)
        {
            Name = name;
            _closed = closed;
            Id = id;
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
                    ComponentConnections.Add(new ComponentConnection(components[i - 1], components[i]));
                }
            }

            if (_closed)
            {
                ComponentConnections.Add(new ComponentConnection(components.Last(), components.First()));
            }
        }
    }
}
