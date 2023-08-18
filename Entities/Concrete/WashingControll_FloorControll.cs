using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class WashingControll_FloorControll:BaseEntity,IEntity
    {

        public int Amount { get; set; }

        public int Percent { get; set; }

        public int FaultyProduct { get; set; }

        public int ControllTime { get; set; }

        public string ControllResult { get; set; }

        public string ManagerReview { get; set; }

        public string PercentResult { get; set; }

        public int OrderId { get; set; }




    }
}
