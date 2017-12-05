﻿using ServiceStack;

namespace Ponics.Analysis.Levels.Salinity
{
    [Api("Returns Analysis of Salinity level for an Organism")]
    [Route("/organisms/{OrganismId}/Tolerances/Salinity/{Value}", "GET")]
    public class AnalyseSalinity : AnalyseQuery<SalinityAnalysis, SalinityTolerance>
    {
    }
}