using System;
using UnityEngine;

namespace CorePackage.ObjectGUIDs
{
    /// <summary>
    /// Simple component to hold the Guid value. Internal behavior is controlled through Editor only classes.
    /// </summary>
    [DisallowMultipleComponent]
    public partial class ObjectGuid : MonoBehaviour
    {
        [SerializeField] SerializableGuid _instanceGuid;
        public Guid InstanceGuid => _instanceGuid;
    }
}