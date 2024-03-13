using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using ABI_RC.Core.InteractionSystem;
using ABI_RC.Systems.Movement;
using ECM2;
using MelonLoader;
using HarmonyLib;

namespace QMFreeze
{
    
    /// <summary>
    /// Reworked entire mod to use HarmonyX annotations and avoid causing issues with other mods
    /// </summary>
    
    public class Main : MelonMod
    {
        public static bool IsQMFrozen => IsQMFreezeApplied || BetterBetterCharacterController.Instance.IsGrounded();

        public static MelonPreferences_Entry<bool> EnableQMFreeze;
        public static bool IsQMFreezeApplied;

        public override void OnInitializeMelon()
        {
            var cat = MelonPreferences.CreateCategory("QMFreeze");
            EnableQMFreeze = MelonPreferences.CreateEntry("QMFreeze", "EnableQMFreeze", true, "Enable QMFreeze", "Enables the QMFreeze immobilize functionality");
        }
    }
    
    [HarmonyPatch]
    class MakeThingWork
    {
        [HarmonyPostfix]
        [HarmonyPatch(typeof(CVR_MenuManager), nameof(CVR_MenuManager.ToggleQuickMenu))]
        [HarmonyPatch(typeof(ViewManager), nameof(ViewManager.UiStateToggle), typeof(bool))]
        static void UIToggle(bool __0)
        {
            if(!Main.EnableQMFreeze.Value && !Main.IsQMFrozen) return;

            Main.IsQMFreezeApplied = __0;

            if (BetterBetterCharacterController.Instance != null)
                BetterBetterCharacterController.Instance.SetImmobilized(__0);
        }
    }

    [HarmonyPatch(typeof(BetterBetterCharacterController))]
    class BBCCPatch
    {
        private static MethodInfo _groundedGetter = typeof(Character).GetMethod(nameof(Character.IsGrounded), BindingFlags.Instance | BindingFlags.Public);
        private static MethodInfo _isFrozen = typeof(Main).GetProperty(nameof(Main.IsQMFrozen), BindingFlags.Static | BindingFlags.Public)?.GetMethod;

        [HarmonyPatch("Animate")]
        [HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> TranspileTime(IEnumerable<CodeInstruction> code)
        {
            return new CodeMatcher(code)
                .MatchForward(false, new CodeMatch(OpCodes.Ldarg_0), new CodeMatch(OpCodes.Call, _groundedGetter))
                .RemoveInstruction()
                .SetOperandAndAdvance(_isFrozen)
                .InstructionEnumeration();
        }
    }
}
