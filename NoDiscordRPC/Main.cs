using ABI_RC.Helpers;
using MelonLoader;
using System.Collections;
using HarmonyLib;

namespace NoDiscordRPC
{
    /// <summary>
    /// Reworked mod to use HarmonyX annotations, removed coroutine usage and instead hooked methods that shouldn't be used.
    /// </summary>
    
    public class Main : MelonMod
    {
    }

    [HarmonyPatch(typeof(PresenceManager))]
    class PresenceDisable
    {
        [HarmonyPrefix]
        [HarmonyPatch("OnEnable")]
        [HarmonyPatch(nameof(PresenceManager.UpdatePresence))]
        static bool GoAway()
        {
            return false;
        }
    }
}
