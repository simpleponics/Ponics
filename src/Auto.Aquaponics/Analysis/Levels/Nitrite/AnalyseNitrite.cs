﻿using ServiceStack;

namespace Auto.Aquaponics.Analysis.Levels.Nitrite
{
    [Api("Returns Analysis of Nitrite levels for an Organism")]
    [Route("/Levels/Nitrite", "POST")]
    public class AnalyseNitrite : AnalyseQuery<NitriteAnalysis>
    {
    }
}
