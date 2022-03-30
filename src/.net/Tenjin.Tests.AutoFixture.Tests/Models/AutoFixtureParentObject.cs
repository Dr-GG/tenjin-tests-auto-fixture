namespace Tenjin.Tests.AutoFixture.Tests.Models
{
    public class AutoFixtureParentObject
    {
        public AutoFixtureGrandParentObject? Parent { get; set; }
        public AutoFixtureChildObject? Child { get; set; }
    }
}
