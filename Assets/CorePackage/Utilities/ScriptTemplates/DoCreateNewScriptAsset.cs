using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using UnityEditor;
using UnityEditor.Compilation;
using UnityEditor.Experimental;
using UnityEditor.ProjectWindowCallback;
using UnityEngine;

namespace CorePackage.Utilities.ScriptTemplates
{
    public class DoCreateNewScriptAsset : EndNameEditAction
    {
        public static Texture2D DefaultScriptIcon => EditorGUIUtility.IconContent("cs Script Icon").image as Texture2D;

        /// <summary>
        /// Instance ID given by Unity.
        /// </summary>
        public int InstanceID { get; private set; }

        /// <summary>
        /// Asset created
        /// </summary>
        public UnityEngine.Object FileAsset { get; set; }

        /// <summary>
        /// Actual file data. Editable by processors.
        /// </summary>
        public string FileContent { get; set; }

        /// <summary>
        /// File name with no extension.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// File extension with no .(dot)
        /// </summary>
        public string FileExtension { get; set; }

        /// <summary>
        /// Relative directory path.
        /// </summary>
        public string FileDirectory { get; private set; }

        /// <summary>
        /// Relative path with file and extensions.
        /// </summary>
        public string FilePath => FileDirectory + "\\" + FileName + "." + FileExtension;

        /// <summary>
        /// Absolute path with file and extensions.
        /// </summary>
        public string FileFullPath => Path.GetFullPath(FilePath);

        /// <summary>
        /// File name with no space and no extension.
        /// </summary>
        public string FileNameNoSpace => FileName.Replace(" ", "");

        /// <summary>
        /// Root namespace for C# script files.
        /// </summary>
        public string RootNamespace { get; private set; }

        /// <summary>
        /// Relative path to the template with file and extension. May have two extensions.
        /// </summary>
        public string TemplatePath => AssetDatabase.GetAssetPath(TemplateAsset);

        /// <summary>
        /// The template asset this file was based on.
        /// </summary>
        public TextAsset TemplateAsset { get; private set; }

        /// <summary>
        /// Icon the final script file will display.
        /// <para>Does not work on non-script files</para>
        /// </summary>
        public Texture2D Icon { get; set; }

        /// <summary>
        /// When false, default processors will not be applied automatically.
        /// </summary>
        public bool ApplyDefaultProcessors { get; private set; } = true;

        /// <summary>
        /// Cached from <see cref="Selection.objects"/> on the moment the script creation was requested.
        /// <para>Use for context information.</para>
        /// </summary>
        public UnityEngine.Object[] SelectedObjects { get; private set; }

        /// <summary>
        /// Types marked for evaluation to add using directives.
        /// </summary>
        public ReadOnlyCollection<Type> TypesList => _typesList.AsReadOnly();


        private readonly List<Type> _typesList = new();
        private readonly List<Action<DoCreateNewScriptAsset>> _processorList = new();


        /// <summary>
        /// <para>Will startup the process of creating a new script from a TXT file template.</para>
        /// <para>Default script name will be inferred from the template file name.</para>
        /// </summary>
        /// <param name="scriptTemplate">Text file with the script template</param>
        public static void CreateScriptAssetFromTemplate(TextAsset scriptTemplate)
        {
            CreateScriptAssetFromTemplate(scriptTemplate, string.Empty, DefaultScriptIcon, true);
        }

        /// <summary>
        /// <para>Will startup the process of creating a new script from a TXT file template.</para>
        /// <para>Default script name will be inferred from the template file name.</para>
        /// </summary>
        /// <param name="scriptTemplate">Text file with the script template</param>
        /// <param name="customScriptIcon">Icon for the new script file.</param>
        public static void CreateScriptAssetFromTemplate(TextAsset scriptTemplate, Texture2D customScriptIcon)
        {
            CreateScriptAssetFromTemplate(scriptTemplate, string.Empty, customScriptIcon, true);
        }

        /// <summary>
        /// <para>Will startup the process of creating a new script from a TXT file template.</para>
        /// <para>Default script name will be inferred from the template file name.</para>
        /// <para>Able to inject custom processors and override default ones.</para>
        /// </summary>
        /// <param name="scriptTemplate">Text file with the script template</param>
        /// <param name="customScriptIcon">Icon for the new script file.</param>
        /// <param name="customScriptProcessors"><para>Custom processors to apply on the script content.</para><para>Can override default processors.</para><para>Can also recieve default processors to be applied manually.</para></param>
        public static void CreateScriptAssetFromTemplate(TextAsset scriptTemplate, Texture2D customScriptIcon, params Action<DoCreateNewScriptAsset>[] customScriptProcessors)
        {
            CreateScriptAssetFromTemplate(scriptTemplate, string.Empty, customScriptIcon, true, customScriptProcessors);
        }

