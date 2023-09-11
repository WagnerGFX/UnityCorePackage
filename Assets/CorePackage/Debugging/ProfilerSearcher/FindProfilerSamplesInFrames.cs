// https://forum.unity.com/threads/search-for-samples-by-name-in-the-profiler.1046746/#post-6774617

#if UNITY_2020_1_OR_NEWER
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Profiling;
using UnityEditorInternal;
using UnityEngine;

namespace CorePackage.Debugging.ProfilerSearcher
{
    public class FindProfilerSamplesInFrames : EditorWindow
    {
        private string _sampleName = "";
        private string _previousSearch = string.Empty;
        private readonly List<Vector2Int> _foundFrames = new();


        [MenuItem("Window/Analysis/Profiler Marker Search")]
        public static void ShowWindow()
        {
            GetWindow<FindProfilerSamplesInFrames>("Profiler Marker Search");
        }

        private void OnGUI()
        {
            EditorGUILayout.LabelField("Use the textbox to search for an specific Marker in all sampled frames.");
            EditorGUILayout.LabelField("You must use the full Marker name as displayed in the Profiler.");
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("Marker:");
            _sampleName = EditorGUILayout.TextField(_sampleName);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space();
            bool searchPressed = GUILayout.Button("Search");

            if (searchPressed)
            {
                _previousSearch = _sampleName;

                _foundFrames.Clear();
                for (int frame = ProfilerDriver.firstFrameIndex; frame < ProfilerDriver.lastFrameIndex; frame++)
                {
                    int threadIndex = 0;
                    RawFrameDataView frameData = ProfilerDriver.GetRawFrameDataView(frame, threadIndex);

                    while (frameData.valid)
                    {
                        int markerId = frameData.GetMarkerId(_sampleName);

                        if (markerId == FrameDataView.invalidMarkerId)
                        { break; }

                        for (int sampleIndex = 0; sampleIndex < frameData.sampleCount; sampleIndex++)
                        {
                            if (frameData.GetSampleMarkerId(sampleIndex) == markerId)
                            {
                                _foundFrames.Add(new Vector2Int(frame, threadIndex));
                                break;
                            }
                        }

                        frameData = ProfilerDriver.GetRawFrameDataView(frame, ++threadIndex);
                        frameData.Dispose();
                    }
                    frameData.Dispose();
                }
            }

#if UNITY_2021_1_OR_NEWER

            if (_previousSearch != string.Empty)
            {
                EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
                EditorGUILayout.Space();
                EditorGUILayout.LabelField($"{_foundFrames.Count} result(s) found for: \"{_previousSearch}\"");
            }

            GUILayout.BeginHorizontal();
            for (int i = 0; i < _foundFrames.Count; i++)
            {
                if (i % 10 == 0)
                {
                    // line break
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                }
                if (GUILayout.Button($"Frame: {_foundFrames[i].x} - Thread: {_foundFrames[i].y}"))
                {
                    ProfilerWindow window = GetWindow<ProfilerWindow>();
                    IProfilerFrameTimeViewSampleSelectionController cpuModule = window.GetFrameTimeViewSampleSelectionController(ProfilerWindow.cpuModuleIdentifier);
                    using RawFrameDataView frameData = ProfilerDriver.GetRawFrameDataView(_foundFrames[i].x, _foundFrames[i].y);
                    cpuModule.SetSelection(_sampleName, _foundFrames[i].x, threadId: frameData.threadId);
                }
            }
            GUILayout.EndHorizontal();
#else
        var sb = new StringBuilder();
        foreach (var frame in m_FoundFrames)
        {
            sb.AppendFormat("Frame: {0} - Thread: {1} | ", frame.x, frame.y);
        }

        GUILayout.TextArea(sb.ToString());
#endif
        }
    }
}
#endif