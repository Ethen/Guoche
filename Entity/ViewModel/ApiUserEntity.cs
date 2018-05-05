using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.ViewModel
{
    [Serializable]
    public class ApiUserEntity
    {
        public bool result { get; set; }
        public string token { get; set; }

        public string message { get; set; }
        public string vcode { get; set; }

        public CustomerEntity customerEntity { get; set; }
    }
}
