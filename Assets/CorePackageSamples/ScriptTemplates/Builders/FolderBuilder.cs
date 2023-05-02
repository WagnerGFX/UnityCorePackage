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

            NewScriptData assetData = new(instance.Template, emptyFolderIcon);
            assetData.DefaulNameWithExtension = "New Folder";
            assetData.ApplyDefaultProcessors = false;
            assetData.AddProcessors(CreateFolder, DefaultProcessors.FocusOnAsset);

            DoCreateNewScriptAsset.CreateFromTemplate(assetData);
        }

        private static void CreateFolder(DoCreateNewScriptAsset folderAsset)
        {
            string guid = AssetDatabase.CreateFolder(folderAsset.FileDirectory, folderAsset.FileName);

            folderAsset.FileAsset = AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(guid), typeof(UnityEngine.Object));
        }
    }
}
