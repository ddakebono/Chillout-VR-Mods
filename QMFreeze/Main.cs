using ABI_RC.Core.InteractionSystem;
using ABI_RC.Systems.Movement;
using MelonLoader;
using HarmonyLib;

namespace QMFreeze
{
    
    /// <summary>
    /// Reworked entire mod to use HarmonyX annotations and avoid causing issues with other mods
    /// </summary>
    
    public class Main : MelonMod
    {

    }
    
    [HarmonyPatch]
    class MakeThingWork
    {
        [HarmonyPostfix]
        [HarmonyPatch(typeof(CVR_MenuManager), nameof(CVR_MenuManager.ToggleQuickMenu))]
        [HarmonyPatch(typeof(ViewManager), nameof(ViewManager.UiStateToggle), typeof(bool))]
        static void UIToggle(bool __0)
        {
            if (BetterBetterCharacterController.Instance != null)
                BetterBetterCharacterController.Instance.SetImmobilized(__0);
        }
    }
}
