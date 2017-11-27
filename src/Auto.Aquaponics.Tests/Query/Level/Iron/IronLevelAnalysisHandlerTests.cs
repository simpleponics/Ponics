﻿using Auto.Aquaponics.Analysis.Levels.Iron;
using Auto.Aquaponics.Tolerances;
using NSubstitute;

namespace Auto.Aquaponics.Tests.Query.Level.Iron
{
    public abstract class IronLevelAnalysisHandlerTests:
    LevelAnalysisHandlerTests<
    AnalyseIronQueryHandler,
    IAnalyseIronMagicStrings,
    AnalyseIron,
    IronAnalysis,
    IronTolerance
    >
    {

        protected override void DoSetUp()
        {
            LevelQueryHandlerMagicStrings.LevelsKey.Returns("Iron");
            Sut = new AnalyseIronQueryHandler(LevelQueryHandlerMagicStrings, GetAllOrganismsDataQueryHandler);
        }
    }
}