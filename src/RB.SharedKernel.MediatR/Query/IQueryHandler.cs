namespace RB.SharedKernel.MediatR.Query;

public interface IQueryHandler<in TQuery> : IRequestHandler<TQuery>
    where TQuery : IQuery
{ }
public interface IQueryHandler<in TQuery, TQueryResult> : IRequestHandler<TQuery, TQueryResult>
    where TQuery : IQuery<TQueryResult>
    where TQueryResult : IQueryResult { }
