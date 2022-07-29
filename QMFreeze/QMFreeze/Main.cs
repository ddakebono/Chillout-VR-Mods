using ABI_RC.Core.InteractionSystem;
using ABI_RC.Systems.MovementSystem;
using HarmonyLib;
using MelonLoader;
using System;

namespace QMFreeze
{
    public class Main : MelonMod
    {
        public override void OnApplicationStart()
        {
            Type[] type = new[] { typeof(bool) }; // this stops ambigious matches, as two methods exist for 'ViewManagger.UiStateTogggle', one takea a bool for a parameter, the other doesnt.

            HarmonyLib.Harmony harmonyInstance = new("vicuQM");
            harmonyInstance.Patch(
               typeof(CVR_MenuManager).GetMethod(nameof(CVR_MenuManager.ToggleQuickMenu), type),
               null,
               new HarmonyLib.HarmonyMethod(typeof(Main).GetMethod(nameof(UIToggle), AccessTools.all))
           );

            harmonyInstance.Patch(
               typeof(ViewManager).GetMethod(nameof(ViewManager.UiStateToggle), type),
               null,
               new HarmonyLib.HarmonyMethod(typeof(Main).GetMethod(nameof(UIToggle), AccessTools.all))
           );
        }       

        private static void UIToggle(bool __0)
        {
            MovementSystem.Instance.enabled = !__0;
        }


    }
}
