using UnityEditor;

namespace CorePackage.Utilities.ScriptTemplates.Builders
{
    public class InterfaceScriptBuilder : ScriptBuilderBase<InterfaceScriptBuilder>
    {
        [MenuItem(MENU_SCRIPTS_NAME + "Interface", false, MENU_SCRIPTS_PRIORITY + 103)]
        private static void NewScript()
        {
            NewScriptDefault();
        }
    }
}