using AutoMapper;
using System.Reflection;

namespace NMMS.Common.Application.Tools.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            ApplyMappingsFromAssembly();
        }

        private void ApplyMappingsFromAssembly()
        {
            IEnumerable<Assembly> assemblies = AppDomain.CurrentDomain.GetAssemblies()
                .Where(assembly => assembly.GetName().Name != Assembly.GetExecutingAssembly().GetName().Name && assembly.GetName().Name.EndsWith(".Application"));

            Type iMapType = typeof(IMap<>);
            string iMapName = nameof(iMapType);
            foreach (var assembly in assemblies)
            {
                List<Type> types = assembly.GetExportedTypes()
                    .Where(type => type.GetInterfaces().Any(interfaceType => interfaceType.IsGenericType && interfaceType.GetGenericTypeDefinition() == iMapType))
                    .ToList();

                foreach (var type in types)
                {
                    MethodInfo methodInfo = type.GetMethod("Mapping") ?? type.GetInterface(iMapName)?.GetMethod("Mapping");

                    methodInfo?.Invoke(Activator.CreateInstance(type), new object[] { this });
                }
            }
        }
    }
}
