
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
using Business.Handlers.WashingControll_Floors.ValidationRules;


namespace Business.Handlers.WashingControll_Floors.Commands
{


    public class UpdateWashingControll_FloorCommand : WashingControll_Floor, IRequest<IResult>
    {


        public class UpdateWashingControll_FloorCommandHandler : IRequestHandler<UpdateWashingControll_FloorCommand, IResult>
        {
            private readonly IWashingControll_FloorRepository _washingControll_FloorRepository;
            private readonly IMediator _mediator;

            public UpdateWashingControll_FloorCommandHandler(IWashingControll_FloorRepository washingControll_FloorRepository, IMediator mediator)
            {
                _washingControll_FloorRepository = washingControll_FloorRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateWashingControll_FloorValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateWashingControll_FloorCommand request, CancellationToken cancellationToken)
            {
                var isThereWashingControll_FloorRecord = await _washingControll_FloorRepository.GetAsync(u => u.Id == request.Id);


             
                isThereWashingControll_FloorRecord.LastUpdatedUserId = request.LastUpdatedUserId;
                isThereWashingControll_FloorRecord.LastUpdatedDate = request.LastUpdatedDate;
                isThereWashingControll_FloorRecord.Status = request.Status;
                isThereWashingControll_FloorRecord.isDeleted = false;
                isThereWashingControll_FloorRecord.OrderName = request.OrderName;
                isThereWashingControll_FloorRecord.MachineType = request.MachineType;
                isThereWashingControll_FloorRecord.MachineEmployee = request.MachineEmployee;
                isThereWashingControll_FloorRecord.ManagerName = request.ManagerName;
                isThereWashingControll_FloorRecord.SumProductAmount = request.SumProductAmount;
                isThereWashingControll_FloorRecord.BrendaNumber = request.BrendaNumber;
                isThereWashingControll_FloorRecord.jobRotation = request.jobRotation;


                _washingControll_FloorRepository.Update(isThereWashingControll_FloorRecord);
                await _washingControll_FloorRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

