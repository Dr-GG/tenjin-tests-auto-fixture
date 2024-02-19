using AutoFixture;
using Tenjin.Tests.AutoFixture.Data.Builders;
using Tenjin.Tests.AutoFixture.Data.Builders.Options;
using Tenjin.Tests.AutoFixture.Tests.Models;

namespace Tenjin.Tests.AutoFixture.Tests.Builders;

public class UnitTestAutoFixtureDataBuilder(AutoFixtureDataBuilderOptions options) : AutoFixtureDataBuilder(options)
{
    public AutoFixtureChildObject CreateChildObject()
    {
        return Fixture.Build<AutoFixtureChildObject>().Create();
    }

    public AutoFixtureCarObject CreateCarObject()
    {
        return Fixture.Build<AutoFixtureCarObject>().Create();
    }

    public override void Save() { }
}