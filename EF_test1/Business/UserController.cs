using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EF_test1.Models;
using System.Data.Objects;

namespace EF_test1.Business
{
    public class UserController
    {
        protected List<users> obj;

        public UserController()
        { }
        public string print()
        {
            StringBuilder sb = new StringBuilder();
            //PermissionDBEntities is entity factory ,object-myusers can be created by PermissionDBEntities with dynmaic
            using (var ctx = new PermissionDBEntities())
            {
                ObjectQuery<users> myuser = ctx.users;
                foreach (users u in myuser) {
                    sb.AppendLine("id:" + u.userid + "\tname:" + u.name + "\tusername:" + u.username);
                }
            }
            return sb.ToString();
        }
        public string print(IList<users> myuser)
        {
            StringBuilder sb = new StringBuilder();
            foreach (users u in myuser)
            {
                sb.AppendLine("id:" + u.userid + "\tname:" + u.name + "\tusername:" + u.username);
            } 
            return sb.ToString();
        }
        public string print(users u)
        {
            return "id:" + u.userid + "\tname:" + u.name + "\tusername:" + u.username;
        }
        public void Insert()
        {
            //create an instance belong to users
            var myusers = new users() { 
                userid=Guid.NewGuid().ToString(),
                username="mike",
                passwordword="123",
                name="张伟2",
                sex="男"            };
            //PermissionDBEntities is entity factory ,object-myusers can be created by PermissionDBEntities with dynmaic
            using (var ctx = new PermissionDBEntities()) 
            {
                ctx.users.AddObject(myusers); 
                ctx.SaveChanges(); 
            }
        }
        public void Insert2()
        {
            //create an instance belong to users
            var myusers = new users()
            {
                userid = Guid.NewGuid().ToString(),
                username = "mike",
                passwordword = "123",
                name = "张伟2",
                sex = "男"
            };
            BusinessController contoller = new BusinessController();
            contoller.Insert<users>(myusers);
        }

        public void Modify(string id)
        {
            using (var ctx = new PermissionDBEntities())
            {
                users myusers = ctx.users.FirstOrDefault(u => u.userid == id);
                myusers.username = "keily_m";
                ctx.ApplyCurrentValues("users", myusers);
                int intAffected = ctx.SaveChanges();
                ctx.AcceptAllChanges();
            }
        }
        public void Delete2(string uid)
        {
            BusinessController contoller = new BusinessController();
            contoller.Delete<users>(id: uid, name: "userid");
        }
    }
}