        /// <summary>
        /// <para>Will startup the process of creating a new script from a TXT file template.</para>
        /// <para>Default script name will be inferred from the template file name.</para>
        /// <para>Able to inject custom processors and override default ones.</para>
        /// </summary>
        /// <param name="scriptTemplate">Text file with the script template</param>
        /// <param name="customScriptIcon">Icon for the new script file.</param>
        /// <param name="applyDefaultProcessors"><para>When false, will only apply custom processors.</para>The default processors can still be applied manually as custom processors.</param>
        /// <param name="customScriptProcessors"><para>Custom processors to apply on the script content.</para><para>Can override default processors.</para><para>Can also recieve default processors to be applied manually.</para></param>
        public static void CreateScriptAssetFromTemplate(TextAsset scriptTemplate, Texture2D customScriptIcon, bool applyDefaultProcessors, params Action<DoCreateNewScriptAsset>[] customScriptProcessors)
        {
            CreateScriptAssetFromTemplate(scriptTemplate, string.Empty, customScriptIcon, applyDefaultProcessors, customScriptProcessors);
        }

        /// <summary>
        /// <para>Will startup the process of creating a new script from a TXT file template.</para>
        /// <para>Default script name will be inferred from the template file name.</para>
        /// <para>Able to inject custom processors and override default ones.</para>
        /// </summary>
        /// <param name="scriptTemplate">Text file with the script template</param>
        /// <param name="customScriptProcessors"><para>Custom processors to apply on the script content.</para><para>Can override default processors.</para><para>Can also recieve default processors to be applied manually.</para></param>
        public static void CreateScriptAssetFromTemplate(TextAsset scriptTemplate, params Action<DoCreateNewScriptAsset>[] customScriptProcessors)
        {
            CreateScriptAssetFromTemplate(scriptTemplate, string.Empty, DefaultScriptIcon, true, customScriptProcessors);
        }

        /// <summary>
        /// <para>Will startup the process of creating a new script from a TXT file template.</para>
        /// <para>Default script name will be inferred from the template file name.</para>
        /// <para>Able to inject custom processors and override default ones.</para>
        /// </summary>
        /// <param name="scriptTemplate">Text file with the script template</param>
        /// <param name="applyDefaultProcessors"><para>When false, will only apply custom processors.</para>The default processors can still be applied manually as custom processors.</param>
        /// <param name="customScriptProcessors"><para>Custom processors to apply on the script content.</para><para>Can override default processors.</para><para>Can also recieve default processors to be applied manually.</para></param>
        public static void CreateScriptAssetFromTemplate(TextAsset scriptTemplate, bool applyDefaultProcessors, params Action<DoCreateNewScriptAsset>[] customScriptProcessors)
        {
            CreateScriptAssetFromTemplate(scriptTemplate, string.Empty, DefaultScriptIcon, applyDefaultProcessors, customScriptProcessors);
        }

        /// <summary>
        /// <para>Will startup the process of creating a new script from a TXT file template.</para>
        /// </summary>
        /// <param name="scriptTemplate">Text file with the script template</param>
        /// <param name="defaulScriptNameWithExtension">Default name with the .cs extension</param>
        public static void CreateScriptAssetFromTemplate(TextAsset scriptTemplate, string defaulScriptNameWithExtension)
        {
            CreateScriptAssetFromTemplate(scriptTemplate, defaulScriptNameWithExtension, DefaultScriptIcon, true);
        }

        /// <summary>
        /// <para>Will startup the process of creating a new script from a TXT file template.</para>
        /// <para>Can replace the default icon.</para>
        /// </summary>
        /// <param name="scriptTemplate">Text file with the script template</param>
        /// <param name="defaulScriptNameWithExtension">Default name with the .cs extension</param>
        /// <param name="customScriptIcon">Icon for the new script file.</param>
        public static void CreateScriptAssetFromTemplate(TextAsset scriptTemplate, string defaulScriptNameWithExtension, Texture2D customScriptIcon)
        {
            CreateScriptAssetFromTemplate(scriptTemplate, defaulScriptNameWithExtension, customScriptIcon, true);
        }

        /// <summary>
        /// <para>Will startup the process of creating a new script from a TXT file template.</para>
        /// <para>Able to inject custom processors and override default ones.</para>
        /// </summary>
        /// <param name="scriptTemplate">Text file with the script template</param>
        /// <param name="defaulScriptNameWithExtension">Default name with the .cs extension</param>
        /// <param name="customScriptProcessors"><para>Custom processors to apply on the script content.</para><para>Can override default processors.</para><para>Can also recieve default processors to be applied manually.</para></param>
        public static void CreateScriptAssetFromTemplate(TextAsset scriptTemplate, string defaulScriptNameWithExtension, Texture2D customScriptIcon, params Action<DoCreateNewScriptAsset>[] customScriptProcessors)
        {
            CreateScriptAssetFromTemplate(scriptTemplate, defaulScriptNameWithExtension, customScriptIcon, true, customScriptProcessors);
        }

        /// <summary>
        /// <para>Will startup the process of creating a new script from a TXT file template.</para>
        /// <para>Able to inject custom processors and override default ones.</para>
        /// </summary>
        /// <param name="scriptTemplate">Text file with the script template</param>
        /// <param name="defaulScriptNameWithExtension">Default name with the .cs extension</param>
        /// <param name="customScriptProcessors"><para>Custom processors to apply on the script content.</para><para>Can override default processors.</para><para>Can also recieve default processors to be applied manually.</para></param>
        public static void CreateScriptAssetFromTemplate(TextAsset scriptTemplate, string defaulScriptNameWithExtension, params Action<DoCreateNewScriptAsset>[] customScriptProcessors)
        {
            CreateScriptAssetFromTemplate(scriptTemplate, defaulScriptNameWithExtension, DefaultScriptIcon, true, customScriptProcessors);
        }

