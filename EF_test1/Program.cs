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
            Console.WriteLine("insert:");
            contoller.Insert();
            Console.WriteLine(contoller.print());
            Console.WriteLine("delete:");
            contoller.Delete2("4B407F2D-CC6D-4467-833B-4DAE2A7A0293");
            Console.WriteLine(contoller.print());
            Console.ReadKey();
        }
    }
}
