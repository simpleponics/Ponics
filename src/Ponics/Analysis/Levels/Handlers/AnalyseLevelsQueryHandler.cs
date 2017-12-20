﻿using System;
using System.Collections.Generic;
using System.Linq;
using Ponics.Analysis.Levels.MagicStrings;
using Ponics.Kernel.Data;
using Ponics.Organisms;
using Ponics.Queries;

namespace Ponics.Analysis.Levels.Handlers
{
    public abstract class AnalyseLevelsQueryHandler<TQuery, TResult, TTolerance> : IQueryHandler<TQuery, TResult>
        where TQuery: AnalyseToleranceQuery<TResult, TTolerance>
        where TResult:ToleranceAnalysis<TTolerance>, new()
        where TTolerance : Tolerance
    {
        protected readonly ILevelsMagicStrings MagicStrings;
        private readonly IDataQueryHandler<GetAllOrganisms, List<Organism>> _getAllOrganismsDataQueryHandler;

        protected AnalyseLevelsQueryHandler(
            ILevelsMagicStrings magicStrings,
            IDataQueryHandler<GetAllOrganisms, List<Organism>> getAllOrganismsDataQueryHandler
            )
        {
            MagicStrings = magicStrings;
            _getAllOrganismsDataQueryHandler = getAllOrganismsDataQueryHandler;
        }

        protected abstract TResult Analyse(TQuery query, TResult analysis, Organism organism);
        protected abstract void OrganismToleranceNotDefined();

        public TResult Handle(TQuery query)
        {
            var organisms = _getAllOrganismsDataQueryHandler.Handle(new GetAllOrganisms());
            var organism = organisms.SingleOrDefault(o => o.Id == query.OrganismId);
            if (organism == default(Organism))
            {
                throw new ArgumentNullException(nameof(organism), MagicStrings.OrganismNotDefined);
            }

            if (organism.Tolerances == null || !organism.Tolerances.Any())
            {
                throw new ArgumentNullException(nameof(organism.Tolerances), MagicStrings.OrganismTolerancesNotDefined);
            }

            if (!organism.Tolerances.Any(t => t is TTolerance))
            {
                OrganismToleranceNotDefined();
            }

            var analysis = new TResult
            {
                IdealForOrganism = IdealForOrganism(query.Value, organism, MagicStrings.LevelName),
                SutablalForOrganism = SuitableForOrganism(query.Value, organism, MagicStrings.LevelName),
                Tolerance = organism.Tolerances.Single(t => t is TTolerance) as TTolerance
            };

            return Analyse(query, analysis, organism);
        }

        protected bool SuitableForOrganism(double value, Organism organism, string key)
        {
            var tolerance = organism.Tolerances.Single(t => t is TTolerance);

            return tolerance.Lower <= value && tolerance.Upper >= value;
        }

        protected bool IdealForOrganism(double value, Organism organism, string key)
        {
            var tolerance = organism.Tolerances.Single(t => t is TTolerance);
            return tolerance.DesiredLower <= value && tolerance.DesiredUpper >= value;
        }
    }
}