#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CorePackage.ObjectGUIDs
{
    [ExecuteInEditMode]
    public partial class ObjectGuid
    {
        //the list will help to enforce new GUIDs when duplicating GOs
        //The list will only hold values for the current scene
        private static readonly List<string> guidList = new();

        private bool IsPrefab => gameObject.scene.name == null || gameObject.scene.name == gameObject.name;


        private void Awake()
        {
            //Adding the component; Dragging the Prefab to scene
            if (_instanceGuid == Guid.Empty.ToString())
                CreateNewGUID();

            //Duplicating the GO;
            else if (guidList.Contains(_instanceGuid))
                CreateNewGUID();

            //Scene Opened, Undo/Redo
            else
                guidList.Add(_instanceGuid);
        }

        //Called when the Component is reset or added to an existing GO
        private void Reset()
        {
            if (!IsPrefab && _instanceGuid == Guid.Empty.ToString())
                CreateNewGUID();
        }

        private void OnValidate()
        {
            //When the Prefab is created
            if (IsPrefab && _instanceGuid != Guid.Empty.ToString())
                _instanceGuid = Guid.Empty.ToString();
        }

        //Called when the Component or GO is removed, also when the Scene is closed
        private void OnDestroy()
        {
            guidList.Remove(_instanceGuid);
        }

        private void CreateNewGUID()
        {
            _instanceGuid = Guid.NewGuid().ToString();
            guidList.Add(_instanceGuid);
        }
    }
}
#endif
