using System;
using UnityEditor;
using UnityEngine;

namespace CorePackage.GameObjectGUID.Editor
{
    [CustomPropertyDrawer(typeof(SerializableGuid))]
    public class SerializableGuidPropertyDrawer : PropertyDrawer
    {
        private const string SHOW_LABEL_PREF = "serialized_guid_show_label";
        private const string SHOW_BUTTONS_PREF = "serialized_guid_show_buttons";
        private static bool _showLabel;
        private static bool _showButtons;

        private bool _createNewGuid = false;


        static SerializableGuidPropertyDrawer()
        {
            _showLabel = EditorPrefs.GetBool(SHOW_LABEL_PREF, true);
            _showButtons = EditorPrefs.GetBool(SHOW_BUTTONS_PREF, true);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty serializedGuid = property.FindPropertyRelative("_serializedGuid");

            //Context Menu
            Event e = Event.current;
            bool rightClickedOnProperty = e.type == EventType.MouseDown && e.button == 1 && position.Contains(e.mousePosition);
            if (rightClickedOnProperty)
            {
                GenericMenu context = new();
                context.AddItem(new GUIContent("Copy GUID"), false, CopyGuidToClipboard, serializedGuid.stringValue);
                context.AddItem(new GUIContent("New GUID"), false, () => _createNewGuid = true);
                context.AddSeparator(string.Empty);
                context.AddItem(new GUIContent("Show Label"), _showLabel, SwitchCompactView);
                context.AddItem(new GUIContent("Show Buttons"), _showButtons, SwitchHideButtons);

                context.ShowAsContext();
            }

            //GUID Display
            if (_showLabel)
            {
                EditorGUI.LabelField(position, label.text, serializedGuid.stringValue);
            }
            else
            {
                EditorGUI.LabelField(position, new GUIContent(serializedGuid.stringValue, label.text));
            }

            //Buttons
            if (_showButtons)
            {
                GUILayout.BeginHorizontal();
                if (GUILayout.Button("Copy GUID"))
                {
                    CopyGuidToClipboard(serializedGuid.stringValue);
                }

                if (GUILayout.Button("New GUID"))
                {
                    _createNewGuid = true;
                }
                GUILayout.EndHorizontal();
            }

            // Create new GUID when requested
            if (_createNewGuid)
            {
                _createNewGuid = false;
                serializedGuid.stringValue = Guid.NewGuid().ToString();
                serializedGuid.serializedObject.ApplyModifiedProperties();
            }
        }

        private void SwitchCompactView()
        {
            _showLabel = !_showLabel;
            EditorPrefs.SetBool(SHOW_LABEL_PREF, _showLabel);
        }

        private void SwitchHideButtons()
        {
            _showButtons = !_showButtons;
            EditorPrefs.SetBool(SHOW_BUTTONS_PREF, _showButtons);
        }

        private void CopyGuidToClipboard(object guidString)
        {
            EditorGUIUtility.systemCopyBuffer = guidString.ToString();
            Debug.Log($"GUID copied to clipboard: {guidString}");
        }
    }
}
