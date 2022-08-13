using ABI_RC.Helpers;
using MelonLoader;
using System.Collections;
using HarmonyLib;

namespace NoDiscordRPC
{
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
