using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EF_test1.Business;

namespace EF_test1
{
    class Program
    {
        static void Main(string[] args)
        {
            UserController contoller = new UserController();
            //insert 
            contoller.Insert();

        }
    }
}
