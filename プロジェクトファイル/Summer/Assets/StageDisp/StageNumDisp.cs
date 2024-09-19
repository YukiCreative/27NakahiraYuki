using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class StageNumDisp : MonoBehaviour
{
    private TextMeshProUGUI m_TextMeshProUGUI;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        m_TextMeshProUGUI = GetComponent<TextMeshProUGUI>();
        m_TextMeshProUGUI.text = GameVariables.s_stageNum.ToString();

        yield return new WaitForSeconds(2);

        SceneManager.LoadScene("ZankiDispScene");
    }
}
