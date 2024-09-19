using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeManager : MonoBehaviour
{
    private CanvasGroup m_canvasGroup;
    public static FadeManager instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        m_canvasGroup = GetComponent<CanvasGroup>();
        m_canvasGroup.alpha = 1; // エディタでは透明度を1にしているはず
        StartCoroutine(FadeIn(60));
    }

    // インが明るくなるほう
    public IEnumerator FadeIn(int frame)
    {
        float fadeValue = 1.0f / frame;
        while (m_canvasGroup.alpha > 0)
        {
            m_canvasGroup.alpha -= fadeValue;
            yield return null;
        }
    }

    public IEnumerator FadeOut(int frame)
    {
        float fadeValue = 1.0f / frame;
        while (m_canvasGroup.alpha < 1)
        {
            m_canvasGroup.alpha += fadeValue;
            yield return null;
        }
    }
}
