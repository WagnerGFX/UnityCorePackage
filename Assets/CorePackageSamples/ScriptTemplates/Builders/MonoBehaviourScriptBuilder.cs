using UnityEditor;

// This script has a different namespace by design.
// The Editor will place the submenu based on the priority defined here.
// Keep other Builders one namespace deeper than this one
namespace CorePackage.Utilities.ScriptTemplates
{
    public class MonoBehaviourScriptBuilder : ScriptBuilderBase<MonoBehaviourScriptBuilder>
    {
        [MenuItem(MENU_SCRIPTS_NAME + "MonoBehaviour", false, MENU_SCRIPTS_PRIORITY)]
        private static void NewScript()
        {
            NewScriptDefault();
        }
    }
}
