using System;
using UnityEngine;

namespace CorePackage.ObjectGUIDs
{
    [DisallowMultipleComponent]
    public partial class ObjectGuid : MonoBehaviour
    {
        [SerializeField] SerializableGuid _instanceGuid;
        public Guid InstanceGuid => _instanceGuid;
    }
}