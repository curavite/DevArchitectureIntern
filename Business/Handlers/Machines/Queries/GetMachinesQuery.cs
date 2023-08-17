
using Business.BusinessAspects;
using Core.Aspects.Autofac.Performance;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Aspects.Autofac.Caching;

namespace Business.Handlers.Machines.Queries
{

    public class GetMachinesQuery : IRequest<IDataResult<IEnumerable<Machine>>>
    {
        public class GetMachinesQueryHandler : IRequestHandler<GetMachinesQuery, IDataResult<IEnumerable<Machine>>>
        {
            private readonly IMachineRepository _machineRepository;
            private readonly IMediator _mediator;

            public GetMachinesQueryHandler(IMachineRepository machineRepository, IMediator mediator)
            {
                _machineRepository = machineRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<Machine>>> Handle(GetMachinesQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<Machine>>(await _machineRepository.GetListAsync());
            }
        }
    }
}