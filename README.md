# UnityCorePackage
[![Unity 2020 LTS+](https://img.shields.io/badge/Unity-2020%20LTS%20%2B-blue.svg)](https://unity3d.com/get-unity/download)
[![License: MIT](https://img.shields.io/badge/License-MIT-brightgreen.svg)](https://github.com/WagnerGFX/UnityCorePackage/blob/main/LICENSE.md)

A collection of small, but useful libraries for Unity. Many were collected, improved or re-implemented, check the credits section for their respective original works.

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
- [TextMeshPro](https://docs.unity3d.com/Manual/com.unity.textmeshpro.html) + TMP Essentials
- [DOTween Free](https://assetstore.unity.com/packages/tools/animation/dotween-hotween-v2-27676)

## Installation
The project can be installed in an existing Unity project using the .unitypackage files available in the [Releases](https://github.com/WagnerGFX/UnityCorePackage/releases/latest) section.

After applying the package, also install any relevant packages listed in the Requirements.

## Overview

### Common
Holds value references for the entire project, mostly strings for names and paths.
In Samples, holds common assets for other samples.

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

### GameObject GUID [^guids]
A GUID that can be serialized by Unity and a component that holds it. Useful for tracking non-procedural GameObjects in the scene.

The Component will prevent the GUID from being duplicated or changed arbitrarily by the Editor. This includes:
- Resetting the Component.
- Duplicating the GameObject in the same scene.
- Creating and instatiating Prefabs.
- Overriding or reverting Prefab data.
- Undo/Redo.

Limitations:
 - GUID will always be empty for prefab assets.
 - Duplicating the scene will also duplicate all GUIDs inside.
 - Pratically useless for dynamic instancing.

### Singleton
Two singleton classes to create objects that require only a single instance to exist.

When detecting duplicate objects: The MonoBehaviour will delete itself and the Scriptable Object will issue a warning in the console.

Both of them prevent public access to the static instance, avoiding common issues with singletons.

### State Machine [^fsm]
A State Machine highly configurable through Scriptable Objects.


## Other recommended libraries
Need more? Take a look at these free libraries to help in your gamedev journey.

[**NaughtyAttributes**](https://github.com/dbrizov/NaughtyAttributes) is quite an useful extension to supplement the default Unity attributes.

[**Code Monkey Utilities**](https://unitycodemonkey.com/utils.php) contains a bunch of useful classes like displaying UI in the world space and extension methods with helper functions.


## Credits and References
[^arrow]: *Original idea discussed and implemented in the Unity Forums* | [link](https://forum.unity.com/threads/debug-drawarrow.85980/) |
[^profiler]: *Original code made by Unity Developer MartinTilo* | [link](https://forum.unity.com/threads/search-for-samples-by-name-in-the-profiler.1046746/#post-6774617) |
[^cevent]: *Based on Mike Mittleman's talk on Unite 2008* | [link](https://www.youtube.com/watch?v=FNyzfrujJtk) |
[^uevent]: *Based on a similar implementation used in Unity Open Project#1 - Chop Chop* | [link](https://github.com/UnityTechnologies/open-project-1) |
[^guids]: *Improved from the code created by Searous* | [link 1](https://forum.unity.com/threads/cannot-serialize-a-guid-field-in-class.156862/#post-6996680) | [link 2](https://github.com/Searous/Unity.SerializableGuid) |
[^fsm]: *Originally created by DeivSky for Unity Open Project#1 - Chop Chop* | [link](https://github.com/UnityTechnologies/open-project-1) |
