using ABI_RC.Core.Player;
using CameraMod.Utils;
using MelonLoader;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.XR;

namespace CameraMod
{
    public class Loader : MelonMod
    {
        public static Camera mainCamera;
        public static int currentFOV = 60;
        public static float currentAspect = 1.7778f;

        public static bool inVR = XRDevice.isPresent;
        public static bool menuOpen = false;

        public override void OnApplicationStart()
        {

            MelonCoroutines.Start(WaitForCamera());
        }

        public static IEnumerator WaitForCamera()
        {
            while (PlayerUtils.GetLocalPlayer() == null) yield return null;

            mainCamera = PlayerUtils.GetPlayerCamera(inVR);
        }

        public override void OnUpdate()
        {
            if (mainCamera != null && PlayerSetup.Instance != null)
            {
                mainCamera.fieldOfView = currentFOV;
                mainCamera.aspect = currentAspect;
            }

            if (Input.GetKeyDown(KeyCode.Insert))
            {
                menuOpen = !menuOpen;
            }
        }

        public override void OnGUI()
        {

            if (menuOpen)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                GUI.skin.button.richText = true;

                GUI.Box(new Rect(100, 20f, 200, 120), "<b><size=25>Camera Mods</size></b>");

                GUI.Label(new Rect(110f, 70f, 150f, 30f), $"FOV : {currentFOV}");
                currentFOV = (int)GUI.HorizontalSlider(new Rect(110f, 60f, 150f, 30f), currentFOV, 30, 120);

                GUI.Label(new Rect(110f, 100f, 150f, 30f), $"Aspect : {currentAspect}");
                currentAspect = GUI.HorizontalSlider(new Rect(110f, 90f, 150f, 30f), currentAspect, 0.5f, 3f);
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }

    }
}
