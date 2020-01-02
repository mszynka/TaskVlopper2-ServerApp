using FluentNHibernate.Utils;
using StructureMap;
using StructureMap.Graph;
using StructureMap.Graph.Scanning;

namespace ServerApp.Api
{
    public class DiRegistry : Registry
    {
        public DiRegistry()
        {
            Scan(_ =>
            {
                _.AssemblyContainingType<Program>();
                _.Convention<AllInterfacesConvention>();;
            });
        }
    }
    
    public class AllInterfacesConvention : IRegistrationConvention
    {
        public void ScanTypes(TypeSet types, Registry registry)
        {
            // Only work on concrete types
            types.FindTypes(TypeClassification.Concretes | TypeClassification.Closed).Each(type =>
            {
                // Register against all the interfaces implemented
                // by this concrete class
                type.GetInterfaces().Each(@interface => registry.For(@interface).Use(type));
            });
        }
    }
}
