using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading;

public class TextBlinking : MonoBehaviour
{
    private int m_timer;
    private const int kBlinkInterval = 60;
    private TextMeshProUGUI m_text;
    // Start is called before the first frame update
    void Start()
    {
        m_text = GetComponent<TextMeshProUGUI>();
    }

    private void FixedUpdate()
    {
        m_timer++;
        // 一定時間ごとに
        if (m_timer > kBlinkInterval)
        {
            m_timer = 0;
            // アクティブかどうかの状態を反転させる
            m_text.enabled = !(m_text.enabled);
        }
    }
}
