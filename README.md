# UnityCorePackage
A collection of small, but useful libraries for Unity. Many were collected, improved or re-implemented, check the credits section for their respective original works.

### Common
Holds value references for the entire project, mostly strings for names and paths.

### Debugging > DrawArrow [^arrow]
Allows to draw a simple arrow made of lines using Gizmo or Debug.

### Debugging > ProfilerAPI
Uses the **Unity Profiling Core API** package to create an easy way to track specific blocks of code in the Profiler.

### Debugging > ProfilerSearch [^profiler]
Window that helps to find functions in the Profiler. Useful when it's not clear on what frame the function was called.

It's useful if you are not allowed to install the **Unity Profiling Core API** package, otherwise I strongly suggest using the ProfilerAPI above.

### EventSystem > Classic [^cevent]
A centralized event system made with pure C# events and custom event args.

It's highly performant but only works with code. Great for programmers.

### EventSystem > Unity [^uevent]
An alternative event system made with UnityEvents and Scriptable Objects.

It's not as performant, but it's easily configurable through the Editor and Inspector with minimal code.

### Extension Methods
A collection of useful extension methods for Unity classes and types.

### Object GUIDs
A component that holds a unique GUID value. Useful for tracking non-procedural objects in the scene.

### Singleton
Two singleton classes to create objects that require only a single instance to exist.

When detecting duplicate objects: The MonoBehaviour will delete itself and the Scriptable Object will issue a warning in the console.

Both of them prevent public access to the static instance, avoiding common issues with singletons.

### State Machine [^fsm]
A State Machine highly configurable through Scriptable Objects.


# Installation
The project can be installed in an existing Unity project using the .unitypackage files available in the [Releases](https://github.com/WagnerGFX/UnityCorePackage/releases/latest) section.

After that, install the relevant packages listed below.

## Requirements

### The Core Package
- Unity Editor 2020 LTS or above. Older versions may be compatible, but are not supported.
- **ProfilerAPI:** Requires the [Unity Profiling Core API](https://docs.unity3d.com/Manual/com.unity.profiling.core.html) package.
- **ProfilerSearch:** Requires Unity 2021 LTS or above for full functionality.
- **StateMachine:** Editor window uses the [UI Toolkit](https://docs.unity3d.com/Manual/UIElements.html).

### Samples
The samples may use some of these packages/assets for demonstration purposes.
- [2D Packages](https://docs.unity3d.com/Manual/2DFeature.html#)
- [Cinemachine](https://docs.unity3d.com/Manual/com.unity.cinemachine.html)
- [Input System](https://docs.unity3d.com/Manual/com.unity.inputsystem.html)
- [TextMeshPro](https://docs.unity3d.com/Manual/com.unity.textmeshpro.html)
- [DOTween Free](https://assetstore.unity.com/packages/tools/animation/dotween-hotween-v2-27676)


# Other recommended libraries
I highly recommend these free libraries to help in your gamedev needs.

[**NaughtyAttributes**](https://github.com/dbrizov/NaughtyAttributes) is quite an useful extension to supplement the default Unity attributes.

[**Code Monkey Utilities**](https://unitycodemonkey.com/utils.php) contains a bunch of useful classes like displaying UI in the world space and extension methods with helper functions.


# Credits and References
[^arrow]: *Original idea discussed and implemented in the Unity Forums* | [link](https://forum.unity.com/threads/debug-drawarrow.85980/) |
[^profiler]: *Original code made by Unity Developer MartinTilo* | [link](https://forum.unity.com/threads/search-for-samples-by-name-in-the-profiler.1046746/#post-6774617) |
[^cevent]: *Based on Mike Mittleman's talk on Unite 2008* | [link](https://www.youtube.com/watch?v=FNyzfrujJtk) |
[^uevent]: *Based on a similar implementation used in Unity Open Project#1 - Chop Chop* | [link](https://github.com/UnityTechnologies/open-project-1) |
[^fsm]: *Originally created by DeivSky for Unity Open Project#1 - Chop Chop* | [link](https://github.com/UnityTechnologies/open-project-1) |
