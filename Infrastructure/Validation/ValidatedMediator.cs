using System.Threading.Tasks;
using Core.Logging;
using Core.Validation;
using Infrastructure.Logging;
using MediatR;

namespace Infrastructure.Validation
{
    public class ValidatedMediator : IValidatedMediator
    {
        private readonly IRequestValidator _requestValidator;
        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public ValidatedMediator(
            IRequestValidator requestValidator,
            IMediator mediator,
            ILogger logger)
        {
            _requestValidator = requestValidator;
            _mediator = mediator;
            _logger = logger;
        }

        public ValidatedResult<TResponse> Send<TResponse>(IRequest<TResponse> request)
        {
            using (LoggerContext.DecorateWithUser())
            {
                _logger.Debug("Send {@request} for {User}", request);
            }

            var validationSummary = _requestValidator.Validate(request);

            return GetValidatedResult(request, validationSummary);
        }

        public async Task<ValidatedResult<TResponse>> SendAsync<TResponse>(IAsyncRequest<TResponse> request)
        {
            using (LoggerContext.DecorateWithUser())
            {
                _logger.Debug("Send {@request} for {User}", request);
            }

            var validationSummary = _requestValidator.Validate(request);

            return await GetValidatedResultAsync(request, validationSummary);
        }


        public ValidatedResult<TResponse> Send<TBody, TResponse>(IWrappedRequest<TBody, TResponse> request)
        {
            using (LoggerContext.DecorateWithUser())
            {
                _logger.Debug("Send {@request} for {User}", request);
            }

            var validationSummary = _requestValidator.Validate(request);

            return GetValidatedResult(request, validationSummary);
        }

        public async Task<ValidatedResult<TResponse>> SendAsync<TBody, TResponse>(IWrappedAsyncRequest<TBody, TResponse> request)
        {
            using (LoggerContext.DecorateWithUser())
            {
                _logger.Debug("Send {@request} for {User}", request);
            }

            var validationSummary = _requestValidator.Validate(request);

            return await GetValidatedResultAsync(request, validationSummary);
        }

        private async Task<ValidatedResult<TResponse>> GetValidatedResultAsync<TResponse>(IAsyncRequest<TResponse> request, ValidationSummary validationSummary)
        {
            var validationResult = new ValidatedResult<TResponse> { ValidationSummary = validationSummary };

            if (validationSummary.IsValid)
            {
                var response = await GetResultAsync(request);
                validationResult.Result = response;
            }

            using (LoggerContext.DecorateWithUser())
            {
                _logger.Debug("Response {@validationResult} for {User}", request);
            }

            return validationResult;
        }

        private ValidatedResult<TResponse> GetValidatedResult<TResponse>(IRequest<TResponse> request, ValidationSummary validationSummary)
        {
            var validationResult = new ValidatedResult<TResponse> { ValidationSummary = validationSummary };

            if (validationSummary.IsValid)
            {
                var response = GetResult(request);
                validationResult.Result = response;
            }

            using (LoggerContext.DecorateWithUser())
            {
                _logger.Debug("Response {@validationResult} for {User}", request);
            }


            return validationResult;
        }

        private async Task<TResponse> GetResultAsync<TResponse>(IAsyncRequest<TResponse> request)
        {
            var result = await _mediator.SendAsync(request);

            return result;
        }

        private TResponse GetResult<TResponse>(IRequest<TResponse> request)
        {
            var result = _mediator.Send(request);

            return result;
        }

        public async Task<ValidatedResult<TResponse>> PublishAsync<TResponse>(IAsyncNotification asynchNotifcation)
        {
            using (LoggerContext.DecorateWithUser())
            {
                _logger.Debug("Publish {@validationResult} for {User}", asynchNotifcation);
            }

            var validationSummary = _requestValidator.Validate(asynchNotifcation);
            var validationResult = new ValidatedResult<TResponse> { ValidationSummary = validationSummary };

            if (validationSummary.IsValid)
            {
                await _mediator.PublishAsync(asynchNotifcation);
            }

            return  await Task.FromResult(validationResult);
        }
    }
}