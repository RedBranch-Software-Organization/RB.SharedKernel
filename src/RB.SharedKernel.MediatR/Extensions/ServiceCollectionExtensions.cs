using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace RB.SharedKernel.MediatR.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddSharedKernelMediatR(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
    }
}
