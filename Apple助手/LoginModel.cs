using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apple
{
   public class LoginModel
    {
        public LoginModel() { }
        public LoginModel(string appleId,string pwd) {
            this.accountName = appleId;
            this.password = pwd;
            this.rememberMe = false;
        }

        public string accountName { get; set; }
        public string password { get; set; }
        public bool rememberMe { get; set; }
    }
}
