using UnityEngine;

namespace CorePackage.Singleton
{
    /// <summary>
    /// Component that behaves like a singleton. Any other of the same type will destroy itself. Must be implemented as sealed.
    /// </summary>
    /// <typeparam name="T">The class being implemented.</typeparam>
    public abstract class MonoSingletonBehaviour<T> : MonoBehaviour where T : MonoSingletonBehaviour<T>
    {
        [SerializeField][Tooltip("When active, the GameObject must be at the root of the scene.")]
        private bool _activateDontDestroyOnLoad = true;

        // The object can only be found by searching the scene, preventing static access.
        private static T Instance { get; set; }

        protected void Awake()
        {
            if (Instance == null)
            {

                Instance = this as T;

                if (_activateDontDestroyOnLoad)
                {
                    if (transform.parent == null)
                    { DontDestroyOnLoad(gameObject); }
                    else
                    { Debug.LogWarning($"The MonoSingletonBehaviour {typeof(T)} must be at the scene's root to call DontDestroyOnLoad()!", this); }
                }

                AwakeSingleton();
            }
            else
            {
                gameObject.SetActive(false);
                Destroy(gameObject);
            }
        }

        /// <summary>
        /// Replacement for the Awake() method. Only called when the instance is valid.
        /// </summary>
        protected virtual void AwakeSingleton() { }


        //No need to implement OnDestroy. Instance becomes null when the object gets destroyed.
    }
}
