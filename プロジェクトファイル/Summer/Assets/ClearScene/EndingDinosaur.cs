using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingDinosaur : MonoBehaviour
{
    private int m_timer = 0;
    private const int kInterval = 5;
    private RectTransform m_rectTransform;
    // Start is called before the first frame update
    void Start()
    {
        m_rectTransform = GetComponent<RectTransform>();
    }

    private void FixedUpdate()
    {
        m_timer++;
        if (m_timer > kInterval)
        {
            m_timer = 0;
            m_rectTransform.DOShakePosition(0.1f, strength : 5, fadeOut: false);
        }
    }
}
