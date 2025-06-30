using MediatR;

namespace RB.SharedKernel.MediatR.CQRS
{
    public interface IQuery<out TResult> : IRequest<TResult> where TResult : IResult
    {
    }
}
