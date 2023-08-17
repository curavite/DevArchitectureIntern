
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
using DataAccess.Concrete.EntityFramework;
using static Business.Handlers.WashingControll_Floors.Queries.AmountControllQuery;
using Business.Handlers.WashingControll_Floors.Queries;

namespace Business.Handlers.WashingControll_Floors.Queries
{

    public class AmountControllQuery : IRequest<IDataResult<bool>>
    {
        public int ProductId { get; set; }
        public int Amount { get; set; }
        public class AmountControllQueryHandler : IRequestHandler<AmountControllQuery, IDataResult<bool>>
        {

            private readonly IWashingControll_FloorRepository _washingControll_FloorRepository;
            private readonly IMediator _mediator;

            public AmountControllQueryHandler(IWashingControll_FloorRepository washingControll_FloorRepository, IMediator mediator)
            {
                _washingControll_FloorRepository = washingControll_FloorRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<bool>> Handle(AmountControllQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<bool>(await _washingControll_FloorRepository.AmountControll(request.Amount));

            }
        }
    }

}

