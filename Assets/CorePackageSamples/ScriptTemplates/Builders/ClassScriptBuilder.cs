using UnityEditor;

namespace CorePackage.Utilities.ScriptTemplates.Builders
{
    public class ClassScriptBuilder : ScriptBuilderBase<ClassScriptBuilder>
    {
        [MenuItem(MENU_SCRIPTS_NAME + "Class", false, MENU_SCRIPTS_PRIORITY + 101)]
        private static void NewScript()
        {
            NewScriptDefault();
        }
    }
}