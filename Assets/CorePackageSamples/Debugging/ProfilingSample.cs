using CorePackage.Debugging.ProfilerAPI;
using System.Collections;
using UnityEngine;

namespace CorePackageSamples.Debugging
{
    public class ProfilingSample : MonoBehaviour
    {
        public float Result { get; private set; }

        private readonly WaitForSeconds _waitSeconds = new(1f);

        private void Start()
        {
            StartCoroutine(HeavyProcess());
        }

        private IEnumerator HeavyProcess()
        {
            while (true)
            {
                yield return _waitSeconds;

                ScriptWatcher.Begin(this);

                Result = 0f;

                for (int i = 0; i < 1000; i++)
                {
                    Result += Mathf.Sqrt(i) / 7;
                }

                ScriptWatcher.End();
            }
        }
    }
}
