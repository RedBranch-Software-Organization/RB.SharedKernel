namespace RB.SharedKernel.MediatR.Command;
public interface ICommand : IRequest { }
public interface ICommand<out TCommandResponse> : IRequest<TCommandResponse>
where TCommandResponse : ICommandResponse 
{ }