namespace Tenjin.Tests.AutoFixture.Data.Builders.Options
{
    public class AutoFixtureDataBuilderOptions
    {
        public static readonly AutoFixtureDataBuilderOptions Default = new()
        {
            OmitComplexTypes = true,
            OmitRecursions = true
        };

        public bool OmitRecursions { get; set; }

        public bool OmitComplexTypes { get; set; }
    }
}
