using CorePackage.Utilities.ScriptTemplates;
using System;
using System.Collections.ObjectModel;
using UnityEngine;

namespace CorePackage.Utilities
{
    /// <summary>
    /// Interface for basic data to startup the process of creating a new asset.
    /// </summary>
    public interface INewAssetData
    {
        TextAsset Template { get; }
        Texture2D Icon { get; }
        string DefaulNameWithExtension { get; }
        bool ApplyDefaultProcessors { get; }
        ReadOnlyCollection<Action<DoCreateNewScriptAsset>> CustomProcessors { get; }

        void AddProcessors(params Action<DoCreateNewScriptAsset>[] customProcessors);
    }
}
