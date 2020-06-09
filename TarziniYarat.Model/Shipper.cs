using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarziniYarat.Core.Model;

namespace TarziniYarat.Model
{
    public class Shipper:BaseEntity
    {
        public Shipper()
        {
            Orders = new HashSet<Order>();
        }
        public int ShipperID { get; set; }
        public string CompanyName { get; set; }
        public string Phone { get; set; }

        //Navigation prop
        public ICollection<Order> Orders { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
