using MediatR;

namespace RB.SharedKernel.MediatR.CQRS
{
    public interface IQueryHandler<in TQuery, TResult> : IRequestHandler<TQuery, TResult>
        where TQuery : IQuery<TResult>
        where TResult : IResult
    {
    }
}
