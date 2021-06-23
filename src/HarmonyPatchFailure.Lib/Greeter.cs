using System.Diagnostics;

namespace HarmonyPatchFailure.Lib
{
    class Greeter
    {
        public static string SayHello(string name)
        {
            var stackTrace = new StackTrace();

            var frame = stackTrace.GetFrame(1);
            var methodBase = frame?.GetMethod();

            if (typeof(Greeter).Assembly != methodBase?.DeclaringType?.Assembly)
            {
                return "Not Patched!";
            }

            return $"Hello {name}!";
        }
    }
}
