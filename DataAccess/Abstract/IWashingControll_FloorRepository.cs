
using System;
using System.Threading.Tasks;
using Core.DataAccess;
using Entities.Concrete;
namespace DataAccess.Abstract
{
    public interface IWashingControll_FloorRepository : IEntityRepository<WashingControll_Floor>
    {
        Task<Boolean> AmountControll(int amount);

    }
}