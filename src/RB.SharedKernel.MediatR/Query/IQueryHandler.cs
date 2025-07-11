namespace RB.SharedKernel.MediatR.Query;

public interface IQueryHandler<in TQuery> : IRequestHandler<TQuery>
    where TQuery : IQuery
{ }
public interface IQueryHandler<in TQuery, TQueryResponse> : IRequestHandler<TQuery, TQueryResponse>
    where TQuery : IQuery<TQueryResponse>
    where TQueryResponse : IQueryResponse  { }
