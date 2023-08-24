
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.DataAccess;
using Entities.Concrete;
using Entities.Dtos;

namespace DataAccess.Abstract
{
    public interface IFloorControllErrorRepository : IEntityRepository<FloorControllError>
    {
        Task<FloorControllError> Delete2(int Id,string ErrorName);

        Task<IEnumerable<FloorErrorDto>> GetFloorErrorDtos();


    }
}