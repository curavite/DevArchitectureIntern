
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
using Business.Handlers.WashingControll_FloorControlls.ValidationRules;


namespace Business.Handlers.WashingControll_FloorControlls.Commands
{


    public class UpdateWashingControll_FloorControllCommand : WashingControll_FloorControll, IRequest<IResult>
    {
        
        public class UpdateWashingControll_FloorControllCommandHandler : IRequestHandler<UpdateWashingControll_FloorControllCommand, IResult>
        {
            private readonly IWashingControll_FloorControllRepository _washingControll_FloorControllRepository;
            private readonly IMediator _mediator;

            public UpdateWashingControll_FloorControllCommandHandler(IWashingControll_FloorControllRepository washingControll_FloorControllRepository, IMediator mediator)
            {
                _washingControll_FloorControllRepository = washingControll_FloorControllRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateWashingControll_FloorControllValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateWashingControll_FloorControllCommand request, CancellationToken cancellationToken)
            {
                var isThereWashingControll_FloorControllRecord = await _washingControll_FloorControllRepository.GetAsync(u => u.Id == request.Id);


                isThereWashingControll_FloorControllRecord.CreatedUserId = request.CreatedUserId;
                isThereWashingControll_FloorControllRecord.CreatedDate = request.CreatedDate;
                isThereWashingControll_FloorControllRecord.LastUpdatedUserId = request.LastUpdatedUserId;
                isThereWashingControll_FloorControllRecord.LastUpdatedDate = request.LastUpdatedDate;
                isThereWashingControll_FloorControllRecord.Status = request.Status;
                isThereWashingControll_FloorControllRecord.isDeleted = request.isDeleted;
                isThereWashingControll_FloorControllRecord.Amount = request.Amount;
                isThereWashingControll_FloorControllRecord.Percent = request.Percent;
                isThereWashingControll_FloorControllRecord.FaultyProduct = request.FaultyProduct;
                isThereWashingControll_FloorControllRecord.ControllTime = request.ControllTime;
                isThereWashingControll_FloorControllRecord.ControllResult = request.ControllResult;
                isThereWashingControll_FloorControllRecord.ManagerReview = request.ManagerReview;
                isThereWashingControll_FloorControllRecord.PercentResult = request.PercentResult;


                _washingControll_FloorControllRepository.Update(isThereWashingControll_FloorControllRecord);
                await _washingControll_FloorControllRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

