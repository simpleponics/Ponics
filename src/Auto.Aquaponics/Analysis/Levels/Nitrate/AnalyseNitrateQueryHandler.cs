﻿using Auto.Aquaponics.Organisms;
using System.Collections.Generic;
using Auto.Aquaponics.Kernel.DataQuery;

namespace Auto.Aquaponics.Analysis.Levels.Nitrate
{
    public class AnalyseNitrateQueryHandler: AnalyseLevelsQueryHandler<AnalyseNitrate, NitrateAnalysis>
    {
        private readonly IAnalyseNitrateMagicStrings _magicStrings;

        public AnalyseNitrateQueryHandler(
            IAnalyseNitrateMagicStrings magicStrings,
            IDataQueryHandler<GetAllOrganisms, IList<Organism>> getAllOrganismsDataQueryHandler
        ) : base(magicStrings, getAllOrganismsDataQueryHandler)
        {
            _magicStrings = magicStrings;
        }

        protected override NitrateAnalysis Analyse(AnalyseNitrate query, NitrateAnalysis analysis, Organism organism)
        {
            return analysis;
        }

        protected override void OrganismToleranceNotDefined()
        {
            throw new System.NotImplementedException();
        }
    }
}
