namespace RB.SharedKernel.MediatR.Command;

public interface ICommand<out TCommandResponse> : IRequest<TCommandResponse>
    where TCommandResponse : ICommandResponse
{ }

public interface ICommand : IRequest 
{ }