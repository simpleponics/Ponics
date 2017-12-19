﻿using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using Ponics.Analysis.Levels.Commands;
using Ponics.Analysis.Levels.Handlers;
using Ponics.Organisms;

namespace Ponics.Tests.Command.Tolerance
{
    [TestFixture]
    public class DeleteToleranceTests : ToleranceTests<MockTolerance, DeleteTolerance<MockTolerance>, DeleteToleranceCommandHandler<MockTolerance>>
    {
        [SetUp]
        public void SetUp()
        {
          Sut = new DeleteToleranceCommandHandler<MockTolerance>(
              GetAllOrganismsDataQueryHandler,
              UpdateOrganismDataCommandHandler,
              ToleranceMagicStrings);

            Organism.Tolerances.Add(MockTolerance);

        }

        [Test]
        public void Delete_Tolerance_Organism_Updated()
        {
            //Arrange
            var command = Substitute.For<DeleteTolerance<MockTolerance>>();
            command.OrganismId = Organism.Id;
            command.Tolerance.Returns(MockTolerance);

            //Act
            Sut.Handle(command);

            //Assert
            Organism.Tolerances.Should().NotContain(MockTolerance);
            UpdateOrganismDataCommandHandler.Received().Handle(Arg.Any<UpdateOrganism>());
        }
    }
}
