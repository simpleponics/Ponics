﻿using System;
using System.Collections.Generic;
using NodaTime;
using NodaTime.Text;
using Ponics.Analysis.Levels;
using Ponics.Aquaponics;
using Ponics.Components;
using Ponics.Components.Commands;
using Ponics.HardCodedData.Organisms;

namespace Ponics.HardCodedData.AquaponicSystems
{
    public class AaronsAquaponicSystem
    {
        public static AquaponicSystem SeedSystem()
        {
            var system = new AquaponicSystem();
            system.Id = Guid.Parse("47236a2e40f047a2923034c610c5e444");
            system.Name = "Aaron's Aquaponics System";

            var nitrosomonas = new Nitrosomonas();
            var nitrospira = new Nitrospira();

            system.SystemWideOrganisms = new List<Guid>();
            system.SystemWideOrganisms.Add(nitrosomonas.Id);
            system.SystemWideOrganisms.Add(nitrospira.Id);

            var fishTank = new Component
            {
                Id = Guid.Parse("77eb39a3-00fe-4cf7-b422-37c1408bfbd6"),
                Name = "fishTank"
            };

            fishTank.AddOrganisms(new SilverPerch().Id);
            system.Components.Add(fishTank);

            var growBed = new Component
            {
                Id = Guid.Parse("02067a53-cdad-4ccb-9f04-3cab69b10008"),
                Name = "growBed"
            };
            growBed.AddOrganisms(new Worm().Id);
            system.Components.Add(growBed);

            var sumpTank = new Component
            {
                Id = Guid.Parse("a0e67c51-fc19-4ffc-a39e-167e3711f68b"),
                Name = "sumpTank"
            };
            sumpTank.AddOrganisms(new GoldFish().Id);
            system.Components.Add(sumpTank);

            system.ComponentConnections.Add( new ComponentConnection
            {
                SourceId = fishTank.Id,
                TargetId = growBed.Id
            });

            system.ComponentConnections.Add(new ComponentConnection
            {
                SourceId = growBed.Id,
                TargetId = sumpTank.Id
            });

            system.ComponentConnections.Add(new ComponentConnection
            {
                SourceId = sumpTank.Id,
                TargetId = fishTank.Id
            });
            
            var zdt = ZonedDateTimePattern.CreateWithInvariantCulture("G", DateTimeZoneProviders.Tzdb)
                .Parse("2017-01-06T18:47:34 Australia/Sydney (+11)")
                .Value;

            system.LevelReadings = new List<LevelReading>
            {
                new LevelReading
                {
                    DateTime = zdt.Plus(Duration.FromDays(1)),
                    Type = "pH",
                    Value = 7
                },
                new LevelReading
                {
                    DateTime = zdt.Plus(Duration.FromDays(2)),
                    Type = "pH",
                    Value = 7.2
                },
                new LevelReading
                {
                    DateTime = zdt.Plus(Duration.FromDays(3)),
                    Type = "pH",
                    Value = 7
                },
                new LevelReading
                {
                    DateTime = zdt.Plus(Duration.FromDays(4)),
                    Type = "pH",
                    Value = 6.6
                },
                new LevelReading
                {
                    DateTime = zdt.Plus(Duration.FromDays(1)),
                    Type = "Ammonia",
                    Value = .2
                },
                new LevelReading
                {
                    DateTime = zdt.Plus(Duration.FromDays(2)),
                    Type = "Ammonia",
                    Value = 0
                },
                new LevelReading
                {
                    DateTime = zdt.Plus(Duration.FromDays(3)),
                    Type = "Ammonia",
                    Value = .8
                },
                new LevelReading
                {
                    DateTime = zdt.Plus(Duration.FromDays(4)),
                    Type = "Ammonia",
                    Value = .1
                }
            };

            return system;
        }
    }
}
