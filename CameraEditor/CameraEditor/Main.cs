using ABI_RC.Core.Player;
using MelonLoader;
using System.Collections;
using UnityEngine;
using UnityEngine.XR;

namespace CameraEditor
{
    class Main : MelonMod
    {
        static MelonPreferences_Category melonCategory;
        static MelonPreferences_Entry<int> fovEntry;
        static MelonPreferences_Entry<float> aspectEntry;

        static int currentFOV = 60;
        static float currentAspect = 1.7778f;

        private static Camera mainCamera;
        private static bool inVR = XRDevice.isPresent;

        public override void OnApplicationLateStart()
        {
            melonCategory = MelonPreferences.CreateCategory("Camera Editor");

            fovEntry = melonCategory.CreateEntry("Field Of View", 60);
            aspectEntry = melonCategory.CreateEntry("Aspect Ratio", 1.7778f);

            currentFOV = fovEntry.Value;
            currentAspect = aspectEntry.Value;

            fovEntry.OnValueChanged += (_, val) => SetFOV(val);
            aspectEntry.OnValueChanged += (_, val) => SetAspect(val);

            MelonCoroutines.Start(WaitForCamera());
        }

        static void SetFOV(int value)
            => currentFOV = value;
        
        static void SetAspect(float value)
            => currentAspect = value;

        static IEnumerator WaitForCamera()
        {
            while (GetLocalPlayer() == null) yield return null;

            mainCamera = GetPlayerCamera();
        }

        public override void OnUpdate()
        {
            if (GetLocalPlayer() != null && mainCamera != null)
            {
                mainCamera.fieldOfView = currentFOV;
                mainCamera.aspect = currentAspect;
            }
        }

        static PlayerSetup GetLocalPlayer()
        {
            return PlayerSetup.Instance;
        }

        static Camera GetPlayerCamera()
        {
            if (inVR)
                return GetLocalPlayer().vrCamera.GetComponent<Camera>();
            else
                return GetLocalPlayer().desktopCamera.GetComponent<Camera>();
        }
    }
}
