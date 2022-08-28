using System;
using UnityEngine;

namespace CorePackage.ObjectGUIDs
{
    // Author: Searous
    // Link: https://forum.unity.com/threads/cannot-serialize-a-guid-field-in-class.156862/#post-6996680

    /// <summary>
    /// Serializable wrapper for System.Guid. Can be implicitly converted to/from System.Guid.
    /// </summary>
    [Serializable]
    public struct SerializableGuid : ISerializationCallbackReceiver
    {
        [SerializeField] string serializedGuid;

        private Guid guid;
        private const int arbitraryHashNumber = -1324198676;

        public SerializableGuid(Guid guid)
        {
            this.guid = guid;
            serializedGuid = null;
        }

        public void OnAfterDeserialize()
        {
            try
            {
                guid = Guid.Parse(serializedGuid);
            }
            catch
            {
                guid = Guid.Empty;
                Debug.LogWarning($"Attempted to parse invalid GUID string '{serializedGuid}'. GUID will set to System.Guid.Empty");
            }
        }

        public void OnBeforeSerialize()
        {
            serializedGuid = guid.ToString();
        }

        public override bool Equals(object obj)
            => obj is SerializableGuid guid
               && this.guid.Equals(guid.guid);

        public override int GetHashCode() => arbitraryHashNumber + guid.GetHashCode();
        public override string ToString() => guid.ToString();

        public static bool operator ==(SerializableGuid a, SerializableGuid b) => a.guid == b.guid;
        public static bool operator !=(SerializableGuid a, SerializableGuid b) => a.guid != b.guid;
        public static bool operator ==(SerializableGuid a, Guid b) => a.guid == b;
        public static bool operator !=(SerializableGuid a, Guid b) => a.guid != b;
        public static implicit operator SerializableGuid(Guid guid) => new(guid);
        public static implicit operator SerializableGuid(string serializedGuid) => new(Guid.Parse(serializedGuid));
        public static implicit operator Guid(SerializableGuid serializable) => serializable.guid;
        public static implicit operator string(SerializableGuid serializedGuid) => serializedGuid.ToString();
    }
}