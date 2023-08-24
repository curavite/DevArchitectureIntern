using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class FloorErrorDto:IDto
    {
        public string FullName { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public string ErrorName { get; set; }

        public int Amount { get; set; }

        public int WSHfloorControllId { get; set; }
    }
}
