using System;
using UnityEngine;

namespace CorePackage.GameObjectGUID
{
    /// <summary>
    /// Serializable wrapper for System.Guid. Can be implicitly converted to/from System.Guid.
    /// </summary>
    [Serializable]
    public struct SerializableGuid : ISerializationCallbackReceiver
    {
        [SerializeField] private string _serializedGuid;

        private Guid _guid;
        private const int ARBITRARY_HASH_NUMBER = -1324198676;

        public SerializableGuid(Guid guid)
        {
            this._guid = guid;
            _serializedGuid = null;
        }

        public void OnAfterDeserialize()
        {
            try
            {
                _guid = Guid.Parse(_serializedGuid);
            }
            catch
            {
                _guid = Guid.Empty;
                Debug.LogWarning($"Attempted to parse invalid GUID string '{_serializedGuid}'. GUID will set to System.Guid.Empty");
            }
        }

        public void OnBeforeSerialize()
        {
            _serializedGuid = _guid.ToString();
        }

        public override bool Equals(object obj)
        {
            return obj is SerializableGuid guid
                       && this._guid.Equals(guid._guid);
        }

        public override int GetHashCode()
        {
            return ARBITRARY_HASH_NUMBER + _guid.GetHashCode();
        }

        public override string ToString()
        {
            return _guid.ToString();
        }

        public static bool operator ==(SerializableGuid a, SerializableGuid b)
        {
            return a._guid == b._guid;
        }

        public static bool operator !=(SerializableGuid a, SerializableGuid b)
        {
            return a._guid != b._guid;
        }

        public static bool operator ==(SerializableGuid a, Guid b)
        {
            return a._guid == b;
        }

        public static bool operator !=(SerializableGuid a, Guid b)
        {
            return a._guid != b;
        }

        public static implicit operator SerializableGuid(Guid guid)
        {
            return new(guid);
        }

        public static implicit operator SerializableGuid(string serializedGuid)
        {
            return new(Guid.Parse(serializedGuid));
        }

        public static implicit operator Guid(SerializableGuid serializable)
        {
            return serializable._guid;
        }

        public static implicit operator string(SerializableGuid serializedGuid)
        {
            return serializedGuid.ToString();
        }
    }
}
