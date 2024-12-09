using System;
using System.Collections.ObjectModel;
using UnityEngine;

namespace CorePackage.ScriptTemplates
{
    /// <summary>
    /// Interface for basic data to startup the process of creating a new asset.
    /// </summary>
    public interface INewAssetData
    {
        TextAsset Template { get; }
        Texture2D Icon { get; }
        string DefaultNameWithExtension { get; }
        bool ApplyDefaultProcessors { get; }
        ReadOnlyCollection<Action<DoCreateNewScriptAsset>> CustomProcessors { get; }

        void AddProcessors(params Action<DoCreateNewScriptAsset>[] customProcessors);
    }
}
