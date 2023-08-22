
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

namespace Business.Handlers.FloorControllErrors.Queries
{

    public class GetFloorControllErrorsQuery : IRequest<IDataResult<IEnumerable<FloorControllError>>>
    {
        public class GetFloorControllErrorsQueryHandler : IRequestHandler<GetFloorControllErrorsQuery, IDataResult<IEnumerable<FloorControllError>>>
        {
            private readonly IFloorControllErrorRepository _floorControllErrorRepository;
            private readonly IMediator _mediator;

            public GetFloorControllErrorsQueryHandler(IFloorControllErrorRepository floorControllErrorRepository, IMediator mediator)
            {
                _floorControllErrorRepository = floorControllErrorRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<FloorControllError>>> Handle(GetFloorControllErrorsQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<FloorControllError>>(await _floorControllErrorRepository.GetListAsync());
            }
        }
    }
}