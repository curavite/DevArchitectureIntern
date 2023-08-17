
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class WashingControll_FloorControllRepository : EfEntityRepositoryBase<WashingControll_FloorControll, ProjectDbContext>, IWashingControll_FloorControllRepository
    {
        public WashingControll_FloorControllRepository(ProjectDbContext context) : base(context)
        {
           
        }
      

    }
}
