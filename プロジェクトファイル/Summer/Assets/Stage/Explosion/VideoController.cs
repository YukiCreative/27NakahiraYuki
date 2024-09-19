using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoController : MonoBehaviour
{
    private VideoPlayer m_VideoPlayer;
    // Start is called before the first frame update
    void Start()
    {
        m_VideoPlayer = GetComponent<VideoPlayer>();
        m_VideoPlayer.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_VideoPlayer.isPlaying)
        {
            if (GameVariables.s_zankiNum < 0)
            {
                SceneManager.LoadScene("GameOverScene");
            }
            else
            {
                SceneManager.LoadScene("ZankiDispScene");
            }
        }
    }
}
