using System;
using UnityEngine;

// TODO: Review git changes and commit
// TODO: Review template files for any issues
// TODO: Decide if builders and templates should be samples (recommended)
// TODO: Move files and commit
// TODO: Add sample of "Class of..." or anything with validation

namespace CorePackage.Utilities.ScriptTemplates
{
    public abstract class ScriptBuilderBase<T> : ScriptableObject, IDisposable where T : ScriptBuilderBase<T>
    {
        public const int MENU_SCRIPTS_PRIORITY = -1;
        public const string MENU_SCRIPTS_NAME = "Assets/Add Script/";

        [field: SerializeField]
        public TextAsset Template { get; private set; }

        [field: SerializeField]
        public Texture2D Icon { get; private set; }

        /// <summary>
        /// Auto apply the Template and Icon. Script name is inferred from the template file name.
        /// </summary>
        public static void NewScriptDefault()
        {
            using T instance = CreateInstance();
            DoCreateNewScriptAsset.CreateScriptAssetFromTemplate(instance.Template, instance.Icon);
        }

        public static T CreateInstance()
        {
            return CreateInstance<T>();
        }

        public void Dispose()
        {
            //By default the Editor does not clear loose objects created in code. IDisposable fixes this.
            DestroyImmediate(this);
        }
    }
}