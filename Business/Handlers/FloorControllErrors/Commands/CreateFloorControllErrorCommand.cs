
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
using Business.Handlers.FloorControllErrors.ValidationRules;
using System;
using Business.Handlers.WashingControll_FloorControlls.Queries;

namespace Business.Handlers.FloorControllErrors.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateFloorControllErrorCommand : FloorControllError,IRequest<IResult>
    {

     


        public class CreateFloorControllErrorCommandHandler : IRequestHandler<CreateFloorControllErrorCommand, IResult>
        {
            private readonly IFloorControllErrorRepository _floorControllErrorRepository;
            private readonly IMediator _mediator;
            public CreateFloorControllErrorCommandHandler(IFloorControllErrorRepository floorControllErrorRepository, IMediator mediator)
            {
                _floorControllErrorRepository = floorControllErrorRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateFloorControllErrorValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateFloorControllErrorCommand request, CancellationToken cancellationToken)
            {

                var isErrorNameExist =  _floorControllErrorRepository.Query().Any(u=>u.ErrorName == request.ErrorName && u.WSHfloorControllId == request.WSHfloorControllId);
                if (isErrorNameExist==true)
                {
                    var isThereFloorControllErrorRecord = await _floorControllErrorRepository.GetAsync(u=> u.WSHfloorControllId==request.WSHfloorControllId &&u.ErrorName==request.ErrorName);

                    isThereFloorControllErrorRecord.Amount++;
                    
                    _floorControllErrorRepository.Update(isThereFloorControllErrorRecord);
                    await _floorControllErrorRepository.SaveChangesAsync();
                    return new SuccessResult("Güncellendi");


                }

                //var allFloorControll = await _mediator.Send(new GetWashingControll_FloorControllsQuery());
                //var maxFloorControllId = allFloorControll.Data.Max(u => u.Id);
                //var newFloorControllId = maxFloorControllId + 1;


                var addedFloorControllError = new FloorControllError
                {
                    CreatedUserId = request.CreatedUserId,
                    CreatedDate = DateTime.Now,
                    Status = true,
                    isDeleted = false,
                    ErrorName = request.ErrorName,
                    Amount = request.Amount,
                    WSHfloorControllId = request.WSHfloorControllId,

                };

                _floorControllErrorRepository.Add(addedFloorControllError);
                await _floorControllErrorRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}