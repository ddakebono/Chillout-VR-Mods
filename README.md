# Chillout-VR-Mods
This repo has been archived. The game isn't fun to me.

QOL mods for the game 'Chillout VR' or 'CVR' to be used with 'Melonloader'

--- 
### How to compile
- Ensure you have the latest ML build downloaded to CVR.
- Clone the repo and select the specific mod you want to build
- In your System Enviroment Variables, add a new variable named 'CVR' which points to your ChillOutVR Folder
- Open the solution in your preferred IDE / Editor.
- Build as Debug or Release and it should automatically build into your 'Mods' Folder

---

### Camera Mod :camera:
Allows you to edit different properties of the players camera.
Currently only allows you to edit FOV and Aspect Ratio.

This mod works through melonpreferences. I suggest Sinai's Melon Pref Manager (Mono).

https://github.com/sinai-dev/MelonPreferencesManager

---

### Floor Drop :fallen_leaf: (PATCHED)

DISCLAIMER: This has since been patched by CVR after me and a few others reported it directly to the devs.

An educational proof of concept of breaking colliders.

This exploit works by instantiating portals (or any other networked object) at either an extremely low, or high floating point value.

This becomes an issue due to floating point accuracy, which causes major issues, as the more specific a number, the less accurate it is calculated.
For example : 'invalidating colliders'.
To learn more about this, look at Computerphile's extremely informative video: 

[![Computerphile Floating Point Accuracy](https://img.youtube.com/vi/PZRI1IfStY0/0.jpg)](https://www.youtube.com/watch?v=PZRI1IfStY0)

- Keybinds: [Insert] - Toggles GUI Menu

---

### No Discord RPC :closed_lock_with_key: 
Disables CVR's custom Discord RPC as soon as it initializes.

---

### QM Freeze :stop_button: 
Disables the local players movement system when either of the menu's are opened. Allowing you to open and control your menu while falling.
This keeps you completely still in air whenever any menu is open.

Yes i'm aware a similar mod has been created for BepinEx by 'xKiraiChan', but i didn't find out about this till i'd finished this mod.

---

### No Mirrors :mirror: 

Instantly deletes all mirrors the second they try to be rendered. This may help with performance or just be a preference.
