namespace RB.SharedKernel.MediatR.Query;

public interface IQuery : IRequest { }
public interface IQuery<out TQueryResponse> : IRequest<TQueryResponse>
where TQueryResponse : ICommandResponse 
 { }
