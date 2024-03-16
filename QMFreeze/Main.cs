using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using ABI_RC.Core.InteractionSystem;
using ABI_RC.Core.Player;
using ABI_RC.Systems.Camera.VisualMods;
using ABI_RC.Systems.GameEventSystem;
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
        public static bool IsImmoGetter => IsQMFreezeApplied || BetterBetterCharacterController.Instance.IsImmobilized;

        public static MelonPreferences_Entry<bool> EnableQMFreeze;
        public static bool IsQMFreezeApplied;
        public static Character.MovementMode MovementMode;
        public static bool SkipMovementSave;

        public override void OnInitializeMelon()
        {
            var cat = MelonPreferences.CreateCategory("QMFreeze");
            EnableQMFreeze = MelonPreferences.CreateEntry("QMFreeze", "EnableQMFreeze", true, "Enable QMFreeze", "Enables the QMFreeze immobilize functionality");
        }
    }
    
    [HarmonyPatch]
    class UIOpenPatches
    {
        [HarmonyPostfix]
        [HarmonyPatch(typeof(CVR_MenuManager), nameof(CVR_MenuManager.ToggleQuickMenu))]
        [HarmonyPatch(typeof(ViewManager), nameof(ViewManager.UiStateToggle), typeof(bool))]
        static void UIToggle(bool __0)
        {
            if(!Main.EnableQMFreeze.Value && !Main.IsQMFreezeApplied) return;

            Main.IsQMFreezeApplied = __0;

            if (BetterBetterCharacterController.Instance == null) return;

            if (__0)
            {
                Main.SkipMovementSave = true;
                if(BetterBetterCharacterController.Instance.GetMovementMode() != Character.MovementMode.Flying && BetterBetterCharacterController.Instance.GetMovementMode() != Character.MovementMode.Swimming)
                    BetterBetterCharacterController.Instance.SetMovementMode(Character.MovementMode.None);
                Main.SkipMovementSave = false;
            }
            else
            {
                BetterBetterCharacterController.Instance.SetMovementMode(Main.MovementMode);
            }
        }
    }

    [HarmonyPatch(typeof(BetterBetterCharacterController))]
    class BBCCPatch
    {
        [HarmonyPatch(typeof(Character), nameof(Character.SetMovementMode))]
        [HarmonyPostfix]
        private static void MovementMode(Character.MovementMode newMovementMode)
        {
            if (Main.SkipMovementSave) return;

            Main.MovementMode = newMovementMode;

            if (!Main.IsQMFreezeApplied || newMovementMode is not (Character.MovementMode.Walking or Character.MovementMode.Falling)) return;

            Main.SkipMovementSave = true;
            BetterBetterCharacterController.Instance.SetMovementMode(Character.MovementMode.None);
            Main.SkipMovementSave = false;
        }

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

        private static MethodInfo _immoGetter = typeof(Character).GetProperty(nameof(BetterBetterCharacterController.IsImmobilized), BindingFlags.Instance | BindingFlags.Public)?.GetMethod;
        private static MethodInfo _isImmo = typeof(Main).GetProperty(nameof(Main.IsImmoGetter), BindingFlags.Static | BindingFlags.Public)?.GetMethod;

        [HarmonyPatch(nameof(BetterBetterCharacterController.CanMove))]
        [HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> CanMoveTranspiler(IEnumerable<CodeInstruction> code)
        {
            return new CodeMatcher(code)
                .MatchForward(false, new CodeMatch(OpCodes.Ldarg_0), new CodeMatch(OpCodes.Call, _immoGetter))
                .RemoveInstruction()
                .SetOperandAndAdvance(_isImmo)
                .InstructionEnumeration();
        }
    }
}
