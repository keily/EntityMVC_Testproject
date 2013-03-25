using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EF_test1.Models;

namespace EF_test1.Business
{
    public class UserController
    {
        protected users obj;

        public UserController()
        { }
        public string print()
        {
            
            return "";
        }
        public void Insert()
        {
            //create an instance belong to users
            var myusers = new users() { 
                userid=Guid.NewGuid().ToString(),
                username="keily",
                passwordword="123",
                name="张伟",
                sex="男"
            };

            //PermissionDBEntities is entity factory ,object-myusers can be created by PermissionDBEntities with dynmaic
            using (var ctx = new PermissionDBEntities()) 
            {
                ctx.users.AddObject(myusers); 
                ctx.SaveChanges(); 
            }
        }
        public void Modify(string name)
        {
            var myusers = new users()
            {
                userid = Guid.NewGuid().ToString(),
                username = "keily",
                passwordword = "123",
                name = "张伟",
                sex = "男"
            };
        }
    }
}
