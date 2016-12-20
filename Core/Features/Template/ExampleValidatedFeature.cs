using Core.Validation;
using FluentValidation;

namespace Core.Features.Template
{
    using System;
    using MediatR;

    namespace Web.Features.UserManager
    {
        public class ExampleValidatedFeature
        {
       
            public class Command : IWrappedRequest<CommandBody, Result>
            {
                public Guid Id { get; set; }

                public CommandBody Body { get; set; }
            }

            public class CommandBody
            {
                public string Name { get; set; }
            }

            public class CommandValidator : AbstractValidator<Command> 
            {
                public CommandValidator()
                {
                    RuleFor(x => x.Id).NotEqual(Guid.Empty).WithMessage("Id must be a Guid that is not Empty");
                    RuleFor(x => x.Body.Name).NotEmpty().WithMessage("Name can not be empty");
                }
            }

            public class Handler : IRequestHandler<Command, Result>
            {
            
                //Cound inject interfacted here that are defined in Core and implemented in Infrastructure 
                public Handler()
                {
                }

                public Result Handle(Command message)
                {
                    return new Result()
                    {
                        ResultValue = $"{message.Id} ,{message.Body.Name}"
                    };
                }
            }

            public class Result
            {
                public string ResultValue { get; set; }
            }
         
        }
    }

}
