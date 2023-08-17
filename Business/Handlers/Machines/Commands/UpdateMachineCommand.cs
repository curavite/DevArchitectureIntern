
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
using Business.Handlers.Machines.ValidationRules;
using System;

namespace Business.Handlers.Machines.Commands
{


    public class UpdateMachineCommand : Machine, IRequest<IResult>
    {
   

        public class UpdateMachineCommandHandler : IRequestHandler<UpdateMachineCommand, IResult>
        {
            private readonly IMachineRepository _machineRepository;
            private readonly IMediator _mediator;

            public UpdateMachineCommandHandler(IMachineRepository machineRepository, IMediator mediator)
            {
                _machineRepository = machineRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateMachineValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateMachineCommand request, CancellationToken cancellationToken)
            {
                var isThereMachineRecord = await _machineRepository.GetAsync(u => u.Id == request.Id);


                isThereMachineRecord.LastUpdatedUserId = request.LastUpdatedUserId;
                isThereMachineRecord.LastUpdatedDate =  DateTime.Now;
                isThereMachineRecord.Status = request.Status;
                isThereMachineRecord.isDeleted = false;
                isThereMachineRecord.MachineName = request.MachineName;
                isThereMachineRecord.MachineType = request.MachineType;


                _machineRepository.Update(isThereMachineRecord);
                await _machineRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

