
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.FloorControllErrors.Queries
{
    public class GetFloorControllErrorQuery : IRequest<IDataResult<FloorControllError>>
    {
        public int Id { get; set; }

        public class GetFloorControllErrorQueryHandler : IRequestHandler<GetFloorControllErrorQuery, IDataResult<FloorControllError>>
        {
            private readonly IFloorControllErrorRepository _floorControllErrorRepository;
            private readonly IMediator _mediator;

            public GetFloorControllErrorQueryHandler(IFloorControllErrorRepository floorControllErrorRepository, IMediator mediator)
            {
                _floorControllErrorRepository = floorControllErrorRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<FloorControllError>> Handle(GetFloorControllErrorQuery request, CancellationToken cancellationToken)
            {
                var floorControllError = await _floorControllErrorRepository.GetAsync(p => p.Id == request.Id);
                return new SuccessDataResult<FloorControllError>(floorControllError);
            }
        }
    }
}
