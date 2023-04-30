using UnityEditor;

namespace CorePackage.Utilities.ScriptTemplates.Builders
{
    public class ScriptableObjectScriptBuilder : ScriptBuilderBase<ScriptableObjectScriptBuilder>
    {
        [MenuItem(MENU_SCRIPTS_NAME + "Scriptable Object", false, MENU_SCRIPTS_PRIORITY + 1)]
        private static void NewScript()
        {
            NewScriptDefault();
        }
    }
}