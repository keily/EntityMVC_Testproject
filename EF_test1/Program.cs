using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EF_test1.Business;
using EF_test1.Models;
using System.Data.Objects;

namespace EF_test1
{
    class Program
    {
        static void Main(string[] args)
        {
            UserController contoller = new UserController();
            BusinessController contollerCom = new BusinessController();
            
            
            //insert
            string _guid0 = Guid.NewGuid().ToString();
            var myusers = new users(){
                userid = _guid0,username = "0",passwordword = "1",name = "0a",sex = "男"
            };
            Console.WriteLine("after insert:" + _guid0);
            contollerCom.Insert<users>(myusers);
            Console.WriteLine(contoller.print(contollerCom.Get<users>("userid", _guid0)));

            var myuserLists = new List<users>{ 
                new users(){userid = Guid.NewGuid().ToString(),username = "1",passwordword = "1",name = "1a"},
                new users(){userid = Guid.NewGuid().ToString(),username = "2",passwordword = "1",name = "2a"}
            };
            Console.WriteLine("after insert many:");            ;
            Console.WriteLine(contollerCom.InsertList<users>(myuserLists));

            //update
            List<users> mylist = contollerCom.Get<users>(u => u.username == "0");
            var curUser = mylist.First<users>();
            Console.WriteLine("before update:" + curUser.userid);
            Console.WriteLine(contoller.print(curUser));
            curUser.username = "0axiugai";
            contollerCom.Update<users>(curUser, "userid");
            Console.WriteLine("after update:" + curUser.userid);
            Console.WriteLine(contoller.print(contollerCom.Get<users>(u => u.username == "0axiugai")));
            //manual update
            Console.WriteLine("manual update:" + curUser.userid);
            contoller.Modify(curUser.userid);
            Console.WriteLine(contoller.print(contollerCom.Get<users>(u => u.userid == curUser.userid)));
            

            //delete 
            Console.WriteLine("delete all:" + _guid0);
            contollerCom.Delete<users>("userid", _guid0);
            Console.WriteLine(contoller.print(contollerCom.Get<users>("select * from users ")));
            Console.WriteLine("delete u.sex==男:");
            contollerCom.Delete<users>(u=>u.sex=="男");
            Console.WriteLine(contoller.print(contollerCom.Get<users>("select * from users ")));

            //select
            Console.WriteLine("select all:");
            Console.WriteLine(contoller.print());
            Console.WriteLine("select * from users where username='keily'");
            Console.WriteLine(contoller.print(
                contollerCom.Get<users>("select * from users where username='keily'")
                ));
            Console.WriteLine("u=>u.username==1");
            Console.WriteLine(contoller.print(
                contollerCom.Get<users>(u => u.username == "1a")
                ));

            Console.ReadKey();

        }
    }
}
