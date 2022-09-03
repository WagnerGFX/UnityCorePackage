using System.Linq;
using UnityEngine;

namespace CorePackage.Singleton
{
    /// <summary>
    /// ScritableObject that behaves like a Singleton. Issues a warning if there are more instances of the same type. Must be implemented as sealed.
    /// </summary>
    /// <typeparam name="T">The class being implemented.</typeparam>
    public abstract class ScriptableSingletonObject<T> : ScriptableObject where T : ScriptableSingletonObject<T>
    {
        // The object can only be found by searching the scene, preventing the access of a static member.
        private static T Instance { get; set; }

        protected void OnEnable()
        {
            if (Instance == null)
            {
                Instance = Resources.FindObjectsOfTypeAll<T>().FirstOrDefault();
                OnEnableSingleton();
            }
            else if (Resources.FindObjectsOfTypeAll<T>().Count() > 1)
            {
                Debug.LogWarning($"Multiple instances of the ScriptableSingletonObject {typeof(T)} detected!");
            }
        }

        /// <summary>
        /// Replacement for OnEnable(). Only called when the instance is valid.
        /// </summary>
        protected virtual void OnEnableSingleton() { }
    } 
}