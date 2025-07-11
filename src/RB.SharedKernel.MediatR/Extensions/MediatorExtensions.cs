namespace RB.SharedKernel.MediatR.Extensions;

public static class MediatorExtensions
{
    public static async Task<TResponse> SendQueryAsync<TResponse>(this IMediator mediator, IQuery<TResponse> query) where TResponse : ICommandResponse
    {
        ArgumentNullException.ThrowIfNull(mediator);
        ArgumentNullException.ThrowIfNull(query);
        return await mediator.Send(query);
    }

    public static async Task SendQueryAsync(this IMediator mediator, IQuery query)
    {
        ArgumentNullException.ThrowIfNull(mediator);
        ArgumentNullException.ThrowIfNull(query);
        await mediator.Send(query);
    }

    public static async Task<TResponse> SendCommandAsync<TResponse>(this IMediator mediator, ICommand<TResponse> command) where TResponse : ICommandResponse
    {
        ArgumentNullException.ThrowIfNull(mediator);
        ArgumentNullException.ThrowIfNull(command);
        return await mediator.Send(command);
    }

    public static async Task SendCommandAsync(this IMediator mediator, ICommand command)
    {
        ArgumentNullException.ThrowIfNull(mediator);
        ArgumentNullException.ThrowIfNull(command);
        await mediator.Send(command);
    }


}
