
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Entities.Dtos;

namespace DataAccess.Concrete.EntityFramework
{
    public class FloorControllErrorRepository : EfEntityRepositoryBase<FloorControllError, ProjectDbContext>, IFloorControllErrorRepository
    {
        public FloorControllErrorRepository(ProjectDbContext context) : base(context)
        {
        }

        public async Task<FloorControllError> Delete2(int Id, string ErrorName)
        {
            var floorError = await Context.FloorControllErrors
                .Where(error => error.WSHfloorControllId == Id && error.ErrorName == ErrorName)
                .FirstOrDefaultAsync();

            if (floorError != null)
            {
                floorError.isDeleted = true;
                await Context.SaveChangesAsync();
            }

            return floorError;

        }

        public async Task<IEnumerable<FloorErrorDto>> GetFloorErrorDtos()
        {
            return await (from f in Context.FloorControllErrors
                          join u in Context.Users on f.CreatedUserId equals u.UserId
                          where f.isDeleted == false
                          select new FloorErrorDto
                          {
                              Amount = f.Amount,
                              CreatedDate = f.CreatedDate,
                              ErrorName = f.ErrorName,
                              FullName = u.FullName,
                              WSHfloorControllId = f.WSHfloorControllId
                              

                          }
                         ).ToListAsync();
        }
    }
}
