
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
using Entities.Concrete;

namespace Business.Handlers.Machines.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteMachineCommand :Machine, IRequest<IResult>
    {

        public class DeleteMachineCommandHandler : IRequestHandler<DeleteMachineCommand, IResult>
        {
            private readonly IMachineRepository _machineRepository;
            private readonly IMediator _mediator;

            public DeleteMachineCommandHandler(IMachineRepository machineRepository, IMediator mediator)
            {
                _machineRepository = machineRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteMachineCommand request, CancellationToken cancellationToken)
            {
                var machineToDelete = _machineRepository.Get(p => p.Id == request.Id);

                _machineRepository.Delete(machineToDelete);
                await _machineRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

