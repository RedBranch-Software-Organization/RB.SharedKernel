namespace RB.SharedKernel.MediatR.Command;

public interface ICommandHandler<in TCommand, TCommandResult>
    : IRequestHandler<TCommand, TCommandResult>
    where TCommand : ICommand<TCommandResult>
    where TCommandResult : ICommandResult
{ }

public interface ICommandHandler<in TCommand> 
    : IRequestHandler<TCommand>
    where TCommand : ICommand
{ }
