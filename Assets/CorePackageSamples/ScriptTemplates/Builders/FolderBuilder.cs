using UnityEditor;
using UnityEditor.Experimental;
using UnityEngine;

namespace CorePackage.Utilities.ScriptTemplates.Builders
{
    public class FolderBuilder : ScriptBuilderBase<FolderBuilder>
    {
        [MenuItem(MENU_SCRIPTS_NAME + "Folder", false, MENU_SCRIPTS_PRIORITY + 15)]
        private static void NewScript()
        {
            Texture2D emptyFolderIcon = EditorGUIUtility.IconContent(EditorResources.emptyFolderIconName).image as Texture2D;

            using FolderBuilder instance = CreateInstance();

            DoCreateNewScriptAsset.CreateScriptAssetFromTemplate(
                instance.Template,
                "New Folder",
                emptyFolderIcon,
                false,
                CreateFolder,
                DefaultProcessors.FocusOnAsset);
        }

        private static void CreateFolder(DoCreateNewScriptAsset folderAsset)
        {
            string guid = AssetDatabase.CreateFolder(folderAsset.FileDirectory, folderAsset.FileName);

            folderAsset.FileAsset = AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(guid), typeof(UnityEngine.Object));
        }
    }
}
