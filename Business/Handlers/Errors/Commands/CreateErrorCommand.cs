
using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Business.Handlers.Errors.ValidationRules;
using System;

namespace Business.Handlers.Errors.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateErrorCommand : Error, IRequest<IResult>
    {



        public class CreateErrorCommandHandler : IRequestHandler<CreateErrorCommand, IResult>
        {
            private readonly IErrorRepository _errorRepository;
            private readonly IMediator _mediator;
            public CreateErrorCommandHandler(IErrorRepository errorRepository, IMediator mediator)
            {
                _errorRepository = errorRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateErrorValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateErrorCommand request, CancellationToken cancellationToken)
            {

           

                var addedError = new Error
                {
                    CreatedUserId = request.CreatedUserId,
                    CreatedDate = DateTime.Now,
                    Status = request.Status,
                    isDeleted = request.isDeleted,
                    ErrorName = request.ErrorName,
                    IsError = request.IsError,
                    RowNumber = request.RowNumber,
                    Departmant = request.Departmant,
                    ColorCode = request.ColorCode,

                };

                _errorRepository.Add(addedError);
                await _errorRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}