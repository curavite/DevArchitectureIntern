﻿
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;


namespace Business.Handlers.FloorControllErrors.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteFloorControllErrorCommand : IRequest<IResult>
    {
        public int Id { get; set; }

        public string ErrorName { get; set; }

        public class DeleteFloorControllErrorCommandHandler : IRequestHandler<DeleteFloorControllErrorCommand, IResult>
        {
            private readonly IFloorControllErrorRepository _floorControllErrorRepository;
            private readonly IMediator _mediator;

            public DeleteFloorControllErrorCommandHandler(IFloorControllErrorRepository floorControllErrorRepository, IMediator mediator)
            {
                _floorControllErrorRepository = floorControllErrorRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteFloorControllErrorCommand request, CancellationToken cancellationToken)
            {
                var deleteFloorError = await _floorControllErrorRepository.Delete2(request.Id,request.ErrorName);

                if (deleteFloorError != null)
                {
                    return new SuccessResult(Messages.Deleted);
                }

                return new ErrorResult("Silenecek ürün bulunamadı");
            }
        }
    }
}

