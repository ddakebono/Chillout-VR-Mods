using HarmonyLib;
using MelonLoader;
using Object = UnityEngine.Object;

namespace NoMirrors
{
    
    /// <summary>
    /// Reworked mod to use HarmonyX annotations, switched to removing only the mirror component itself instead of deleting the entire GameObject as that could cause unintended issues.
    /// </summary>
    
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
