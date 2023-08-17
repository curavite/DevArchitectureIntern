using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Machine:BaseEntity,IEntity
    {
        public string MachineName { get; set; }

        public string MachineType { get; set; }


    }
}
