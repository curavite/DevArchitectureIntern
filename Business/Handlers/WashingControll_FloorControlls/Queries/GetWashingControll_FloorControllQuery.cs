
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.WashingControll_FloorControlls.Queries
{
    public class GetWashingControll_FloorControllQuery : IRequest<IDataResult<WashingControll_FloorControll>>
    {
        public int Id { get; set; }

        public class GetWashingControll_FloorControllQueryHandler : IRequestHandler<GetWashingControll_FloorControllQuery, IDataResult<WashingControll_FloorControll>>
        {
            private readonly IWashingControll_FloorControllRepository _washingControll_FloorControllRepository;
            private readonly IMediator _mediator;

            public GetWashingControll_FloorControllQueryHandler(IWashingControll_FloorControllRepository washingControll_FloorControllRepository, IMediator mediator)
            {
                _washingControll_FloorControllRepository = washingControll_FloorControllRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<WashingControll_FloorControll>> Handle(GetWashingControll_FloorControllQuery request, CancellationToken cancellationToken)
            {
                var washingControll_FloorControll = await _washingControll_FloorControllRepository.GetAsync(p => p.Id == request.Id);
                return new SuccessDataResult<WashingControll_FloorControll>(washingControll_FloorControll);
            }
        }
    }
}
