using UnityEditor;

namespace CorePackage.ScriptTemplates.Builders
{
    public class PropertyDrawerScriptBuilder : ScriptBuilderBase<PropertyDrawerScriptBuilder>
    {
        [MenuItem(MENU_SCRIPTS_NAME + "Property Drawer of ...", false, MENU_SCRIPTS_PRIORITY + 54)]
        private static void NewScript()
        {
            NewScriptDefault();
        }
    }
}
