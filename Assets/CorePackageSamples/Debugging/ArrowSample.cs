using UnityEngine;

namespace CorePackageSamples.Debugging
{
    /// <summary>
    /// Sample with only a target transform. Arrow is drawn in ArrowSampleEditor.
    /// </summary>
    public class ArrowSample : MonoBehaviour
    {
        [SerializeField]
        private Transform _target;
        public Transform Target => _target;
    }
}
