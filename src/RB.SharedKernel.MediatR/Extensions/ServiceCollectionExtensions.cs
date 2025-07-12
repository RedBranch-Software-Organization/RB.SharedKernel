using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace RB.SharedKernel.MediatR.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddSharedKernelMediatR(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<ICommandHandler<ICommand>>()
                                      .RegisterServicesFromAssemblyContaining<ICommandHandler<ICommand<ICommandResult>, ICommandResult>>()
                                      .RegisterServicesFromAssemblyContaining<IQueryHandler<IQuery>>()
                                      .RegisterServicesFromAssemblyContaining<IQueryHandler<IQuery<IQueryResult>, IQueryResult>>()
                                      .RegisterServicesFromAssembly(Assembly.GetExecutingAssembly())
        );
    }
}
