using UnityEditor;

namespace CorePackage.ScriptTemplates.Builders
{
    public class EditorScriptBuilder : ScriptBuilderBase<EditorScriptBuilder>
    {
        [MenuItem(MENU_SCRIPTS_NAME + "Editor of ...", false, MENU_SCRIPTS_PRIORITY + 50)]
        private static void NewScript()
        {
            NewScriptDefault();
        }
    }
}
