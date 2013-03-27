using System;
using IronPython.Hosting;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            var engine = Python.CreateEngine();
            var scope = engine.CreateScope();
            var source = engine.CreateScriptSourceFromString(
                "def adder(arg1, arg2):\n" +
                "   return arg1 + arg2\n" +
                "\n" +
                "class MyClass(object):\n" +
                "   def __init__(self, value):\n" +
                "       self.value = value\n");
            source.Execute(scope);
            
            var adder = scope.GetVariable<Func<object, object, object>>("adder");
            Console.WriteLine(adder(2, 2));
            Console.WriteLine(adder(2.0, 2.5));

            var myClass = scope.GetVariable<Func<object, object>>("MyClass");
            var myInstance = myClass("hello");

            Console.WriteLine(engine.Operations.GetMember(myInstance, "value"));

            Console.ReadLine();
        }
    }

}
