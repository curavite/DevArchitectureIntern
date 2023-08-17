
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class MachineRepository : EfEntityRepositoryBase<Machine, ProjectDbContext>, IMachineRepository
    {
        public MachineRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
