using HarmonyLib;
using MelonLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace NoMirrors
{
    public class Main : MelonMod
    {
        public override void OnApplicationStart()
        {
            HarmonyLib.Harmony harmonyInstance = new("vicuMirror");

            harmonyInstance.Patch(
               typeof(CVRMirror).GetMethod(nameof(CVRMirror.OnWillRenderObject)),
               new HarmonyLib.HarmonyMethod(typeof(Main).GetMethod(nameof(OnWillRenderObject), AccessTools.all)) // In most circumstances, using AccessTools.all is bad practice, and inefficent. This will be corrected soon // 
           );
        }

        private static void OnWillRenderObject(CVRMirror __instance)
        {
            GameObject.DestroyImmediate(__instance.gameObject);
        }
    }
}
