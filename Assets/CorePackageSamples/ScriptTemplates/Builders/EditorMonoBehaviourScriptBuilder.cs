using UnityEditor;

namespace CorePackage.Utilities.ScriptTemplates.Builders
{
    public class EditorMonoBehaviourScriptBuilder : ScriptBuilderBase<EditorMonoBehaviourScriptBuilder>
    {
        [MenuItem(MENU_SCRIPTS_NAME + "Editor MonoBehaviour", false, MENU_SCRIPTS_PRIORITY + 51)]
        private static void NewScript()
        {
            NewScriptDefault();
        }
    }
}