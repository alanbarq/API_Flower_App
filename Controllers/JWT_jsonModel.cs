using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiFlowerTwo.Controllers
{
    public class JWT_jsonModel
    {
        public string User { get; set; }
        public string Token { get; set; }
        public string UserRole { get; set; }
        public int IdUser { get; set; }

        public JWT_jsonModel(string user,string token, string userRole, int idUser)
        {
            this.User = user;
            this.Token = token;
            this.UserRole = userRole;
            this.IdUser = idUser;
        }
    }
}