
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


namespace Business.Handlers.WashingControll_Floors.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteWashingControll_FloorCommand : IRequest<IResult>
    {
        public int Id { get; set; }

        public class DeleteWashingControll_FloorCommandHandler : IRequestHandler<DeleteWashingControll_FloorCommand, IResult>
        {
            private readonly IWashingControll_FloorRepository _washingControll_FloorRepository;
            private readonly IMediator _mediator;

            public DeleteWashingControll_FloorCommandHandler(IWashingControll_FloorRepository washingControll_FloorRepository, IMediator mediator)
            {
                _washingControll_FloorRepository = washingControll_FloorRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteWashingControll_FloorCommand request, CancellationToken cancellationToken)
            {
                var washingControll_FloorToDelete = _washingControll_FloorRepository.Get(p => p.Id == request.Id);

                _washingControll_FloorRepository.Delete(washingControll_FloorToDelete);
                await _washingControll_FloorRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

