using ABI_RC.Helpers;
using MelonLoader;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace NoDiscordRPC
{
    public class Main : MelonMod
    {
        public override void OnApplicationStart()
        {
            MelonCoroutines.Start(WaitForPresence());
        }

        public static IEnumerator WaitForPresence()
        {
            while (PresenceManager.instance == null) yield return null;

            PresenceManager.instance.enabled = false;
        }
    }
}
