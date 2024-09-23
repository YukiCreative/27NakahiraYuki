using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ClearSceneManager : MonoBehaviour
{
    private RectTransform m_rectTransform;
    private Vector3 m_moveSpeed = new Vector3(0, 3f, 0);
    private bool m_speedUp = false;
    private int m_waitTimer = 0;
    private const int kWaitTime = 50;
    // Start is called before the first frame update
    void Start()
    {
        m_rectTransform = GameObject.Find("Group").GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        Mouse mouse = Mouse.current;
        if (mouse != null)
        {
            if (mouse.leftButton.wasPressedThisFrame)
            {
                SceneManager.LoadScene("TitleScene");
            }
        }

        Keyboard keyboard = Keyboard.current;
        if (keyboard != null)
        {
            if (keyboard[Key.Space].wasPressedThisFrame)
            {
                m_speedUp = true;
            }
            if (keyboard[Key.Space].wasReleasedThisFrame)
            {
                m_speedUp = false;
            }
        }
    }

    private void FixedUpdate()
    {
        m_waitTimer++;
        if (m_waitTimer < kWaitTime) return;

        // ƒXƒ‰ƒCƒh‚µ‚Ä‚¢‚­‚©
        if (m_rectTransform.localPosition.y < 3000)
        {
            if (m_speedUp)
            {
                m_rectTransform.localPosition += m_moveSpeed * 5;
            }
            else
            {
                m_rectTransform.localPosition += m_moveSpeed;
            }
        }
    }
}
