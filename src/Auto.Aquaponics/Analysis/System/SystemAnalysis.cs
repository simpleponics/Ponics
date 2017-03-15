using System.Collections.Generic;
using Auto.Aquaponics.Analysis.Level;
using Auto.Aquaponics.Components;
using Auto.Aquaponics.Kernel.Query;
using Auto.Aquaponics.Organisms;

namespace Auto.Aquaponics.Analysis.System
{
    public class SystemAnalysis: QueryResult
    {
        public IDictionary<Component, IDictionary<Organism, IList<LevelAnalysis>>> Results { get; }

        public SystemAnalysis()
        {
            Results = new Dictionary<Component, IDictionary<Organism, IList<LevelAnalysis>>>();
        }
    }
}
