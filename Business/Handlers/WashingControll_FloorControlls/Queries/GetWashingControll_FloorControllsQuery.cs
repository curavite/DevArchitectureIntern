
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

namespace Business.Handlers.WashingControll_FloorControlls.Queries
{

    public class GetWashingControll_FloorControllsQuery : IRequest<IDataResult<IEnumerable<WashingControll_FloorControll>>>
    {
        public class GetWashingControll_FloorControllsQueryHandler : IRequestHandler<GetWashingControll_FloorControllsQuery, IDataResult<IEnumerable<WashingControll_FloorControll>>>
        {
            private readonly IWashingControll_FloorControllRepository _washingControll_FloorControllRepository;
            private readonly IMediator _mediator;

            public GetWashingControll_FloorControllsQueryHandler(IWashingControll_FloorControllRepository washingControll_FloorControllRepository, IMediator mediator)
            {
                _washingControll_FloorControllRepository = washingControll_FloorControllRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<WashingControll_FloorControll>>> Handle(GetWashingControll_FloorControllsQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<WashingControll_FloorControll>>(await _washingControll_FloorControllRepository.GetListAsync());
            }
        }
    }
}