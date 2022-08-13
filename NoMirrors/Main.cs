using HarmonyLib;
using MelonLoader;
using Object = UnityEngine.Object;

namespace NoMirrors
{
    public class Main : MelonMod
    {

    }

    [HarmonyPatch(typeof(CVRMirror))]
    class MirrorPatch
    {
        [HarmonyPatch("Start")]
        [HarmonyPostfix]
        static void MirrorStart(CVRMirror __instance)
        {
            Object.DestroyImmediate(__instance);
        }
    }
}
