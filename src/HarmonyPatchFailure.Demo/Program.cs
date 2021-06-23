using System;
using System.IO;
using System.Linq;
using System.Reflection;
using dnlib.DotNet;

namespace HarmonyPatchFailure.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            var libraryName = "HarmonyPatchFailure.Lib.dll";
            var directoryPath = Directory.GetCurrentDirectory();
            var libraryPath = Path.Combine(directoryPath, libraryName);

            StackFramePatcher.Patch();

            var module = ModuleDefMD.Load(libraryPath);
            var assembly = Assembly.LoadFile(libraryPath);

            var sayHelloMethod = Utils.GetMethodsRecursive(module).FirstOrDefault(m => m.Name == "SayHello");

            if (sayHelloMethod == null)
                throw new Exception("Could not find 'SayHello' method in 'HarmonyPatchFailure.Lib.dll'");

            var sayHello = Utils.FindMethod(assembly, sayHelloMethod, new[] { typeof(string) });

            var result = (string)sayHello.Invoke(null, new object[] {"World"});

            Console.WriteLine(result);
            Console.ReadKey();
        }
    }
}
