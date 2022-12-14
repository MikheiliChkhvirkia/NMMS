using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NMMS.Common.Application.Behaviors;
using System.Reflection;

namespace NMMS.Common.Application.Tools.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddCommonApplication(this IServiceCollection services, Assembly assembly)
        {
            var currentAssembly = Assembly.GetExecutingAssembly();
            services.AddAutoMapper(currentAssembly);
            services.AddMediatR(assembly);
            services.AddValidatorsFromAssembly(assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        }
    }
}
