
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.WashingControll_Floors.Queries
{
    public class GetWashingControll_FloorQuery : IRequest<IDataResult<WashingControll_Floor>>
    {
        public int Id { get; set; }

        public class GetWashingControll_FloorQueryHandler : IRequestHandler<GetWashingControll_FloorQuery, IDataResult<WashingControll_Floor>>
        {
            private readonly IWashingControll_FloorRepository _washingControll_FloorRepository;
            private readonly IMediator _mediator;

            public GetWashingControll_FloorQueryHandler(IWashingControll_FloorRepository washingControll_FloorRepository, IMediator mediator)
            {
                _washingControll_FloorRepository = washingControll_FloorRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<WashingControll_Floor>> Handle(GetWashingControll_FloorQuery request, CancellationToken cancellationToken)
            {
                var washingControll_Floor = await _washingControll_FloorRepository.GetAsync(p => p.Id == request.Id);
                return new SuccessDataResult<WashingControll_Floor>(washingControll_Floor);
            }
        }
    }
}
