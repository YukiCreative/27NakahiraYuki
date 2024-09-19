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
        // PlayOnAwake�ł͌�t�����ꂽSE���Ȃ�Ȃ��̂�Play()
        m_audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        // ���ʉ����I������������
        if (m_audioSource.isPlaying) return;

        Destroy(gameObject);
    }
}
