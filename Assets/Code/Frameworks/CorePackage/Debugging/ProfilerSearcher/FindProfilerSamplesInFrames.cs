// https://forum.unity.com/threads/search-for-samples-by-name-in-the-profiler.1046746/#post-6774617

#if UNITY_2020_1_OR_NEWER
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using UnityEditor.Profiling;

namespace CorePackage.Debugging.ProfilerSearcher
{
    public class FindProfilerSamplesInFrames : EditorWindow
    {
        string m_SampleName = "";
        string previousSearch = string.Empty;

        List<Vector2Int> m_FoundFrames = new List<Vector2Int>();


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
            m_SampleName = EditorGUILayout.TextField(m_SampleName);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space();
            bool searchPressed = GUILayout.Button("Search");

            if (searchPressed)
            {
                previousSearch = m_SampleName;

                m_FoundFrames.Clear();
                for (int frame = ProfilerDriver.firstFrameIndex; frame < ProfilerDriver.lastFrameIndex; frame++)
                {
                    var threadIndex = 0;
                    var frameData = ProfilerDriver.GetRawFrameDataView(frame, threadIndex);

                    while (frameData.valid)
                    {
                        var markerId = frameData.GetMarkerId(m_SampleName);

                        if (markerId  == FrameDataView.invalidMarkerId)
                            break;

                        for (int sampleIndex = 0; sampleIndex < frameData.sampleCount; sampleIndex++)
                        {
                            if (frameData.GetSampleMarkerId(sampleIndex) == markerId)
                            {
                                m_FoundFrames.Add(new Vector2Int(frame, threadIndex));
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

            if (previousSearch != string.Empty)
            {
                EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
                EditorGUILayout.Space();
                EditorGUILayout.LabelField($"{m_FoundFrames.Count} result(s) found for: \"{previousSearch}\"");
            }

            GUILayout.BeginHorizontal();
            for (int i = 0; i < m_FoundFrames.Count; i++)
            {
                if (i % 10 == 0)
                {
                    // line break
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                }
                if (GUILayout.Button($"Frame: { m_FoundFrames[i].x} - Thread: { m_FoundFrames[i].y}"))
                {
                    var window = GetWindow<ProfilerWindow>();
                    var cpuModule = window.GetFrameTimeViewSampleSelectionController(ProfilerWindow.cpuModuleIdentifier);
                    using (var frameData = ProfilerDriver.GetRawFrameDataView(m_FoundFrames[i].x, m_FoundFrames[i].y))
                    {
                        cpuModule.SetSelection(m_SampleName, m_FoundFrames[i].x, threadId: frameData.threadId);
                    }
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