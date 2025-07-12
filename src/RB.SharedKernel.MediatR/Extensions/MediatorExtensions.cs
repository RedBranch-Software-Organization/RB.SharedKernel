namespace RB.SharedKernel.MediatR.Extensions;

public static class MediatorExtensions
{
    public static async Task<TResult> SendQueryAsync<TResult>(this IMediator mediator, IQuery<TResult> query) where TResult : IQueryResult
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

    public static async Task<TResult> SendCommandAsync<TResult>(this IMediator mediator, ICommand<TResult> command) where TResult : ICommandResult
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
