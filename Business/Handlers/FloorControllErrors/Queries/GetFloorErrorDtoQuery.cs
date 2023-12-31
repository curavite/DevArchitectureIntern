﻿
using Business.BusinessAspects;
using Core.Utilities.Results;
using Core.Aspects.Autofac.Performance;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Caching;
using Entities.Dtos;

namespace Business.Handlers.FloorControllErrors.Queries
{

    public class GetFloorErrorDtoQuery : IRequest<IDataResult<IEnumerable<FloorErrorDto>>>
    {
        public class GetFloorErrorDtoQueryHandler : IRequestHandler<GetFloorErrorDtoQuery, IDataResult<IEnumerable<FloorErrorDto>>>
        {
            private readonly IFloorControllErrorRepository _floorControllErrorRepository;
            private readonly IMediator _mediator;

            public GetFloorErrorDtoQueryHandler(IFloorControllErrorRepository floorControllErrorRepository, IMediator mediator)
            {
                _floorControllErrorRepository = floorControllErrorRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<FloorErrorDto>>> Handle(GetFloorErrorDtoQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<FloorErrorDto>>(await _floorControllErrorRepository.GetFloorErrorDtos());
            }
        }
    }
}