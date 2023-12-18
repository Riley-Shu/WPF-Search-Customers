using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Sample06_WPF.Model
{
    public class Customers
    {
        public String customerId { get; set; }
        public String customerName { get; set; }
        public String areaId { get; set; }
        public String area { get; set; }
        public String company { get; set; }
        public String contact { get; set; }
        public String phone { get; set; }
        public String adress { get; set; }
        public String salesRep { get; set; }
        public String ps { get; set; }

        public override string ToString() 
        {
            return this.customerName;
        }
    }
}
