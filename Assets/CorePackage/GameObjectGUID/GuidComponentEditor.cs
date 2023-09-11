#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CorePackage.GameObjectGUID
{
    // Q: Why not use the UnityEditor.Editor class?
    // A: Because the Editor's events and lifecycle are more related to the Inspector than to the Component itself.
    // Code is heavilly commented to track the unintuitive lifecycle of the Unity Editor.
    // Many comments and empty code paths are there to be easily replaceable with logs.
    [ExecuteAlways]
    public partial class GuidComponent
    {
        // The list will help to enforce new GUIDs when duplicating GOs
        // The list will only hold values for the current scene
        private static readonly List<string> _guidList = new();

        private bool IsPrefab => gameObject.scene.name == null || gameObject.scene.name == gameObject.name;

        private bool IsSceneLoaded => gameObject.scene.isLoaded;

        private bool _isAwake = false;

        private Guid _backupGuid;


        private void Awake()
        {
            if (Application.isPlaying || _isAwake)
            { return; }

            if (IsPrefab)
            {
                //Prefab opened
            }
            else if (_instanceGuid == Guid.Empty)
            {
                //Component added to GameObject; Prefab added to scene
                CreateNewGUID();
            }
            else if (_guidList.Contains(_instanceGuid))
            {
                //Duplicated the GameObject
                CreateNewGUID();
            }
            else
            {
                //Scene Opened; Delete Undo/Redo
                LoadGUID();
            }

            _isAwake = true;
        }

        private void Reset()
        {
            if (Application.isPlaying)
            { return; }

            if (!IsPrefab && _instanceGuid == Guid.Empty && _backupGuid != Guid.Empty)
            {
                //Component Reset
                RestoreGUID();
            }
        }

        private void OnValidate()
        {
            if (Application.isPlaying)
            { return; }

            //Scene Opened: Prevents validating instances before awaking
            if (!_isAwake)
            {
                Awake();
            }

            if (IsPrefab)
            {
                if (_instanceGuid != Guid.Empty)
                {
                    //Prefab created; Applied to prefab; Edited prefab
                    ClearGUID();
                }
            }
            else if (_instanceGuid == Guid.Empty)
            {
                //Prefab instance reverted
                RestoreGUID();
            }
            else if (_instanceGuid != _backupGuid)
            {
                if (_backupGuid == Guid.Empty)
                {
                    //Project Recompiled
                    LoadGUID();
                }
                else
                {
                    //New GUID Button; Edit Undo/Redo
                    LoadGUID();
                }
            }
        }

        private void OnDestroy()
        {
            if (Application.isPlaying)
            { return; }

            if (IsPrefab)
            {
                //Prefab closed
            }
            else if (IsSceneLoaded)
            {
                //Component or GO removed
                _guidList.Remove(_instanceGuid);
            }
            else
            {
                //Scene closed
                _guidList.Remove(_instanceGuid);
            }
        }


        private void ClearGUID()
        {
            _instanceGuid = Guid.Empty;
            _backupGuid = _instanceGuid;
        }

        private void CreateNewGUID()
        {
            _instanceGuid = Guid.NewGuid();
            _backupGuid = _instanceGuid;
            _guidList.Add(_instanceGuid);
        }

        private void LoadGUID()
        {
            if (_backupGuid != Guid.Empty)
            {
                _guidList.Remove(_backupGuid.ToString());
            }

            _backupGuid = _instanceGuid;
            _guidList.Add(_instanceGuid);
        }

        private void RestoreGUID()
        {
            _instanceGuid = _backupGuid;
        }
    }
}
#endif
