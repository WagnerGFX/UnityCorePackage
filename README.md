# UnityCorePackage
[![Unity 2020 LTS+](https://img.shields.io/badge/Unity-2020%20LTS%20%2B-blue.svg)](https://unity3d.com/get-unity/download)
[![License: MIT](https://img.shields.io/badge/License-MIT-brightgreen.svg)](https://github.com/WagnerGFX/UnityCorePackage/blob/main/LICENSE.md)

A collection of small, but useful libraries for Unity. Many were collected from open sources, then improved or re-implemented, check the credits section for their respective original works.

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
The project can be installed in an existing Unity project using the .unitypackage files available in the ***Releases*** section.

After applying the package, also install any relevant packages listed in the Requirements.

## Project Compilation
If you forked or cloned the whole project, then use the menu **Tools > Export UnityPackages** to automatically generate all unitypackage files.

## User Contributions
> ⚠ This is not a project created with the intention of being actively maintained with frequent updates and new features.

You are free to open new issues/PRs for bug fixes or features, but be aware that I might take a long time to reply and may deny them if they diverge too wildly from the intended scope.

If you want to fork the project for your own use, I just politely ask to attribute me and the [other creators](#credits-and-references) in your credits section.

## Features Overview

### Debugging > DrawArrow [^arrow]
Allows to draw a simple arrow made of lines using Gizmo or Debug.
- The arrowhead can be placed anywhere in the line, not just at the end.

### Debugging > ProfilerAPI
Uses the **Unity Profiling Core API** package to create an easy way to track specific blocks of code in the Profiler.

### Debugging > ProfilerSearch [^profiler]
Window that helps to find functions in the Profiler. Useful when it's not clear on what frame the function was called.

It's useful if you can't use the **Unity Profiling Core API** package, use the **ProfilerAPI** otherwise.

### EventSystem > Classic [^cevent]
A centralized event system made with pure C# events and custom event args.

It's highly performant but only works with code. Great for programmers.

### EventSystem > Unity [^uevent]
An alternative event system made with UnityEvents and Scriptable Objects.

It's not as performant, but it's easily configurable through the Editor and Inspector with minimal code.

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

### Script Templates
A fully customizable script template creation.
- Uses individual methods to process each tag in a template.
- Can inject your own behavior to process tags or override existing behaviors.
- Can change the default script creation behavior from start to end.
- I highly suggest keeping the sample files as a base.

### Singleton
Two singleton classes to create MonoBehaviours or ScriptableObjects that require only a single instance to exist.

When detecting duplicate objects: The MonoBehaviour will delete itself and the Scriptable Object will issue a warning in the console.

Both of them prevent public access to the static instance, avoiding some clean code issues with singletons.

### State Machine [^fsm]
A State Machine highly configurable through Scriptable Objects.

### Utilities > Debug Camera Controller
A camera controller useful for moving freely around the scene during Play Mode.

### Utilities > Extensions
- **Object:** Validation to check for null, files and prefab files.
- **MonoBehaviour:** Assert Unity Objects for null values. Useful during OnValidate().
- **Vector2D/3D:** A collection of methods to convert analog movement into discrete. Great for grid movement.

## Credits and References
[^arrow]: *Original idea discussed and implemented in the Unity Forums* | [link](https://forum.unity.com/threads/debug-drawarrow.85980/) |
[^profiler]: *Original code made by Unity Developer MartinTilo* | [link](https://forum.unity.com/threads/search-for-samples-by-name-in-the-profiler.1046746/#post-6774617) |
[^cevent]: *Based on Mike Mittleman's talk on Unite 2008* | [link](https://www.youtube.com/watch?v=FNyzfrujJtk) |
[^uevent]: *Based on a similar implementation used in Unity Open Project#1 - Chop Chop* | [link](https://github.com/UnityTechnologies/open-project-1) |
[^guids]: *Improved from the code created by Searous* | [link 1](https://forum.unity.com/threads/cannot-serialize-a-guid-field-in-class.156862/#post-6996680) | [link 2](https://github.com/Searous/Unity.SerializableGuid) |
[^fsm]: *Originally created by DeivSky for Unity Open Project#1 - Chop Chop* | [link](https://github.com/UnityTechnologies/open-project-1) |
