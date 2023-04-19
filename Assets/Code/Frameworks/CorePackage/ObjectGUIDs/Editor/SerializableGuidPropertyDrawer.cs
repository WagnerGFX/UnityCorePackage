using System;
using UnityEditor;
using UnityEngine;

namespace CorePackage.ObjectGUIDs.Editor
{
    [CustomPropertyDrawer(typeof(SerializableGuid))]
    public class SerializableGuidPropertyDrawer : PropertyDrawer
    {
        private const string SHOW_LABEL_PREF = "serialized_guid_show_label";
        private const string SHOW_BUTTONS_PREF = "serialized_guid_show_buttons";
        private static bool s_showLabel;
        private static bool s_showButtons;

        private bool m_createNewGuid = false;


        static SerializableGuidPropertyDrawer()
        {
            s_showLabel = EditorPrefs.GetBool(SHOW_LABEL_PREF, true);
            s_showButtons = EditorPrefs.GetBool(SHOW_BUTTONS_PREF, true);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty serializedGuid = property.FindPropertyRelative("serializedGuid");

            //Context Menu
            Event e = Event.current;
            bool rightClickedOnProperty = e.type == EventType.MouseDown && e.button == 1 && position.Contains(e.mousePosition);
            if (rightClickedOnProperty)
            {
                GenericMenu context = new();
                context.AddItem(new GUIContent("Copy GUID"), false, CopyGuidToClipboard, serializedGuid.stringValue);
                context.AddItem(new GUIContent("New GUID"), false, () => m_createNewGuid = true);
                context.AddSeparator(string.Empty);
                context.AddItem(new GUIContent("Show Label"), s_showLabel, SwitchCompactView);
                context.AddItem(new GUIContent("Show Buttons"), s_showButtons, SwitchHideButtons);

                context.ShowAsContext();
            }

            //GUID Display
            if (s_showLabel)
            {
                EditorGUI.LabelField(position, label.text, serializedGuid.stringValue);
            }
            else
            {
                EditorGUI.LabelField(position, new GUIContent(serializedGuid.stringValue, label.text));
            }

            //Buttons
            if (s_showButtons)
            {
                GUILayout.BeginHorizontal();
                if (GUILayout.Button("Copy GUID"))
                {
                    CopyGuidToClipboard(serializedGuid.stringValue);
                }

                if (GUILayout.Button("New GUID"))
                {
                    m_createNewGuid = true;
                }
                GUILayout.EndHorizontal();
            }

            // Create new GUID when requested
            if (m_createNewGuid)
            {
                m_createNewGuid = false;
                serializedGuid.stringValue = Guid.NewGuid().ToString();
                serializedGuid.serializedObject.ApplyModifiedProperties();
            }
        }

        private void SwitchCompactView()
        {
            s_showLabel = !s_showLabel;
            EditorPrefs.SetBool(SHOW_LABEL_PREF, s_showLabel);
        }

        private void SwitchHideButtons()
        {
            s_showButtons = !s_showButtons;
            EditorPrefs.SetBool(SHOW_BUTTONS_PREF, s_showButtons);
        }

        private void CopyGuidToClipboard(object guidString)
        {
            EditorGUIUtility.systemCopyBuffer = guidString.ToString();
            Debug.Log($"GUID copied to clipboard: {guidString}");
        }
    }
}