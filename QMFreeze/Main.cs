using ABI_RC.Core.InteractionSystem;
using ABI_RC.Systems.MovementSystem;
using MelonLoader;
using HarmonyLib;

namespace QMFreeze
{
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
            if (MovementSystem.Instance != null)
                MovementSystem.Instance.canMove = !__0;
        }
    }
}
