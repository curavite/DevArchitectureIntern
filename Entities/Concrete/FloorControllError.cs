using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class FloorControllError:BaseEntity,IEntity
    {
        public string ErrorName { get; set; }

        public int Amount { get; set; }

        public int Percent { get; set; }
    }
}
