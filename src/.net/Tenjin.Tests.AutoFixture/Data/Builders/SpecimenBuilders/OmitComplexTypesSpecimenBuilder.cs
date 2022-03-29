using System.Reflection;
using AutoFixture.Kernel;

namespace Tenjin.Tests.AutoFixture.Data.Builders.SpecimenBuilders
{
    public class OmitComplexTypesSpecimenBuilder : ISpecimenBuilder
    {
        private static readonly IEnumerable<Type> IgnoreComplexTypes = new[]
        {
            typeof(string)
        };

        public object Create(object request, ISpecimenContext context)
        {
            if (request is not PropertyInfo property)
            {
                return new NoSpecimen();
            }

            var type = property.PropertyType;

            if (IgnoreComplexTypes.Contains(type))
            {
                return new NoSpecimen();
            }

            if (type.IsClass)
            {
                return new OmitSpecimen();
            }

            return new NoSpecimen();
        }
    }
}
