# Harmony Patch Failure

Repro for a bug I'm seeing using the Harmony library where the applied patches are silently failing.

Through some investigation, I've been able to discover that manually including the ```MonoMod.Common``` repro resolves the issue.

There are 3 projects included in the project:

1. ```HarmonyPatchFailure.Lib``` contains a class to be patched.
2. ```HarmonyPatchFailure.Demo``` is a console application that depends on the public NuGet version and demonstrates the failure.
3. ```HarmonyPatchFailure.DemoFixed``` is another console application that depends on the public NuGet version, but has the patch applied, and works properly.