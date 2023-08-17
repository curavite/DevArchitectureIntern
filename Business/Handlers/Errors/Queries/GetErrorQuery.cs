
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.Errors.Queries
{
    public class GetErrorQuery : IRequest<IDataResult<Error>>
    {
        public int Id { get; set; }

        public class GetErrorQueryHandler : IRequestHandler<GetErrorQuery, IDataResult<Error>>
        {
            private readonly IErrorRepository _errorRepository;
            private readonly IMediator _mediator;

            public GetErrorQueryHandler(IErrorRepository errorRepository, IMediator mediator)
            {
                _errorRepository = errorRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<Error>> Handle(GetErrorQuery request, CancellationToken cancellationToken)
            {
                var error = await _errorRepository.GetAsync(p => p.Id == request.Id);
                return new SuccessDataResult<Error>(error);
            }
        }
    }
}
