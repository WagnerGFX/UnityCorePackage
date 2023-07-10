using UnityEditor;

namespace CorePackage.ScriptTemplates.Builders
{
    public class AttributeScriptBuilder : ScriptBuilderBase<AttributeScriptBuilder>
    {
        [MenuItem(MENU_SCRIPTS_NAME + "Attribute", false, MENU_SCRIPTS_PRIORITY + 55)]
        private static void NewScript()
        {
            NewScriptDefault();
        }
    }
}
