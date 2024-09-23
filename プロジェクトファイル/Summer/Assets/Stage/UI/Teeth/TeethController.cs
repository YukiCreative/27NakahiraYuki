using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeethController : MonoBehaviour
{
    public static TeethController instance;
    private RectTransform m_rect;
    private Vector2 kTheethExtendSpeed = new Vector2(0, 0.2f); // 1フレームに何スケール伸びるかに使用
    private bool m_isYellowLabel = false; // 黄色の注意ラベルが出ているかどうか
    [SerializeField]
    private GameObject m_yellowLabel;
    private GameObject m_yellowLabelInstance;
    private bool m_isRedLabel = false; // 赤の危険ラベルが出ているかどうか
    [SerializeField]
    private GameObject m_redLabel;
    private GameObject m_redLabelInstance;
    private GameObject m_canvas;
    [SerializeField]
    private GameObject m_tyuuiUI;
    private GameObject m_tyuuiInstance;
    [SerializeField]
    private GameObject m_kikenUI;
    private GameObject m_kikenInstance;
    private bool m_isDeath = false;
    private BGMController m_BGM;
    // Start is called before the first frame update
    void Start()
    {
        m_BGM = GameObject.Find("BGM").GetComponent<BGMController>();
        instance = this;
        m_rect = GetComponent<RectTransform>();
        m_canvas = GameObject.Find("Canvas");
    }

    private void FixedUpdate()
    {
        if (m_isDeath) return;

        m_rect.sizeDelta += kTheethExtendSpeed;
        if (m_rect.sizeDelta.y > 500) // 歯が一定以上の長さになったら
        {
            if (!m_isYellowLabel) // 黄色ラベルの有無を確認
            {
                m_yellowLabelInstance = Instantiate(m_yellowLabel, transform);
                m_isYellowLabel = true;
                // これが危険であることを知らせるUIをショウカン
                m_tyuuiInstance = Instantiate(m_tyuuiUI, m_yellowLabelInstance.transform);
            }
        }
        else
        {
            if (m_isYellowLabel)
            {
                Debug.Log("Destroy通った");
                Destroy(m_yellowLabelInstance);
                m_isYellowLabel = false;
                // これが危険であることを知らせるUIをショウメツ
                Destroy(m_tyuuiInstance);
            }
        }

        if (m_rect.sizeDelta.y > 700)
        {
            if (!m_isRedLabel)
            {
                m_redLabelInstance = Instantiate(m_redLabel, transform);
                m_isRedLabel = true;
                // これが危険であることを知らせるUIをショウカン
                m_kikenInstance = Instantiate(m_kikenUI, m_redLabelInstance.transform);
            }
        }
        else
        {
            if (m_isRedLabel)
            {
                Destroy(m_redLabelInstance);
                m_isRedLabel = false;
                // これが危険であることを知らせるUIをショウメツ
                Destroy(m_kikenInstance);
            }
        }

        if (m_rect.sizeDelta.y > 800)
        {
            m_redLabelInstance.GetComponent<LabelController>().Stop();
            m_yellowLabelInstance.GetComponent<LabelController>().Stop();
            // 一定ラインを超えたら死
            m_isDeath = true;
            StartCoroutine(TeethDeath());
        }
    }

    public void ReduceTeethLength(float value)
    {
        Vector2 temp = new Vector2(0, value);
        m_rect.sizeDelta = m_rect.sizeDelta - temp;
        Vector3 temp2 = new Vector3(0, value, 0);
        //Debug.Log(temp2);
        if (m_yellowLabelInstance != null)
        {
            m_yellowLabelInstance.GetComponent<RectTransform>().localPosition += temp2;
        }
        if (m_redLabelInstance != null)
        {
            m_redLabelInstance.GetComponent<RectTransform>().localPosition += temp2;
        }
        if (m_rect.sizeDelta.y < 10)
        {
            // 高さが最小になりそうなら止める
            // ここリテラルパンチなので、要チェック(定数化はしない模様)
            m_rect.sizeDelta = new Vector2(15, 10);
        }
        if (m_rect.sizeDelta.y > 800)
        {
            // 最大に届いてもそこで止める
            m_rect.sizeDelta = new Vector2(15, 800);
        }
    }

    private IEnumerator TeethDeath()
    {
        // プレイヤーの動きを止める
        PlayerController.s_player.GetComponent<PlayerController>().Stop();
        // 画面を暗くする
        StartCoroutine(FadeManager.instance.FadeOut(120));
        StartCoroutine(m_BGM.ReduceBGM(3));

        yield return new WaitForSeconds(3);

        // ここで残機減らしちゃう
        GameVariables.s_zankiNum--;
        // シーン遷移
        SceneManager.LoadScene("Explosion");
    }
}
