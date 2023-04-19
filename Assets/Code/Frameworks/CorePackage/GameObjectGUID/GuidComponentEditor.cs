#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CorePackage.GameObjectGUID
{
    // Why not use the UnityEditor.Editor class?
    // Because the Editor's events and lifecycle are more related to the Inspector than to the Component itself.
    [ExecuteInEditMode]
    public partial class GuidComponent
    {
        // The list will help to enforce new GUIDs when duplicating GOs
        // The list will only hold values for the current scene
        private static readonly List<string> guidList = new();

        private bool IsPrefab => gameObject.scene.name == null || gameObject.scene.name == gameObject.name;

        private Guid _backupGuid;


        private void Awake()
        {
            // Component added to GO; Prefab added to scene
            if (_instanceGuid == Guid.Empty)
            {
                CreateNewGUID();
            }

            // Duplicated the GO
            else if (guidList.Contains(_instanceGuid))
            {
                CreateNewGUID();
            }

            // Scene Opened; Delete Undo/Redo
            else
            {
                LoadGUID();
            }
        }

        // Called when the Component is reset or added to an existing GO
        private void Reset()
        {
            if (!IsPrefab && _instanceGuid == Guid.Empty)
            {
                // Componenet Reset
                if (_backupGuid != Guid.Empty)
                {
                    RestoreGUID();
                }

                // Component added to GO
                else
                {
                    CreateNewGUID();
                }
            }
        }

        private void OnValidate()
        {
            // Prefab created
            if (IsPrefab && _instanceGuid != Guid.Empty)
            {
                ClearGUID();
            }

            // Prefab instance Reverted
            else if (_instanceGuid == Guid.Empty)
            {
                RestoreGUID();
            }

            // New GUID Button; Edit Undo/Redo
            if (!IsPrefab && _instanceGuid != Guid.Empty && _instanceGuid != _backupGuid)
            {
                LoadGUID();
            }
        }

        // Component or GO removed; Scene closed
        private void OnDestroy()
        {
            guidList.Remove(_instanceGuid);
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
            guidList.Add(_instanceGuid);
        }

        private void LoadGUID()
        {
            if (_backupGuid != Guid.Empty)
            {
                guidList.Remove(_backupGuid.ToString());
            }

            _backupGuid = _instanceGuid;
            guidList.Add(_instanceGuid);
        }

        private void RestoreGUID()
        {
            _instanceGuid = _backupGuid;
        }
    }
}
#endif
