
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
using Business.Handlers.WashingControll_Floors.ValidationRules;

namespace Business.Handlers.WashingControll_Floors.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateWashingControll_FloorCommand : WashingControll_Floor, IRequest<IResult>
    {




        public class CreateWashingControll_FloorCommandHandler : IRequestHandler<CreateWashingControll_FloorCommand, IResult>
        {
            private readonly IWashingControll_FloorRepository _washingControll_FloorRepository;
            private readonly IMediator _mediator;
            public CreateWashingControll_FloorCommandHandler(IWashingControll_FloorRepository washingControll_FloorRepository, IMediator mediator)
            {
                _washingControll_FloorRepository = washingControll_FloorRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateWashingControll_FloorValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateWashingControll_FloorCommand request, CancellationToken cancellationToken)
            {
             

                var addedWashingControll_Floor = new WashingControll_Floor
                {
                    CreatedUserId = request.CreatedUserId,
                    CreatedDate = request.CreatedDate,
                    Status = request.Status,
                    isDeleted = false,
                    OrderName = request.OrderName,
                    MachineType = request.MachineType,
                    MachineEmployee = request.MachineEmployee,
                    ManagerName = request.ManagerName,
                    SumProductAmount = request.SumProductAmount,
                    BrendaNumber = request.BrendaNumber,
                    jobRotation = request.jobRotation,

                };

                _washingControll_FloorRepository.Add(addedWashingControll_Floor);
                await _washingControll_FloorRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}