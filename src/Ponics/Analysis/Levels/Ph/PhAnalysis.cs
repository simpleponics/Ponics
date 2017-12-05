﻿using System.Collections.Generic;

namespace Ponics.Analysis.Levels.Ph
{
    public class PhAnalysis : Analysis<PhTolerance>
    {
        public double HydrogenIonConcentration   { get; set; }
        public double HydroxideIonsConcentration { get; set; }
        public List<string> Warnings { get; set; }

        public PhAnalysis()
        {
            Warnings = new List<string>();
        }
    }
}