        /// <summary>
        /// <para>Will startup the process of creating a new script from a TXT file template.</para>
        /// <para>Able to inject custom processors and override default ones.</para>
        /// </summary>
        /// <param name="scriptTemplate">Text file with the script template</param>
        /// <param name="defaulScriptNameWithExtension">Default name with the .cs extension</param>
        /// <param name="applyDefaultProcessors"><para>When false, will only apply custom processors.</para>The default processors can still be applied manually as custom processors.</param>
        /// <param name="customScriptProcessors"><para>Custom processors to apply on the script content.</para><para>Can override default processors.</para><para>Can also recieve default processors to be applied manually.</para></param>
        public static void CreateScriptAssetFromTemplate(TextAsset scriptTemplate, string defaulScriptNameWithExtension, bool applyDefaultProcessors, params Action<DoCreateNewScriptAsset>[] customScriptProcessors)
        {
            CreateScriptAssetFromTemplate(scriptTemplate, defaulScriptNameWithExtension, DefaultScriptIcon, applyDefaultProcessors, customScriptProcessors);
        }

        /// <summary>
        /// <para>Will startup the process of creating a new script from a TXT file template.</para>
        /// <para>Able to inject custom processors and override default ones.</para>
        /// </summary>
        /// <param name="scriptTemplate">Text file with the script template</param>
        /// <param name="defaulScriptNameWithExtension">Default name with the .cs extension</param>
        /// <param name="customScriptIcon">Icon for the new script file.</param>
        /// <param name="applyDefaultProcessors"><para>When false, will only apply custom processors.</para>The default processors can still be applied manually as custom processors.</param>
        /// <param name="customScriptProcessors"><para>Custom processors to apply on the script content.</para><para>Can override default processors.</para><para>Can also recieve default processors to be applied manually.</para></param>
        public static void CreateScriptAssetFromTemplate(TextAsset scriptTemplate, string defaulScriptNameWithExtension, Texture2D customScriptIcon, bool applyDefaultProcessors, params Action<DoCreateNewScriptAsset>[] customScriptProcessors)
        {
            if (scriptTemplate.IsNull())
            { throw new FileNotFoundException("Invalid template file."); }

            if (customScriptIcon == null)
            { customScriptIcon = DefaultScriptIcon; }

            DoCreateNewScriptAsset scriptAssetCreator = CreateInstance<DoCreateNewScriptAsset>();
            scriptAssetCreator.AddStartupData(scriptTemplate, customScriptIcon);
            scriptAssetCreator.AddProcessors(applyDefaultProcessors, customScriptProcessors);
            scriptAssetCreator.AddCurrentContextSelection();

            if (string.IsNullOrEmpty(defaulScriptNameWithExtension))
            {
                defaulScriptNameWithExtension = Path.GetFileNameWithoutExtension(scriptAssetCreator.TemplatePath);
            }

            ProjectWindowUtil.StartNameEditingIfProjectWindowExists(
                0,
                scriptAssetCreator,
                defaulScriptNameWithExtension,
                customScriptIcon,
                scriptAssetCreator.TemplatePath);
        }


        public override void Action(int instanceId, string pathName, string resourceFile)
        {
            AddAssetData(instanceId, pathName, resourceFile);

            //Custom
            foreach (Action<DoCreateNewScriptAsset> processor in _processorList)
            {
                processor.Invoke(this);
            }

            //Default
            if (ApplyDefaultProcessors)
            {
                DefaultProcessors.ExecuteAll(this);
            }
        }

        /// <summary>
        /// Add Types that the script might need using directives
        /// </summary>
        public void AddTypesForUsingDirectives(params Type[] usingTypes)
        {
            _typesList.AddRange(usingTypes);
        }


        private void AddAssetData(int instanceId, string pathName, string resourceFile)
        {
            InstanceID = instanceId;

            FileContent = File.ReadAllText(resourceFile);
            FileDirectory = Path.GetDirectoryName(pathName);
            FileName = Path.GetFileNameWithoutExtension(pathName);
            FileExtension = Path.GetExtension(pathName).Replace(".", "").ToLower();
            RootNamespace = CompilationPipeline.GetAssemblyRootNamespaceFromScriptPath(pathName);
        }

        private void AddStartupData(TextAsset scriptTemplate, Texture2D icon)
        {
            TemplateAsset = scriptTemplate;
            Icon = icon;
        }

        private void AddProcessors(bool disableDefaultProcessors, Action<DoCreateNewScriptAsset>[] customScriptProcessors)
        {
            _processorList.AddRange(customScriptProcessors);
            ApplyDefaultProcessors = disableDefaultProcessors;
        }

        private void AddCurrentContextSelection()
        {
            SelectedObjects = Selection.objects;
        }
    }
}
