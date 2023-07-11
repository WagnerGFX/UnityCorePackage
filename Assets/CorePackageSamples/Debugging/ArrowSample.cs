using UnityEngine;

namespace CorePackageSamples.Debugging
{
    /// <summary>
    /// Sample with only a target transform. Arrow is drawn in ArrowSampleEditor.
    /// </summary>
    public class ArrowSample : MonoBehaviour
    {
        [SerializeField]
        Transform target;
        public Transform Target => target;

    }
}
