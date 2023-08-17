
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.Machines.Queries
{
    public class GetMachineQuery : IRequest<IDataResult<Machine>>
    {
        public int Id { get; set; }

        public class GetMachineQueryHandler : IRequestHandler<GetMachineQuery, IDataResult<Machine>>
        {
            private readonly IMachineRepository _machineRepository;
            private readonly IMediator _mediator;

            public GetMachineQueryHandler(IMachineRepository machineRepository, IMediator mediator)
            {
                _machineRepository = machineRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<Machine>> Handle(GetMachineQuery request, CancellationToken cancellationToken)
            {
                var machine = await _machineRepository.GetAsync(p => p.Id == request.Id);
                return new SuccessDataResult<Machine>(machine);
            }
        }
    }
}
