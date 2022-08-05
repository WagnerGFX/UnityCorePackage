using System.Collections;
using UnityEngine;
using CorePackage.Debugging.ProfilerAPI;

public class ProfilingSample : MonoBehaviour
{
    private WaitForSeconds waitSeconds = new(1f);

    void Start()
    {
        StartCoroutine(HeavyProcess());
    }

    IEnumerator HeavyProcess()
    {
        while (true)
        {
            yield return waitSeconds;

            ScriptWatcher.Begin(this);

            float result = 0f;

            for (int i = 0; i < 1000; i++)
            {
                result = Mathf.Sqrt(i) / 7;
            }

            ScriptWatcher.End();
        }
    }
}
