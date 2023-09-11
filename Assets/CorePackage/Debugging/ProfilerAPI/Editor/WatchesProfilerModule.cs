using System;
using Unity.Profiling;
using Unity.Profiling.Editor;

namespace CorePackage.Debugging.ProfilerAPI.Editor
{
    /// <summary>
    /// Automatically adds a new module to the Profiler Window.
    /// </summary>
    [Serializable]
    [ProfilerModuleMetadata("Watches")]
    public class WatchesProfilerModule : ProfilerModule
    {
        private static readonly ProfilerCounterDescriptor[] _counters =
        {
            new("WatchedScript Calls", ProfilerCategory.Scripts)
        };

        public WatchesProfilerModule() : base(_counters) { }
    }
}
