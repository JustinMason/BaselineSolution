using MediatR;

namespace Core.Validation
{
    public interface IWrappedRequest<TBody, TResponse> : IRequest<TResponse>, IHaveABody<TBody>
    {
    }
}