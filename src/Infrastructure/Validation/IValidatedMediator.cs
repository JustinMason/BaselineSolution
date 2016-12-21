using System.Threading.Tasks;
using Core.Validation;
using MediatR;

namespace Infrastructure.Validation
{
    public interface IValidatedMediator
    {
        ValidatedResult<TResponse> Send<TResponse>(IRequest<TResponse> request);
        ValidatedResult<TResponse> Send<TBody, TResponse>(IWrappedRequest<TBody, TResponse> request);
        Task<ValidatedResult<TResponse>> SendAsync<TBody, TResponse>(IWrappedAsyncRequest<TBody, TResponse> request);
        Task<ValidatedResult<TResponse>> SendAsync<TResponse>(IAsyncRequest<TResponse> request);
        Task<ValidatedResult<TResponse>> PublishAsync<TResponse>(IAsyncNotification asynchNotifcation);
    }
}