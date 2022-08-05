using UnityEditor;

namespace CorePackage.ObjectGUIDs.Editor
{
    [CustomEditor(typeof(ObjectGuid))]
    public class ObjectGuidEditor : UnityEditor.Editor
    {
        SerializedProperty instanceGuid;

        //Awake will throw errors when recompiling
        private void OnEnable() 
        {
            instanceGuid = serializedObject.FindProperty("_instanceGuid");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.ObjectField("Script", MonoScript.FromMonoBehaviour((ObjectGuid)target), typeof(ObjectGuid), false);
            EditorGUILayout.PropertyField(instanceGuid);
            EditorGUI.EndDisabledGroup();

            serializedObject.ApplyModifiedProperties();
        }
    }
}