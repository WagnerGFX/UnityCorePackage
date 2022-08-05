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
        protected bool ActivateDontDestroyOnLoad = true;

        //When private, the object can only be found by searching the scene (good practice).
        private static T Instance { get; set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this as T;

                if (ActivateDontDestroyOnLoad)
                {
                    if (transform.parent == null)
                        DontDestroyOnLoad(gameObject);
                    else
                        Debug.LogWarning($"The MonoSingletonBehaviour {typeof(T)} must be at the scene's root to call DontDestroyOnLoad()!", this);
                }

                OnAwake();
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
        protected virtual void OnAwake() { }


        //Must Test: if OnDestroy need to clean the Instance when ActivateDontDestroyOnLoad is disabled.
    }
}