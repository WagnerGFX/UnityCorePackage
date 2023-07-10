using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace CorePackage.ScriptTemplates
{
    /// <summary>
    /// Contains all default processors, tag constants and some helper methods.
    /// </summary>
    public static class DefaultProcessors
    {
        /// <summary>
        /// Used in empty lines to block trimming and preserve identation.
        /// </summary>
        public const string TAG_NOTRIM = "#NOTRIM#";

        /// <summary>
        /// Represents the raw file name with no extension or extra processing.
        /// </summary>
        public const string TAG_FILENAME = "#FILENAME#";

        /// <summary>
        /// Represents the file name with no extension or spaces.
        /// </summary>
        public const string TAG_SCRIPT_NAME = "#SCRIPTNAME#";

        /// <summary>
        /// Represents the file name formatted with a Capitalized Style.
        /// </summary>
        public const string TAG_DISPLAY_NAME = "#DISPLAYNAME#";

        /// <summary>
        /// Represents the beginning of the root namespace.
        /// </summary>
        public const string TAG_NAMESPACE_BEGIN = "#ROOTNAMESPACEBEGIN#";

        /// <summary>
        /// Default: Represents the end of the root namespace.
        /// </summary>
        public const string TAG_NAMESPACE_END = "#ROOTNAMESPACEEND#";

        /// <summary>
        /// Represents any using directives that needs to be added.
        /// </summary>
        public const string TAG_USING_DIRECTIVES = "#USINGDIRECTIVES#";

        /// <summary>
        /// Represents the raw name of the first asset that was selected when creating the new script.
        /// </summary>
        public const string TAG_CONTEXT_ASSET_NAME = "#CONTEXTASSETNAME#";

        /// <summary>
        /// Represents the type of the first valid MonoScript asset that was selected when creating the new script.
        /// </summary>
        public const string TAG_CONTEXT_SCRIPT_NAME = "#CONTEXTSCRIPTNAME#";


        /// <summary>
        /// Replaces line endings based on the Editor settings.
        /// <para>Other processors may depend on this. Execute before changing the content.</para>
        /// </summary>
        public static void NormalizeLineEndings(DoCreateNewScriptAsset scriptData)
        {
            string lineEnding = GetLineEndingFromEditorSettings();

            scriptData.FileContent = Regex.Replace(scriptData.FileContent, "\r\n|\r|\n", lineEnding);
        }

        /// <summary>
        /// Removes the tag that blocks trimming.
        /// <para>This is a cleanup processor and should be called after the content has been processed.</para>
        /// <para>Will search for:</para>
        /// <para>- <see cref="TAG_NOTRIM"/></para>
        /// </summary>
        public static void RemoveNoTrim(DoCreateNewScriptAsset scriptData)
        {
            if (!scriptData.FileContent.Contains(TAG_NOTRIM))
            { return; }

            scriptData.FileContent = scriptData.FileContent.Replace(TAG_NOTRIM, "");
        }

        /// <summary>
        /// Inserts the raw file name with no extension or extra processing.
        /// <para>Will search for:</para>
        /// <para>- <see cref="TAG_FILENAME"/></para>
        /// </summary>
        public static void InsertFileName(DoCreateNewScriptAsset scriptData)
        {
            if (!scriptData.FileContent.Contains(TAG_FILENAME))
            { return; }

            scriptData.FileContent = scriptData.FileContent.Replace(TAG_FILENAME, scriptData.FileName);
        }

        /// <summary>
        /// Insert the file name with no extension or spaces.
        /// <para>Will search for:</para>
        /// <para>- <see cref="TAG_SCRIPT_NAME"/></para>
        /// </summary>
        public static void InsertScriptName(DoCreateNewScriptAsset scriptData)
        {
            if (!scriptData.FileContent.Contains(TAG_SCRIPT_NAME))
            { return; }

            scriptData.FileContent = scriptData.FileContent.Replace(TAG_SCRIPT_NAME, scriptData.FileNameNoSpace);
        }

        /// <summary>
        /// Insert the file name formatted with a Capitalized Style.
        /// <para>Will search for:</para>
        /// <para>- <see cref="TAG_DISPLAY_NAME"/></para>
        /// </summary>
        public static void InsertDisplayName(DoCreateNewScriptAsset scriptData)
        {
            if (!scriptData.FileContent.Contains(TAG_DISPLAY_NAME))
            { return; }

            string displayName = Capitalize(scriptData.FileName);

            scriptData.FileContent = scriptData.FileContent.Replace(TAG_DISPLAY_NAME, displayName);
        }

        /// <summary>
        /// Insert the root namespace defined by the Editor or Assembly Definition.
        /// <para>Will search for:</para>
        /// <para>- <see cref="TAG_NAMESPACE_BEGIN"/></para>
        /// <para>- <see cref="TAG_NAMESPACE_END"/></para>
        /// </summary>
        public static void InsertNamespace(DoCreateNewScriptAsset scriptData)
        {
            if (!scriptData.FileContent.Contains(TAG_NAMESPACE_BEGIN) || !scriptData.FileContent.Contains(TAG_NAMESPACE_END))
            { return; }

            string regexNamespaceBegin = $"((\\r\\n)|\\r|\\n)[ \\t]*{TAG_NAMESPACE_BEGIN}[ \\t]*";
            string regexNamespaceEnd = $"((\\r\\n)|\\r|\\n)[ \\t]*{TAG_NAMESPACE_END}[ \\t]*";

            if (string.IsNullOrEmpty(scriptData.RootNamespace))
            {
                scriptData.FileContent = Regex.Replace(scriptData.FileContent, regexNamespaceBegin, string.Empty);
                scriptData.FileContent = Regex.Replace(scriptData.FileContent, regexNamespaceEnd, string.Empty);

                return;
            }

            string indentationString = Regex.Match(scriptData.FileContent, $"(?<=[\\r\\n])([ \\t]*(?={TAG_NAMESPACE_BEGIN}))").Value;
            string lineEnding = scriptData.FileContent.Contains("\r\n") ? "\r\n" : "\n";


            //Edit file line by line
            List<string> contentLines = new(scriptData.FileContent.Split(lineEnding, StringSplitOptions.None));

            int lineIndex = 0;
            for (; lineIndex < contentLines.Count; ++lineIndex)
            {
                if (contentLines[lineIndex].Contains(TAG_NAMESPACE_BEGIN))
                { break; }
            }

            contentLines[lineIndex] = $"namespace {scriptData.RootNamespace}";

            lineIndex++;
            contentLines.Insert(lineIndex, "{");

            lineIndex++;
            for (; lineIndex < contentLines.Count; ++lineIndex)
            {
                string line = contentLines[lineIndex];

                if (string.IsNullOrEmpty(line) || line.Trim().Length == 0)
                { continue; }

                if (line.Contains(TAG_NAMESPACE_END))
                {
                    contentLines[lineIndex] = "}";
                    break;
                }

                contentLines[lineIndex] = $"{indentationString}{line}";
            }

            scriptData.FileContent = string.Join(lineEnding, contentLines.ToArray());
        }

        /// <summary>
        /// Inserts the raw name of the first asset that was selected when creating the new script.
        /// <para>Will search for:</para>
        /// <para>- <see cref="TAG_CONTEXT_ASSET_NAME"/></para>
        /// </summary>
        public static void InsertContextAssetName(DoCreateNewScriptAsset scriptData)
        {
            if (!scriptData.FileContent.Contains(TAG_CONTEXT_ASSET_NAME))
            { return; }

            string contextAssetName = string.Empty;

            foreach (UnityEngine.Object item in scriptData.SelectedObjects)
            {
                if (item.IsValid())
                {
                    contextAssetName = item.name;
                    break;
                }
            }

            scriptData.FileContent = scriptData.FileContent.Replace(TAG_CONTEXT_ASSET_NAME, contextAssetName);
        }

        /// <summary>
        /// Inserts the type name for the first MonoScript asset that was selected when creating the new script.
        /// <para>Will search for:</para>
        /// <para>- <see cref="TAG_CONTEXT_SCRIPT_NAME"/></para>
        /// </summary>
        public static void InsertContextScriptName(DoCreateNewScriptAsset scriptData)
        {
            if (!scriptData.FileContent.Contains(TAG_CONTEXT_SCRIPT_NAME))
            { return; }

            string contextScriptName = string.Empty;

            //Get first valid type and name
            foreach (UnityEngine.Object item in scriptData.SelectedObjects)
            {
                if (item.IsValid() && item is MonoScript script)
                {
                    Type contextScriptType = script.GetClass();

                    if (contextScriptType != null)
                    {
                        contextScriptName = contextScriptType.Name;
                        scriptData.AddTypesForUsingDirectives(contextScriptType);
                        break;
                    }
                }
            }

            scriptData.FileContent = scriptData.FileContent.Replace(TAG_CONTEXT_SCRIPT_NAME, contextScriptName);
        }

        /// <summary>
        /// Inserts using directives for any types informed.
        /// <para>This processor should be called after filling the Type list by other processors.</para>
        /// <para>- <see cref="TAG_USING_DIRECTIVES"/></para>
        /// </summary>
        public static void InsertUsingDirectives(DoCreateNewScriptAsset scriptData)
        {
            if (!scriptData.FileContent.Contains(TAG_USING_DIRECTIVES))
            { return; }

            List<string> namespaceList = new();

            foreach (Type itemType in scriptData.TypesList)
            {
                if (itemType == null)
                { continue; }

                if (string.IsNullOrEmpty(itemType.Namespace))
                { continue; }

                if (itemType.Namespace.Equals(scriptData.RootNamespace))
                { continue; }

                if (namespaceList.Contains(itemType.Namespace))
                { continue; }

                if (scriptData.FileContent.Contains($"using {itemType.Namespace};"))
                { continue; }

                namespaceList.Add(itemType.Namespace);
            }

            string lineEnding = GetLineEndingFromEditorSettings();
            string usingDirectives = string.Empty;


            foreach (string namespaceItem in namespaceList)
            {
                usingDirectives += $"using {namespaceItem};{lineEnding}";
            }

            scriptData.FileContent = Regex.Replace(scriptData.FileContent, $"{TAG_USING_DIRECTIVES}{lineEnding}?", usingDirectives);
        }

        /// <summary>
        /// Save data to disk and import to the Asset Database.
        /// <para>This is the most important processor</para>
        /// </summary>
        public static void SaveFileAndImportAsset(DoCreateNewScriptAsset scriptData)
        {
            if (scriptData.FileAsset.IsValid())
            { return; }

            UTF8Encoding encoding = new(true);
            File.WriteAllText(scriptData.FileFullPath, scriptData.FileContent, encoding);

            AssetDatabase.ImportAsset(scriptData.FilePath);

            scriptData.FileAsset = AssetDatabase.LoadAssetAtPath(scriptData.FilePath, typeof(UnityEngine.Object));
        }

        /// <summary>
        /// Add custom icon to the asset. It will show in the Project Explorer and Inspector.
        /// </summary>
        public static void ChangeAssetIcon(DoCreateNewScriptAsset scriptData)
        {
            if (scriptData.FileAsset.IsNull())
            { return; }

            if (scriptData.Icon.IsNull() || scriptData.Icon == DoCreateNewScriptAsset.DefaultScriptIcon)
            { return; }

            if (AssetImporter.GetAtPath(scriptData.FilePath) is MonoImporter monoImporter)
            {
                EditorGUIUtility.SetIconForObject(scriptData.FileAsset, scriptData.Icon);
                monoImporter.SetIcon(scriptData.Icon);
                monoImporter.SaveAndReimport();
            }
        }

        /// <summary>
        /// Select asset in Project Explorer and show on Inspector
        /// </summary>
        public static void FocusOnAsset(DoCreateNewScriptAsset scriptData)
        {
            if (scriptData.FileAsset.IsNull())
            { return; }

            ProjectWindowUtil.ShowCreatedAsset(scriptData.FileAsset);
        }

        /// <summary>
        /// Execute all default processors in default order.
        /// </summary>
        public static void ExecuteAll(DoCreateNewScriptAsset scriptData)
        {
            //Content - Important
            NormalizeLineEndings(scriptData);

            //Content - Normal
            InsertNamespace(scriptData);
            InsertFileName(scriptData);
            InsertScriptName(scriptData);
            InsertDisplayName(scriptData);
            InsertContextAssetName(scriptData);
            InsertContextScriptName(scriptData);

            //Content - Final and cleanup
            InsertUsingDirectives(scriptData);
            RemoveNoTrim(scriptData);

            //Asset File
            SaveFileAndImportAsset(scriptData);
            ChangeAssetIcon(scriptData);
            FocusOnAsset(scriptData);
        }

        public static string GetLineEndingFromEditorSettings()
        {
            const string LINE_ENGINDS_WIN = "\r\n";
            const string LINE_ENDINGS_UNIX = "\n";

            string lineEnding = EditorSettings.lineEndingsForNewScripts switch
            {
                LineEndingsMode.OSNative => Application.platform == RuntimePlatform.WindowsEditor ? LINE_ENGINDS_WIN : LINE_ENDINGS_UNIX,
                LineEndingsMode.Unix => LINE_ENDINGS_UNIX,
                LineEndingsMode.Windows => LINE_ENGINDS_WIN,
                _ => LINE_ENDINGS_UNIX,
            };
            return lineEnding;
        }

        public static string Capitalize(string value)
        {
            //Add Space to PascalCase
            value = Regex.Replace(value, "(?<=[a-z])(?=[A-Z0-9])", " ");
            value = Regex.Replace(value, "(?<=[0-9])(?=[a-zA-Z])", " ");

            //Replace "_" "-" "." with space, preserve between numbers
            value = Regex.Replace(value, "(?<=[a-zA-Z])(-|_|\\.)(?=[a-zA-Z0-9])", " ");
            value = Regex.Replace(value, "(?<=[a-zA-Z0-9])(-|_|\\.)(?=[a-zA-Z])", " ");

            //Capitalize lower words
            value = Regex.Replace(value, "(?<=^|\\s)[a-z]", match => match.ToString().ToUpper());

            //Capitalize UPPER words
            value = Regex.Replace(value, "(?<=[A-Z])([A-Z]+)", match => match.ToString().ToLower());

            return value;
        }
    }
}
