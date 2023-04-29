using System;
using UnityEngine;

namespace CorePackage.GameObjectGUID
{
    /// <summary>
    /// Simple component to hold the Guid value. Internal behavior is controlled through Editor only classes.
    /// </summary>
    [AddComponentMenu("GUID")]
    [DisallowMultipleComponent]
    public partial class GuidComponent : MonoBehaviour
    {
        [SerializeField] SerializableGuid _instanceGuid;
        public Guid InstanceGuid => _instanceGuid;
    }
}