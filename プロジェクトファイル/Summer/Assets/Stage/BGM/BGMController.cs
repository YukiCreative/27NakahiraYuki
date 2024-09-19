using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMController : MonoBehaviour
{
    private static AudioSource s_AudioSource;
    // Start is called before the first frame update
    void Start()
    {
        s_AudioSource = GetComponent<AudioSource>();
    }

    public static void StopBGM()
    {
        s_AudioSource.Stop();
    }

    public static IEnumerator ReduceBGM(float time)
    {
        float factor = 1 / time;
        while (s_AudioSource.volume > 0)
        {
            s_AudioSource.volume -= Time.deltaTime * factor;
            yield return null;
        }
    }
}
