
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

namespace Business.Handlers.Errors.Queries
{

    public class GetErrorsQuery : IRequest<IDataResult<IEnumerable<Error>>>
    {
        public class GetErrorsQueryHandler : IRequestHandler<GetErrorsQuery, IDataResult<IEnumerable<Error>>>
        {
            private readonly IErrorRepository _errorRepository;
            private readonly IMediator _mediator;

            public GetErrorsQueryHandler(IErrorRepository errorRepository, IMediator mediator)
            {
                _errorRepository = errorRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<Error>>> Handle(GetErrorsQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<Error>>(await _errorRepository.GetListAsync());
            }
        }
    }
}