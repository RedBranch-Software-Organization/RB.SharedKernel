namespace RB.SharedKernel.MediatR.Command;
public interface ICommand : IRequest { }
public interface ICommand<out TCommandResult> : IRequest<TCommandResult>
where TCommandResult : ICommandResult { }