using System;
using NUnit.Framework;
using Tenjin.Tests.AutoFixtureTests.Builders;
using Tenjin.Tests.AutoFixtureTests.Models;
using Tenjin.Tests.Data.Builders.AutoFixture.Options;

namespace Tenjin.Tests.AutoFixtureTests.DataTests.BuildersTests
{
    [TestFixture]
    public class AutoFixtureDataBuilderTests
    {
        [Test]
        public void CreateObject_WhenOmittingRecursions_IgnoresRecursions()
        {
            var builder = GetDataBuilder(true, false);
            var obj = builder.CreateChildObject();

            Assert.IsNotNull(obj);
            Assert.IsNotNull(obj.Parent);
            Assert.IsNotNull(obj.Parent!.Parent);

            Assert.IsNull(obj.Parent.Child);
            Assert.IsNull(obj.Parent.Parent!.Child);
        }

        [Test]
        public void CreateObject_WhenNotOmittingRecursions_DoesNotIgnoreRecursions()
        {
            var builder = GetDataBuilder(false, false);
            Exception? error = null;

            try
            {
                builder.CreateChildObject();
            }
            catch (Exception thrownError)
            {
                error = thrownError;
            }

            Assert.IsNotNull(error);
            Assert.AreEqual("AutoFixture.ObjectCreationExceptionWithPath", error!.GetType().FullName);
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
            Assert.IsNull(car.FrontLeftWheel);
            Assert.IsNull(car.FrontRightWheel);
            Assert.IsNull(car.RearLeftWheel);
            Assert.IsNull(car.RearRightWheel);
            Assert.IsNull(car.Manufacturer);
        }

        private static void AssertNotNullWheel(AutoFixtureCarWheelObject wheel)
        {
            Assert.IsNotNull(wheel);
            Assert.IsNotEmpty(wheel.Name);
            Assert.NotZero(wheel.SizeInInches);
        }

        private static void AssertNotNullManufacturer(AutoFixtureCarManufacturerObject manufacturer)
        {
            Assert.IsNotNull(manufacturer);
            Assert.IsNotEmpty(manufacturer.Name);
            Assert.IsNotEmpty(manufacturer.Country);
            Assert.Greater(manufacturer.Founded, DateTime.MinValue);
        }

        private static void AssertNotNullCar(AutoFixtureCarObject car)
        {
            Assert.IsNotNull(car);
            Assert.IsNotEmpty(car.Model);
            Assert.NotZero(car.Year);
            Assert.NotZero(car.Price);
        }

        private static UnitTestAutoFixtureDataBuilder GetDataBuilder(
            bool omitRecursion = true,
            bool omitComplexTypes = true)
        {
            var options = new AutoFixtureDataBuilderOptions
            {
                OmitComplexTypes = omitComplexTypes,
                OmitRecursions = omitRecursion,
            };

            return new UnitTestAutoFixtureDataBuilder(options);
        }
    }
}
