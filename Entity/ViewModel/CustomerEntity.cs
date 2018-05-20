using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.ViewModel
{
    public class CustomerEntity
    {
        public long CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerCode { get; set; }
        public string Password { get; set; }
        public int Channel { get; set; }

        public string Name { get; set; }
        public string Mobile { get; set; }

        public CustomerExtendEntity Detail { get; set; }

    }
}
