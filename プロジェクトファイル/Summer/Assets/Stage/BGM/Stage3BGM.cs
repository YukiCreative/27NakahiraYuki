using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Stage3BGM : MonoBehaviour
{
    private AudioSource m_fieldBGM;
    private AudioSource m_bossBGM;
    private void Start()
    {
        m_fieldBGM = GameObject.Find("BGM").GetComponent<AudioSource>();
        m_bossBGM = GameObject.Find("BossBGM").GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        if (PlayerController.s_player.transform.position.x < 25 || PlayerController.s_player.transform.position.x > 48) return;

        float fieldVolume = 1 - (PlayerController.s_player.transform.position.x - 25) * 0.1f;
        if (fieldVolume < 0 )
        {
            // ˆê‰ž0ˆÈ‰º‚É‚Í‚È‚ç‚È‚¢‚æ‚¤‚É
            fieldVolume = 0;
        }
        m_fieldBGM.volume = fieldVolume;
        float bossVolume = (PlayerController.s_player.transform.position.x - 38) * 0.1f;
        if (bossVolume < 0)
        {
            // “¯‚¶‚­
            bossVolume = 0;
        }
        m_bossBGM.volume = bossVolume;
    }
}
