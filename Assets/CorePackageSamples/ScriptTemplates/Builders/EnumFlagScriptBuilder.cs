using UnityEditor;

namespace CorePackage.ScriptTemplates.Builders
{
    public class EnumFlagScriptBuilder : ScriptBuilderBase<EnumFlagScriptBuilder>
    {
        [MenuItem(MENU_SCRIPTS_NAME + "Enum Flag", false, MENU_SCRIPTS_PRIORITY + 105)]
        private static void NewScript()
        {
            NewScriptDefault();
        }
    }
}
