using System;
using FluentAssertions;
using NUnit.Framework;
using Tenjin.Tests.AutoFixture.Data.Builders.Options;
using Tenjin.Tests.AutoFixture.Tests.Builders;
using Tenjin.Tests.AutoFixture.Tests.Models;

namespace Tenjin.Tests.AutoFixture.Tests.DataTests.BuildersTests;

[TestFixture, Parallelizable(ParallelScope.Children)]
public class AutoFixtureDataBuilderTests
{
    [Test]
    public void CreateObject_WhenOmittingRecursions_IgnoresRecursions()
    {
        var builder = GetDataBuilder(true, false);
        var obj = builder.CreateChildObject();

        obj.Should().NotBeNull();
        obj.Parent.Should().NotBeNull();
        obj.Parent!.Parent.Should().NotBeNull();

        obj.Parent.Child.Should().BeNull();
        obj.Parent.Parent!.Child.Should().BeNull();
    }

    [Test]
    public void CreateObject_WhenNotOmittingComplexTypes_CreatesComplexTypes()
    {
        var builder = GetDataBuilder(false, false);
        var car = builder.CreateCarObject();

        AssertNotNullCar(car);
        AssertNotNullWheel(car.FrontLeftWheel);
        AssertNotNullWheel(car.FrontRightWheel);
        AssertNotNullWheel(car.RearLeftWheel);
        AssertNotNullWheel(car.RearRightWheel);
        AssertNotNullManufacturer(car.Manufacturer);
    }

    [Test]
    public void CreateObject_WhenOmittingComplexTypes_DoesNotCreateComplexTypes()
    {
        var builder = GetDataBuilder(false);
        var car = builder.CreateCarObject();

        AssertNotNullCar(car);
        car.FrontLeftWheel.Should().BeNull();
        car.FrontRightWheel.Should().BeNull();
        car.RearLeftWheel.Should().BeNull();
        car.RearRightWheel.Should().BeNull();
        car.Manufacturer.Should().BeNull();
    }

    private static void AssertNotNullWheel(AutoFixtureCarWheelObject wheel)
    {
        wheel.Should().NotBeNull();
        wheel.Name.Should().NotBeEmpty();
        wheel.SizeInches.Should().NotBe(0);
    }

    private static void AssertNotNullManufacturer(AutoFixtureCarManufacturerObject manufacturer)
    {
        manufacturer.Should().NotBeNull();

        manufacturer.Name.Should().NotBeEmpty();
        manufacturer.Country.Should().NotBeEmpty();
        manufacturer.Founded.Ticks.Should().BeGreaterThan(DateTime.MinValue.Ticks);
    }

    private static void AssertNotNullCar(AutoFixtureCarObject car)
    {
        car.Should().NotBeNull();
        car.Model.Should().NotBeEmpty();
        car.Year.Should().NotBe(0);
        car.Price.Should().NotBe(0);
    }

    private static UnitTestAutoFixtureDataBuilder GetDataBuilder(
        bool omitRecursion = true,
        bool omitComplexTypes = true)
    {
        var options = new AutoFixtureDataBuilderOptions
        {
            OmitComplexTypes = omitComplexTypes,
            OmitRecursions = omitRecursion
        };

        return new UnitTestAutoFixtureDataBuilder(options);
    }
}