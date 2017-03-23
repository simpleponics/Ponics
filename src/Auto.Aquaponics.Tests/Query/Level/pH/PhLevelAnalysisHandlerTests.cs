﻿using System;
using Auto.Aquaponics.Ph;
using Auto.Aquaponics.Analysis.Level.Ph;
using Auto.Aquaponics.Kernel;
using Auto.Aquaponics.Organisms;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace Auto.Aquaponics.Tests.Query.Level.pH
{
    public abstract class PhLevelAnalysisHandlerTests: 
        LevelAnalysisHandlerTests<
            PhLevelAnalysisQueryHandler,
            IPhLevelAnalysisMagicStrings,
            PhLevelAnalysisQuery,
            PhLevelAnalysis
        >
    {
        protected const string LowPhArgumentOutOfRangeExceptionMessage = "Reported ph is too low";
        protected const string HightPhArgumentOutOfRangeExceptionMessage = "Reported ph is too high";
        protected const string OrganismPhTolerancesNotDefinedExceptionMessage = "Organism pH tolerance not defined";
        
        protected const string PhKey = "pH";

        protected IPhRange PhRange;

        protected override void DoSetUp()
        {
            PhRange = Substitute.For<IPhRange>();

            LevelQueryHandlerMagicStrings.LevelKey.Returns(PhKey);

            PhRange.Ceiling.Returns(14);
            PhRange.Floor.Returns(0);

            LevelQueryHandlerMagicStrings.LowPhArgumentOutOfRangeExceptionMessage.Returns(LowPhArgumentOutOfRangeExceptionMessage);
            LevelQueryHandlerMagicStrings.HightPhArgumentOutOfRangeExceptionMessage.Returns(HightPhArgumentOutOfRangeExceptionMessage);
            LevelQueryHandlerMagicStrings.OrganismPhTolerancesNotDefinedExceptionMessage.Returns(OrganismPhTolerancesNotDefinedExceptionMessage);

            Sut = new PhLevelAnalysisQueryHandler(LevelQueryHandlerMagicStrings, PhRange);
        }

        [Test]
        public void organism_pH_tolerance_not_defined_ArgumentNullException_thrown()
        {
            Organism = Substitute.For<Organism>();
            var tolerance = new Tolerance("SomeName", Scale.None, 0, 0, 0, 0 );
            Organism.AddTolerances(tolerance);

            var query = new PhLevelAnalysisQuery(0);
            query.Organism = Organism;
            Action act = () => Sut.Handle(query);

            AssertArgumentNullException(act, OrganismPhTolerancesNotDefinedExceptionMessage, nameof(query.Organism.Tolerances));
        }

        [Test]
        public void pH_lower_than_floor_ArgumentOutOfRangeException_thrown()
        {
            var query = new PhLevelAnalysisQuery(-2);
            query.Organism = Organism;

            Action act = () => Sut.Handle(query);

            AssertArgumentOutOfRangeException(act, LowPhArgumentOutOfRangeExceptionMessage, nameof(query.Value));
        }

        [Test]
        public void pH_higher_than_ceiling_ArgumentOutOfRangeException_thrown()
        {
            var query = new PhLevelAnalysisQuery(16);
            query.Organism = Organism;

            Action act = () => Sut.Handle(query);

            AssertArgumentOutOfRangeException(act, HightPhArgumentOutOfRangeExceptionMessage, nameof(query.Value));
        }

        [Test]
        public void pH_of_14_HydrogenIon_and_HydroxideIons_concentration_is_correct()
        {
            var query = new PhLevelAnalysisQuery(14);
            query.Organism = Organism;
            var result = Sut.Handle(query);
            result.HydrogenIonConcentration.Should().Be(1E-14);
            result.HydroxideIonsConcentration.Should().Be(1);
        }

        [Test]
        public void pH_of_13_HydrogenIon_and_HydroxideIons_concentration_is_correct()
        {
            var query = new PhLevelAnalysisQuery(13);
            query.Organism = Organism;
            var result = Sut.Handle(query);
            result.HydrogenIonConcentration.Should().Be(1E-13);
            result.HydroxideIonsConcentration.Should().Be(0.1);
        }

        [Test]
        public void pH_of_1_HydrogenIon_and_HydroxideIons_concentration_is_correct()
        {
            var query = new PhLevelAnalysisQuery(1);
            query.Organism = Organism;
            var result = Sut.Handle(query);
            result.HydrogenIonConcentration.Should().Be(0.1);
            result.HydroxideIonsConcentration.Should().Be(1E-13);
        }

        [Test]
        public void pH_of_2_HydrogenIon_and_HydroxideIons_concentration_is_correct()
        {
            var query = new PhLevelAnalysisQuery(2);
            query.Organism = Organism;
            var result = Sut.Handle(query);
            result.HydrogenIonConcentration.Should().Be(0.01);
            result.HydroxideIonsConcentration.Should().Be(1E-12);
        }

        private static void AssertArgumentOutOfRangeException(Action act, string message, string paramName)
        {
            act.ShouldThrow<ArgumentOutOfRangeException>()
                .WithMessage($"{message}\r\nParameter name: {paramName}");
        }
    }
}