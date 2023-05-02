using UnityEditor;

namespace CorePackage.Utilities.ScriptTemplates.Builders
{
    public class EditorWindowScriptBuilder : ScriptBuilderBase<EditorWindowScriptBuilder>
    {
        [MenuItem(MENU_SCRIPTS_NAME + "Editor Window", false, MENU_SCRIPTS_PRIORITY + 53)]
        private static void NewScript()
        {
            NewScriptDefault();
        }
    }
}