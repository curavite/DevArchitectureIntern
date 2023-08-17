
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


namespace Business.Handlers.WashingControll_FloorControlls.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteWashingControll_FloorControllCommand : IRequest<IResult>
    {
        public int Id { get; set; }

        public class DeleteWashingControll_FloorControllCommandHandler : IRequestHandler<DeleteWashingControll_FloorControllCommand, IResult>
        {
            private readonly IWashingControll_FloorControllRepository _washingControll_FloorControllRepository;
            private readonly IMediator _mediator;

            public DeleteWashingControll_FloorControllCommandHandler(IWashingControll_FloorControllRepository washingControll_FloorControllRepository, IMediator mediator)
            {
                _washingControll_FloorControllRepository = washingControll_FloorControllRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteWashingControll_FloorControllCommand request, CancellationToken cancellationToken)
            {
                var washingControll_FloorControllToDelete = _washingControll_FloorControllRepository.Get(p => p.Id == request.Id);

                _washingControll_FloorControllRepository.Delete(washingControll_FloorControllToDelete);
                await _washingControll_FloorControllRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

