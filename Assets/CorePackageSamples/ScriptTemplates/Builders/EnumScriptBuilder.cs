using UnityEditor;

namespace CorePackage.ScriptTemplates.Builders
{
    public class EnumScriptBuilder : ScriptBuilderBase<EnumScriptBuilder>
    {
        [MenuItem(MENU_SCRIPTS_NAME + "Enum", false, MENU_SCRIPTS_PRIORITY + 104)]
        private static void NewScript()
        {
            NewScriptDefault();
        }
    }
}
