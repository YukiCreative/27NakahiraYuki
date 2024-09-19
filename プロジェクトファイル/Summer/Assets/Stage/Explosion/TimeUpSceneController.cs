using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class TimeUpSceneController : MonoBehaviour
{
    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Keyboard keyboard = Keyboard.current;
        Mouse mouse = Mouse.current;

        if (keyboard != null)
        {
            if (keyboard.spaceKey.wasPressedThisFrame)
            {
                NextScene();
            }
        }

        if (mouse != null)
        {
            if (mouse.leftButton.wasPressedThisFrame)
            {
                NextScene();
            }
        }
    }

    private void NextScene()
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
