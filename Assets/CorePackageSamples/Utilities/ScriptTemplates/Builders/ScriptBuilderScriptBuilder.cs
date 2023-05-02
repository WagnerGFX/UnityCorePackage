using UnityEditor;

namespace CorePackage.Utilities.ScriptTemplates.Builders
{
    public class ScriptBuilderScriptBuilder : ScriptBuilderBase<ScriptBuilderScriptBuilder>
    {
        [MenuItem(MENU_SCRIPTS_NAME + "Script Builder", false, MENU_SCRIPTS_PRIORITY + 80)]
        private static void NewScript()
        {
            using ScriptBuilderScriptBuilder instance = CreateInstance();

            NewScriptData assetData = new(instance.Template, instance.Icon);
            assetData.AddProcessors(ProcessDisplayName, ProcessDefaultScriptName);

            DoCreateNewScriptAsset.CreateFromTemplate(assetData);
        }

        private static void ProcessDisplayName(DoCreateNewScriptAsset instance)
        {
            string displayName = instance.FileNameNoSpace.Replace("ScriptBuilder", "").Replace("Builder","");

            displayName = DefaultProcessors.Capitalize(displayName);

            instance.FileContent = instance.FileContent.Replace(DefaultProcessors.TAG_DISPLAY_NAME, displayName);
        }

        private static void ProcessDefaultScriptName(DoCreateNewScriptAsset instance)
        {
            string scriptName = instance.FileNameNoSpace.Replace("ScriptBuilder", "").Replace("Builder", "");

            instance.FileContent = instance.FileContent.Replace(DefaultProcessors.TAG_FILENAME, scriptName);
        }
    }
}
