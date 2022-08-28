using UnityEditor;
using UnityEngine;

namespace CorePackage.ObjectGUIDs.Editor
{
    // Author: Searous
    // Link: https://forum.unity.com/threads/cannot-serialize-a-guid-field-in-class.156862/#post-6996680

    /// <summary>
    /// Property drawer for SerializableGuid
    /// </summary>
    [CustomPropertyDrawer(typeof(SerializableGuid))]
    public class SerializableGuidPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty serializedGuid = property.FindPropertyRelative("serializedGuid");
            EditorGUI.PropertyField(position, serializedGuid);
        }
    }
}