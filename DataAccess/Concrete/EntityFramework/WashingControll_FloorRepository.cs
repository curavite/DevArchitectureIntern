
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class WashingControll_FloorRepository : EfEntityRepositoryBase<WashingControll_Floor, ProjectDbContext>, IWashingControll_FloorRepository
    {
        public WashingControll_FloorRepository(ProjectDbContext context) : base(context)
        {
        }
        public async Task<bool> AmountControll(int amount)
        {
            var isOkey = await Context.WashingControll_Floors.AnyAsync(p => p.SumProductAmount / 10 < amount);
            return isOkey;
        }
    }
}
