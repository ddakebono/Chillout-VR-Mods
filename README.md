# Chillout-VR-Mods
QOL mods for the game 'Chillout VR' or 'CVR'

--- 
### How to compile
- Ensure you have the latest ML build downloaded to CVR.
- Clone the repo and select the specific mod you want to build
- In your System Enviroment Variables, add a new variable named 'CVR' which points to your ChillOutVR Folder
- Open the solution in your preferred IDE / Editor.
- Build as Debug or Release and it should automatically build into your 'Mods' Folder

---

### Camera Mod :camera:
Initializes a Unity GUI menu that allows you to edit the camera's FOV and Aspect Ratio. 

- Keybinds: [Insert] - Toggles GUI Menu

### Floor Drop :fallen_leaf: 
An educational proof of concept of breaking colliders.

This exploit works by instantiating portals (or any other networked object) at either an extremely low, or high floating point value.

This becomes an issue due to floating point accuracy, which causes major issues, as the more specific a number, the less accurate it is calculated.
For example : 'invalidating colliders'.
To learn more about this, look at Computerphile's extremely informative video: 
[![Computerphile Floating Point Accuracy](https://img.youtube.com/vi/PZRI1IfStY0/0.jpg)](https://www.youtube.com/watch?v=PZRI1IfStY0)

- Keybinds: [Insert] - Toggles GUI Menu
