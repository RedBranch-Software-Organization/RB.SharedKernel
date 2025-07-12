namespace RB.SharedKernel.MediatR.Query;

public interface IQuery : IRequest { }
public interface IQuery<out TQueryResult> : IRequest<TQueryResult>
where TQueryResult : IQueryResult 
 { }
