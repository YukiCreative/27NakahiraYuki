using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    private AudioSource m_BGM;
    // Start is called before the first frame update
    void Start()
    {
        m_BGM = GameObject.Find("BGM").GetComponent<AudioSource>();
    }

    private void Update()
    {
        // �L�[�{�[�h�̎󂯕t���͕s�������̂ŃN���b�N�̂ݎ󂯕t����
        Mouse mouse = Mouse.current;

        if (mouse != null && mouse.leftButton.wasPressedThisFrame)
        {
            SceneManager.LoadScene("TitleScene");
        }
    }

    private void FixedUpdate()
    {
        if (!m_BGM.isPlaying)
        {
            SceneManager.LoadScene("TitleScene");
        }
    }
}
