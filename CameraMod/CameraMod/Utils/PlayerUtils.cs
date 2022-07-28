using ABI_RC.Core.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CameraMod.Utils
{
    public class PlayerUtils
    {
        public static PlayerSetup GetLocalPlayer()
        {
            return PlayerSetup.Instance;
        }

        public static Camera GetPlayerCamera(bool inVR)
        {
            if (inVR)
                return GetLocalPlayer().vrCamera.GetComponent<Camera>();
            else
                return GetLocalPlayer().desktopCamera.GetComponent<Camera>();
        }
    }
}
