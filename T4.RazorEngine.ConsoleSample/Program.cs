using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RazorEngine;
using RazorEngine.Templating;
namespace T4.RazorEngine.ConsoleSample
{
    class Program
    {
        static void Main(string[] args)
        {
            string template = "Hello @Model.Name, World";
            string result = Engine.Razor.RunCompile(template,"1", null, new { Name = "wangze" });
            Console.WriteLine(result);
            Console.ReadKey();
        }
    }
}
