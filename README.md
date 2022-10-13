# UnityCorePackage
A collection of small, but useful libraries for Unity. Many were collected, improved or re-implemented, check the credits section for their respective original works.

## Common
Holds value references for the entire project, mostly strings for names and paths.

## Debugging > DrawArrow [^arrow]
Allows to draw a simple arrow made of lines using Gizmo or Debug.

## Debugging > ProfilerAPI
Uses the **Unity Profiling Core API** package to create an easy way to track specific blocks of code in the Profiler.

## Debugging > ProfilerSearch [^profiler]
Window that helps to find functions in the Profiler. Useful when it's not clear on what frame the function was called.

It's useful if you are not allowed to install the **Unity Profiling Core API** package, otherwise use the library above.

## EventSystem - Classic [^cevent]
A centralized event system made with pure C# events and custom event args.

It's highly performant but only works with code. Great for programmers.

## EventSystem - Unity [^uevent]
An alternative event system made with UnityEvents and Scriptable Objects.

It's not as performant, but it's easily configurable through the Editor and Inspector with minimal code.

## Extension Methods
A collection of useful extension methods for Unity classes and types.

## Object GUIDs
A component that holds a unique GUID value. Useful for tracking non-procedural objects in the scene.

## Singleton
Two singleton classes to create objects that require only a single instance to exist.

When detecting duplicate objects: The MonoBehaviour will delete itself and the Scriptable Object will issue a warning.

Both of them prevent public access to the static instance, avoiding common issues with singletons.

## State Machine [^fsm]
A State Machine highly configurable through Scriptable Objects.


# Other recommended libraries
I highly recommend these libraries to help in your gamedev needs.

## NaughtyAttributes | [Link](https://github.com/dbrizov/NaughtyAttributes)
Useful extension to the default Unity attributes.

## Code Monkey Utilities | [Link](https://unitycodemonkey.com/utils.php)
Contains a bunch of useful classes like displaying UI in the world space and extension methods with helper functions.


# Credits and References
[^arrow]: *Original idea discussed and implemented in the Unity Forums* | [link](https://forum.unity.com/threads/debug-drawarrow.85980/) |
[^profiler]: *Original code made by Unity Developer MartinTilo* | [link](https://forum.unity.com/threads/search-for-samples-by-name-in-the-profiler.1046746/#post-6774617) |
[^cevent]: *Based on Mike Mittleman's talk on Unite 2008* | [link](https://www.youtube.com/watch?v=FNyzfrujJtk) |
[^uevent]: *Based on a similar implementation used in Unity Open Project#1 - Chop Chop* | [link](https://github.com/UnityTechnologies/open-project-1) |
[^fsm]: *Originally created by DeivSky for Unity Open Project#1 - Chop Chop* | [link](https://github.com/UnityTechnologies/open-project-1) |
