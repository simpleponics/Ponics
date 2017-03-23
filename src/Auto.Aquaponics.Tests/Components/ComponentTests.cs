﻿using Auto.Aquaponics.Components;
using Auto.Aquaponics.Organisms;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace Auto.Aquaponics.Tests.Components
{
    [TestFixture]
    public class ComponentTests
    {
        public Component Sut;

        [SetUp]
        public void SetUp()
        {
            Sut = Substitute.For<Component>();
        }

        [Test]
        public void Can_Add_Organism()
        {
            //Arrange
            var organism = Substitute.For<Organism>();

            //Act
            Sut.AddOrganisms(organism);

            //Assert
            Sut.Organisms.Should().Contain(organism);
        }

        [Test]
        public void Can_Add_Organisms()
        {
            //Arrange
            var fish = Substitute.For<Organism>();
            var plant = Substitute.For<Organism>();

            //Act
            Sut.AddOrganisms(fish, plant);

            //Assert
            Sut.Organisms.Should().Contain(fish);
            Sut.Organisms.Should().Contain(plant);
        }
    }
}