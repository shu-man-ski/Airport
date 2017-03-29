using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport
{
    class Admin
    {
        public string Login { get; set; }
        public string Password { get; set; }

        public Admin()
        {
            Login = "Admin"; Password = "Admin";
        }
    }
}
