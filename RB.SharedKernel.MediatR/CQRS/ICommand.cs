using MediatR;

namespace RB.SharedKernel.MediatR.CQRS
{
    public interface ICommand : IRequest
    {
    }

    public interface ICommand<out TResult> : IRequest<TResult> where TResult : IResult
    {
    }
}
