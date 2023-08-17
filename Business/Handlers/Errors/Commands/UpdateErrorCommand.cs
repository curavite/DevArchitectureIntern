
using Business.Constants;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Core.Aspects.Autofac.Validation;
using Business.Handlers.Errors.ValidationRules;
using System;

namespace Business.Handlers.Errors.Commands
{


    public class UpdateErrorCommand :Error, IRequest<IResult>
    {
    

        public class UpdateErrorCommandHandler : IRequestHandler<UpdateErrorCommand, IResult>
        {
            private readonly IErrorRepository _errorRepository;
            private readonly IMediator _mediator;

            public UpdateErrorCommandHandler(IErrorRepository errorRepository, IMediator mediator)
            {
                _errorRepository = errorRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateErrorValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateErrorCommand request, CancellationToken cancellationToken)
            {
                var isThereErrorRecord = await _errorRepository.GetAsync(u => u.Id == request.Id);


                isThereErrorRecord.LastUpdatedUserId = request.LastUpdatedUserId;
                isThereErrorRecord.LastUpdatedDate = DateTime.Now;
                isThereErrorRecord.Status = request.Status;
                isThereErrorRecord.isDeleted = false;
                isThereErrorRecord.ErrorName = request.ErrorName;
                isThereErrorRecord.IsError = request.IsError;
                isThereErrorRecord.RowNumber = request.RowNumber;
                isThereErrorRecord.Departmant = request.Departmant;
                isThereErrorRecord.ColorCode = request.ColorCode;


                _errorRepository.Update(isThereErrorRecord);
                await _errorRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

