using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ZankiNumTextController : MonoBehaviour
{
    private TextMeshProUGUI m_TextMeshProUGUI;
    // Start is called before the first frame update
    void Start()
    {
        // Žc‹@Žæ“¾‚µ‚Ä•\Ž¦‚·‚é‚¾‚¯
        m_TextMeshProUGUI = GetComponent<TextMeshProUGUI>();
        //Debug.Log(m_zankiNum.zanki);
        UpdateText();
    }

    public void UpdateText()
    {
        m_TextMeshProUGUI.text = GameVariables.s_zankiNum.ToString();
    }
}
