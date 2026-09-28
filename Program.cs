using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ClearScript;
using Microsoft.ClearScript.JavaScript;
using Microsoft.ClearScript.V8;

namespace MiMFa.Compiler.Environment
{
    static class Program
    {
        /// <summary>W
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(params string[] args)
        {
            var compiler = new DaRQ.Compiler();
            var interpretor = new V8ScriptEngine();
            // expose a host type
            interpretor.AddHostType("Console", typeof(Console));
            Console.WriteLine("MiMFa DaRQ v"+compiler.Version+" Environment");

            do
            {
                var output = compiler.Compile(new MiMFa.Compiler.Input(Console.ReadLine()));
                foreach (var err in output.Errors)
                    Console.WriteLine(err);
                if (!string.IsNullOrWhiteSpace(output.Content)) try
                    {
                        Console.WriteLine(interpretor.Evaluate(output.Content));
                    }
                    catch(Exception ex) { Console.WriteLine(ex.Message); }
            } while (true);
        }
    }
}
