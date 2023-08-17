using Core.Entities;
using Org.BouncyCastle.Asn1.X500;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Entities.Concrete
{
    public class Order : BaseEntity, IEntity
    {
      

        public string OrderNumber { get; set; }

        public string OrderModelName { get; set; }

        public string OrderMaterialName { get; set; }
    }
}

