using MediatR;

namespace Core.Validation
{
    public interface IWrappedAsyncRequest<TBody, TResponse> : IAsyncRequest<TResponse>, IHaveABody<TBody>
    {
        
    }
}