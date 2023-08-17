using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class WashingControll_Floor : BaseEntity,IEntity
    {
        public string OrderName { get; set; }

        public string MachineType { get; set; }

        public string MachineEmployee { get; set; }

        public string ManagerName { get; set; }

        public int SumProductAmount { get; set; }

        public string BrendaNumber { get; set; }

        public string jobRotation { get; set; }

        
    }
}
