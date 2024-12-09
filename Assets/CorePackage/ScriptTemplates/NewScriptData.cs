using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace CorePackage.ScriptTemplates
{
    /// <summary>
    /// Contains the basic data to startup the process of creating a new script asset.
    /// </summary>
    public class NewScriptData : INewAssetData
    {
        /// <summary>
        /// Text file with the name, extension and content for the new script asset.
        /// </summary>
        public TextAsset Template { get; private set; }

        /// <summary>
        /// Optional customized Icon. Only applicable to scripts.
        /// <para>Will default to the C# Script icon</para>
        /// </summary>
        public Texture2D Icon { get; private set; }

        /// <summary>
        /// Optional default name and extension.
        /// <para>When undefined, the template file will be used to identify the name and extension</para>
        /// </summary>
        public string DefaultNameWithExtension { get; set; }

        /// <summary>
        /// When false, will only apply custom processors.
        /// <para>The default processors can still be applied manually as custom processors.</para>
        /// </summary>
        public bool ApplyDefaultProcessors { get; set; } = true;

        /// <summary>
        /// <para>Custom processors to apply on the script asset.</para>
        /// <para>Can override default processors.</para>
        /// <para>Can also receive default processors to be applied manually.</para>
        /// </summary>
        public ReadOnlyCollection<Action<DoCreateNewScriptAsset>> CustomProcessors => _customProcessors.AsReadOnly();
        private readonly List<Action<DoCreateNewScriptAsset>> _customProcessors = new();


        /// <summary>
        /// Create some data using the template file and icon.
        /// </summary>
        public NewScriptData(TextAsset template, Texture2D icon)
        {
            if (template.IsNull())
            { throw new FileNotFoundException("Invalid template file."); }

            if(!template.IsAssetFile())
            { throw new FileNotFoundException("Template is not a file."); }

            Template = template;

            //Removes the TextAsset extension, keeping the expected extension, if any.
            DefaultNameWithExtension = Path.GetFileNameWithoutExtension(AssetDatabase.GetAssetPath(template));

            //Default script icon
            if (icon.IsNull())
            {
                icon = EditorGUIUtility.IconContent("cs Script Icon").image as Texture2D;
            }

            Icon = icon;
        }

        public void AddProcessors(params Action<DoCreateNewScriptAsset>[] customProcessors)
        {
            _customProcessors.AddRange(customProcessors);
        }
    }
}
