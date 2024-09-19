using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeController : MonoBehaviour
{
    private AudioSource m_audioSource;
    // Start is called before the first frame update
    void Start()
    {
        m_audioSource = GetComponent<AudioSource>();
        // PlayOnAwake‚Å‚ÍŒã•t‚¯‚³‚ê‚½SE‚ª‚È‚ç‚È‚¢‚Ì‚ÅPlay()
        m_audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        // Œø‰Ê‰¹‚ªI‚í‚Á‚½‚çÁ‚¦‚é
        if (m_audioSource.isPlaying) return;

        Destroy(gameObject);
    }
}
