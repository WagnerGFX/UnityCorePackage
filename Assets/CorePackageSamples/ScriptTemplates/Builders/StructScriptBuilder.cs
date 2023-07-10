using UnityEditor;

namespace CorePackage.Utilities.ScriptTemplates.Builders
{
    public class StructScriptBuilder : ScriptBuilderBase<StructScriptBuilder>
    {
        [MenuItem(MENU_SCRIPTS_NAME + "Struct", false, MENU_SCRIPTS_PRIORITY + 102)]
        private static void NewScript()
        {
            NewScriptDefault();
        }
    }
}