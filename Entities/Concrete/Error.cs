using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Error:BaseEntity,IEntity
    {
        public string ErrorName { get; set; }

        public bool IsError { get; set; }

        public int RowNumber { get; set; }

        public string Departmant { get; set; }

        public string ColorCode { get; set; }

    }
}
