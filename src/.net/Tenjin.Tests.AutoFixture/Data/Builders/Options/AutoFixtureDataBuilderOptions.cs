namespace Tenjin.Tests.AutoFixture.Data.Builders.Options;

/// <summary>
/// A collection of options to be used in the AutofixtureDataBuilder instance.
/// </summary>
public class AutoFixtureDataBuilderOptions
{
    /// <summary>
    /// The default instance.
    /// </summary>
    public static readonly AutoFixtureDataBuilderOptions Default = new()
    {
        OmitComplexTypes = true,
        OmitRecursions = true
    };

    /// <summary>
    /// The flag indicating if recursions should be ignored.
    /// </summary>
    public bool OmitRecursions { get; set; }

    /// <summary>
    /// The flag indicating if complex types should be ignored.
    /// </summary>
    public bool OmitComplexTypes { get; set; }
}