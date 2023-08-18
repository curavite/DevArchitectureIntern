
using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Business.Handlers.WashingControll_FloorControlls.ValidationRules;
using System;
using Business.Handlers.WashingControll_Floors.Queries;
using ServiceStack;

namespace Business.Handlers.WashingControll_FloorControlls.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateWashingControll_FloorControllCommand : WashingControll_FloorControll, IRequest<IResult>
    {

     


        public class CreateWashingControll_FloorControllCommandHandler : IRequestHandler<CreateWashingControll_FloorControllCommand, IResult>
        {
            private readonly IWashingControll_FloorControllRepository _washingControll_FloorControllRepository;
            private readonly IMediator _mediator;
            public CreateWashingControll_FloorControllCommandHandler(IWashingControll_FloorControllRepository washingControll_FloorControllRepository, IMediator mediator)
            {
                _washingControll_FloorControllRepository = washingControll_FloorControllRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateWashingControll_FloorControllValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateWashingControll_FloorControllCommand request, CancellationToken cancellationToken)
            {
               
                var amountControll = await _mediator.Send(new AmountControllQuery { Amount=request.Amount,OrderId=request.OrderId});

                if (amountControll.Data == false)
                {
                    return new ErrorResult(Messages.AmountError);
                }

                var addedWashingControll_FloorControll = new WashingControll_FloorControll
                {
                    CreatedUserId = request.CreatedUserId,
                    CreatedDate = DateTime.Now,
                    Status = request.Status,
                    isDeleted = false,
                    Amount = request.Amount,
                    Percent = request.Percent,
                    FaultyProduct = request.FaultyProduct,
                    ControllTime = request.ControllTime,
                    ControllResult = request.ControllResult,
                    ManagerReview = request.ManagerReview,
                    OrderId=request.OrderId,

                };
                if (request.Percent >= 15)
                {
                    addedWashingControll_FloorControll.PercentResult = "Kaldı";
                }
                else
                {
                    addedWashingControll_FloorControll.PercentResult = "Geçti";


                }

                _washingControll_FloorControllRepository.Add(addedWashingControll_FloorControll);
                await _washingControll_FloorControllRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}