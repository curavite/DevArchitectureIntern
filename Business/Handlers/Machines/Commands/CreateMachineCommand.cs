
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
using Business.Handlers.Machines.ValidationRules;
using System;

namespace Business.Handlers.Machines.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateMachineCommand :Machine, IRequest<IResult>
    {

  

        public class CreateMachineCommandHandler : IRequestHandler<CreateMachineCommand, IResult>
        {
            private readonly IMachineRepository _machineRepository;
            private readonly IMediator _mediator;
            public CreateMachineCommandHandler(IMachineRepository machineRepository, IMediator mediator)
            {
                _machineRepository = machineRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateMachineValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateMachineCommand request, CancellationToken cancellationToken)
            {
              

                var addedMachine = new Machine
                {
                    CreatedUserId = request.CreatedUserId,
                    CreatedDate = DateTime.Now,
                    Status = request.Status,
                    isDeleted = false,
                    MachineName = request.MachineName,
                    MachineType = request.MachineType,

                };

                _machineRepository.Add(addedMachine);
                await _machineRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}