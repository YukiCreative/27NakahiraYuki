using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMController : MonoBehaviour
{
    private AudioSource m_AudioSource;
    // Start is called before the first frame update
    void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }

    public void StopBGM()
    {
        m_AudioSource.Stop();
    }

    public IEnumerator ReduceBGM(float time)
    {
        float factor = 1 / time;
        while (m_AudioSource.volume > 0)
        {
            m_AudioSource.volume -= Time.deltaTime * factor;
            yield return null;
        }
    }
}
