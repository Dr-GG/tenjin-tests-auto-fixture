using AutoFixture;
using Tenjin.Tests.AutoFixture.Data.Builders.Options;
using Tenjin.Tests.AutoFixture.Data.Builders.SpecimenBuilders;
using Tenjin.Tests.Data.Builders;

namespace Tenjin.Tests.AutoFixture.Data.Builders;

/// <summary>
/// The extended DataBuilder tailored towards the AutoFixture library.
/// </summary>
public abstract class AutoFixtureDataBuilder : DataBuilder
{
    /// <summary>
    /// Creates a new instance.
    /// </summary>
    protected AutoFixtureDataBuilder(AutoFixtureDataBuilderOptions? options = null)
    {
        options ??= AutoFixtureDataBuilderOptions.Default;

        Fixture = new Fixture();

        InitialiseFixture(options);
    }

    /// <summary>
    /// The Fixture instance to be used.
    /// </summary>
    public Fixture Fixture { get; }

    private void InitialiseFixture(AutoFixtureDataBuilderOptions options)
    {
        if (options.OmitRecursions)
        {
            OmitRecursion();
        }

        if (options.OmitComplexTypes)
        {
            OmitComplexTypes();
        }
    }

    private void OmitComplexTypes()
    {
        var builder = new AutoFixtureOmitComplexTypesSpecimenBuilder();

        Fixture.Customizations.Insert(0, builder);
    }

    private void OmitRecursion()
    {
        Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        Fixture.Behaviors
            .OfType<ThrowingRecursionBehavior>()
            .ToList()
            .ForEach(x => Fixture.Behaviors.Remove(x));
    }
}