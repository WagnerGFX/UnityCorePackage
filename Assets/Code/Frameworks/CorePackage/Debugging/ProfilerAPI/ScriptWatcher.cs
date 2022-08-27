using Unity.Profiling;
using UnityEngine;


//Package Needed:       Unity Profiling Core API
//Setup:                Automatic through WatchesProfilerModule

//Category:             Custom Category in the profiler settings, to organize Counters.
//Module:               Displays a group of Counters on the Profiler
//Marker:               Profile code blocks. (Exec time, GC Alloc, Calls)
//Counter:              Add counters to be tracked by the Profiler window. (statistics)
//ProfilerCounter:      Has no internal value. Needs to sample manually.
//ProfilerCounterValue: Manages the value internally. Can sample automatically.
//Obs:                  Created Counters cannot be overridden, but they are removed when the Editor is restarted.

namespace CorePackage.Debugging.ProfilerAPI
{
    /// <summary>
    /// Profiles your code using a Marker and adds an execution Counter to easly search in the Profiler Window.
    /// </summary>
    public static class ScriptWatcher
    {
        private static readonly ProfilerCounterValue<int> profilerMarkerCounter;
        private static readonly ProfilerMarker profilerMarker;

        static ScriptWatcher()
        {
            profilerMarkerCounter = new(ProfilerCategory.Scripts,
                                        "WatchedScript Calls",
                                        ProfilerMarkerDataUnit.Count,
                                        ProfilerCounterOptions.FlushOnEndOfFrame | ProfilerCounterOptions.ResetToZeroOnFlush);

            profilerMarker = new(ProfilerCategory.Scripts,
                                 "WatchedScript");
        }

        public static void Begin()
        {
            profilerMarkerCounter.Value++;
            profilerMarker.Begin();
        }

        public static void Begin(Object contextUnityObject)
        {
            profilerMarkerCounter.Value++;
            profilerMarker.Begin(contextUnityObject);
        }

        public static void End()
        {
            profilerMarker.End();
        }

        public static ProfilerMarker.AutoScope Auto()
        {
            profilerMarkerCounter.Value++;
            return profilerMarker.Auto();
        }
    }
}