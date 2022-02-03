using System;
using WebApiFlowerTwo.Controllers;
using WebApiFlowerTwo.Models;
using System.Collections.Generic;
using System.Text;



namespace SpencerService
{
    public class UsersSL
    {
        public UsersController users = new UsersController();
        public List<tbl_users> ListOfUsers(){

            return users.Gettbl_users();

        }

    }
}
