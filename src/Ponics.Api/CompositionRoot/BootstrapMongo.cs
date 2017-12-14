﻿using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Driver;
using Ponics.Analysis.Levels;
using Ponics.AquaponicSystems;
using Ponics.Data.Mongo.CommandHandlers;
using Ponics.Data.Mongo.QueryHandlers;
using Ponics.Kernel.Data;
using Ponics.Organisms;
using SimpleInjector;

namespace Ponics.Api.CompositionRoot
{
    public class BootstrapMongo:IBootstrap
    {
        public void Bootstrap(Container container)
        {
            var mongodbUri = Environment.GetEnvironmentVariable("MONGODB_URI");

            var mongoUrl = new MongoUrl(mongodbUri);
            var dbname = mongoUrl.DatabaseName;
            var db = new MongoClient(mongoUrl).GetDatabase(dbname);
            container.Register(() => db, Lifestyle.Singleton);

            BsonClassMap.RegisterClassMap<AquaponicSystem>(cm =>
            {
                cm.AutoMap();
                cm.MapIdMember(c => c.Id).SetIdGenerator(CombGuidGenerator.Instance);
            });

            BsonClassMap.RegisterClassMap<Organism>(cm =>
            {
                cm.AutoMap();
                cm.MapIdMember(c => c.Id).SetIdGenerator(CombGuidGenerator.Instance);
            });

            foreach (var toleranceType in GetToleranceTypes())
            {
                var bsonClassMap = new BsonClassMap(toleranceType);
                BsonClassMap.RegisterClassMap(bsonClassMap);
            }

            container.Register<
                IDataCommandHandler<AddOrganism>,
                AddOrganismDataCommandHandler>();

            container.Register<
                IDataCommandHandler<AddSystem>,
                AddSystemDataCommandHandler>();

            container.Register<
                IDataCommandHandler<UpdateSystem>,
                UpdateSystemDataCommandHandler>();

            container.Register<
                IDataCommandHandler<UpdateOrganism>,
                UpdateOrganismDataCommandHandler>();

            container.Register<
                IDataQueryHandler<GetAllOrganisms, List<Organism>>,
                GetAllOrganismsDataQueryHandler>();

            container.Register<
                IDataQueryHandler<GetAllSystems, List<AquaponicSystem>>,
                GetAllSystemsDataQueryHandler>();

            container.Register<
                IDataQueryHandler<GetSystem, AquaponicSystem>,
                GetSystemDataQueryHandler>();
        }

        public static IEnumerable<Type> GetToleranceTypes() =>
            from type in typeof(Tolerance).Assembly.GetExportedTypes()
            where typeof(Tolerance).IsAssignableFrom(type)
            where !type.IsAbstract
            select type;
    }
}
