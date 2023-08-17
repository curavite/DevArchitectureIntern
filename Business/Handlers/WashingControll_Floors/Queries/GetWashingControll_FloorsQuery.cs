
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

namespace Business.Handlers.WashingControll_Floors.Queries
{

    public class GetWashingControll_FloorsQuery : IRequest<IDataResult<IEnumerable<WashingControll_Floor>>>
    {
        public class GetWashingControll_FloorsQueryHandler : IRequestHandler<GetWashingControll_FloorsQuery, IDataResult<IEnumerable<WashingControll_Floor>>>
        {
            private readonly IWashingControll_FloorRepository _washingControll_FloorRepository;
            private readonly IMediator _mediator;

            public GetWashingControll_FloorsQueryHandler(IWashingControll_FloorRepository washingControll_FloorRepository, IMediator mediator)
            {
                _washingControll_FloorRepository = washingControll_FloorRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<WashingControll_Floor>>> Handle(GetWashingControll_FloorsQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<WashingControll_Floor>>(await _washingControll_FloorRepository.GetListAsync());
            }
        }
    }
}