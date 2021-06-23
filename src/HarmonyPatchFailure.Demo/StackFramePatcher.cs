using System;
using System.Diagnostics;
using System.Reflection;
using HarmonyLib;

namespace HarmonyPatchFailure.Demo
{
    public static class StackFramePatcher
    {
        private const string HarmonyId = "adstep.harmonypatchfailure.stacktrace";
        private static Harmony _harmony;

        public static void Patch()
        {
            _harmony = new Harmony(HarmonyId);
            _harmony.PatchAll(Assembly.GetExecutingAssembly());
        }

        public static void UnPatch()
        {
            _harmony?.UnpatchAll(_harmony.Id);
            _harmony = null;
        }

        [HarmonyPatch(typeof(StackFrame), "GetMethod")]
        public class PatchStackFrameGetMethod
        {
            public static void Postfix(ref MethodBase __result)
            {
                if (__result.DeclaringType != typeof(RuntimeMethodHandle))
                    return;

                //just replace it with a method
                __result = MethodBase.GetCurrentMethod();
            }
        }
    }
}
