
using Business.Constants;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Core.Aspects.Autofac.Validation;
using Business.Handlers.FloorControllErrors.ValidationRules;


namespace Business.Handlers.FloorControllErrors.Commands
{


    public class UpdateFloorControllErrorCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public int CreatedUserId { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int LastUpdatedUserId { get; set; }
        public System.DateTime LastUpdatedDate { get; set; }
        public bool Status { get; set; }
        public bool isDeleted { get; set; }
        public string ErrorName { get; set; }
        public int Amount { get; set; }
        public int Percent { get; set; }

        public class UpdateFloorControllErrorCommandHandler : IRequestHandler<UpdateFloorControllErrorCommand, IResult>
        {
            private readonly IFloorControllErrorRepository _floorControllErrorRepository;
            private readonly IMediator _mediator;

            public UpdateFloorControllErrorCommandHandler(IFloorControllErrorRepository floorControllErrorRepository, IMediator mediator)
            {
                _floorControllErrorRepository = floorControllErrorRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateFloorControllErrorValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateFloorControllErrorCommand request, CancellationToken cancellationToken)
            {
                var isThereFloorControllErrorRecord = await _floorControllErrorRepository.GetAsync(u => u.Id == request.Id);


                isThereFloorControllErrorRecord.CreatedUserId = request.CreatedUserId;
                isThereFloorControllErrorRecord.CreatedDate = request.CreatedDate;
                isThereFloorControllErrorRecord.LastUpdatedUserId = request.LastUpdatedUserId;
                isThereFloorControllErrorRecord.LastUpdatedDate = request.LastUpdatedDate;
                isThereFloorControllErrorRecord.Status = request.Status;
                isThereFloorControllErrorRecord.isDeleted = request.isDeleted;
                isThereFloorControllErrorRecord.ErrorName = request.ErrorName;
                isThereFloorControllErrorRecord.Amount = request.Amount;
                isThereFloorControllErrorRecord.Percent = request.Percent;


                _floorControllErrorRepository.Update(isThereFloorControllErrorRecord);
                await _floorControllErrorRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

