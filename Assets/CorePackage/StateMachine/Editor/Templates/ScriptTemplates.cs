using System.IO;
using System.Text;
using UnityEditor;
using UnityEditor.ProjectWindowCallback;
using UnityEngine;

internal class ScriptTemplates : ScriptableObject
{
    [SerializeField]
    private TextAsset _stateAction;

    [SerializeField]
    private TextAsset _stateCondition;

    private static ScriptTemplates _instance;

    private static void LoadInstance()
    {
        if (!_instance)
        {
            _instance = CreateInstance<ScriptTemplates>();
        }
    }

    private static string GetStateActionPath()
    {
        LoadInstance();
        return AssetDatabase.GetAssetPath(_instance._stateAction);
    }

    private static string GetStateConditionPath()
    {
        LoadInstance();
        return AssetDatabase.GetAssetPath(_instance._stateCondition);
    }


    [MenuItem("Assets/Create/CorePackage/State Machine/Action Script", false, 1)]
    public static void CreateActionScript()
    {
        ProjectWindowUtil.StartNameEditingIfProjectWindowExists(
                0,
                CreateInstance<DoCreateStateMachineScriptAsset>(),
                "NewActionSO.cs",
                (Texture2D)EditorGUIUtility.IconContent("cs Script Icon").image,
                GetStateActionPath());
    }

    [MenuItem("Assets/Create/CorePackage/State Machine/Condition Script", false, 1)]
    public static void CreateConditionScript()
    {
        ProjectWindowUtil.StartNameEditingIfProjectWindowExists(
            0,
            CreateInstance<DoCreateStateMachineScriptAsset>(),
            "NewConditionSO.cs",
            (Texture2D)EditorGUIUtility.IconContent("cs Script Icon").image,
            GetStateConditionPath());
    }

    private class DoCreateStateMachineScriptAsset : EndNameEditAction
    {
        public override void Action(int instanceId, string pathName, string resourceFile)
        {
            string text = File.ReadAllText(resourceFile);

            string fileName = Path.GetFileName(pathName);
            {
                string newName = fileName.Replace(" ", "");
                if (!newName.Contains("SO"))
                { newName = newName.Insert(fileName.Length - 3, "SO"); }

                pathName = pathName.Replace(fileName, newName);
                fileName = newName;
            }

            string fileNameWithoutExtension = fileName[..^3];
            text = text.Replace("#SCRIPTNAME#", fileNameWithoutExtension);

            string runtimeName = fileNameWithoutExtension.Replace("SO", "");
            text = text.Replace("#RUNTIMENAME#", runtimeName);

            for (int i = runtimeName.Length - 1; i > 0; i--)
            {
                if (char.IsUpper(runtimeName[i]) && char.IsLower(runtimeName[i - 1]))
                { runtimeName = runtimeName.Insert(i, " "); }
            }

            text = text.Replace("#RUNTIMENAME_WITH_SPACES#", runtimeName);

            string fullPath = Path.GetFullPath(pathName);
            UTF8Encoding encoding = new(true);
            File.WriteAllText(fullPath, text, encoding);
            AssetDatabase.ImportAsset(pathName);
            ProjectWindowUtil.ShowCreatedAsset(AssetDatabase.LoadAssetAtPath(pathName, typeof(UnityEngine.Object)));
        }
    }
}
