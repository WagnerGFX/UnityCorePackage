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
        private static readonly List<string> guidList = new();

        private bool IsPrefab => gameObject.scene.name == null || gameObject.scene.name == gameObject.name;

        private bool IsSceneLoaded => gameObject.scene.isLoaded;

        private bool m_isAwake = false;

        private Guid m_backupGuid;


        private void Awake()
        {
            if (Application.isPlaying || m_isAwake)
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
            else if (guidList.Contains(_instanceGuid))
            {
                //Duplicated the GameObject
                CreateNewGUID();
            }
            else
            {
                //Scene Opened; Delete Undo/Redo
                LoadGUID();
            }

            m_isAwake = true;
        }

        private void Reset()
        {
            if (Application.isPlaying)
            { return; }

            if (!IsPrefab && _instanceGuid == Guid.Empty && m_backupGuid != Guid.Empty)
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
            if (!m_isAwake)
            {
                Awake();
            }

            if (IsPrefab)
            {
                if(_instanceGuid != Guid.Empty)
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
            else if (_instanceGuid != m_backupGuid)
            {
                if (m_backupGuid == Guid.Empty)
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
                guidList.Remove(_instanceGuid);
            }
            else
            {
                //Scene closed
                guidList.Remove(_instanceGuid);
            }
        }


        private void ClearGUID()
        {
            _instanceGuid = Guid.Empty;
            m_backupGuid = _instanceGuid;
        }

        private void CreateNewGUID()
        {
            _instanceGuid = Guid.NewGuid();
            m_backupGuid = _instanceGuid;
            guidList.Add(_instanceGuid);
        }

        private void LoadGUID()
        {
            if (m_backupGuid != Guid.Empty)
            {
                guidList.Remove(m_backupGuid.ToString());
            }

            m_backupGuid = _instanceGuid;
            guidList.Add(_instanceGuid);
        }

        private void RestoreGUID()
        {
            _instanceGuid = m_backupGuid;
        }
    }
}
#endif
