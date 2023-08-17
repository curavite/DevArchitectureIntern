
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
using Entities.Concrete;

namespace Business.Handlers.Errors.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteErrorCommand :Error, IRequest<IResult>
    {

        public class DeleteErrorCommandHandler : IRequestHandler<DeleteErrorCommand, IResult>
        {
            private readonly IErrorRepository _errorRepository;
            private readonly IMediator _mediator;

            public DeleteErrorCommandHandler(IErrorRepository errorRepository, IMediator mediator)
            {
                _errorRepository = errorRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteErrorCommand request, CancellationToken cancellationToken)
            {
                var errorToDelete = _errorRepository.Get(p => p.Id == request.Id);

                _errorRepository.Delete(errorToDelete);
                await _errorRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

