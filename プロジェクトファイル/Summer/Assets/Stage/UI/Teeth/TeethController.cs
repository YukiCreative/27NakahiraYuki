using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeethController : MonoBehaviour
{
    public static TeethController instance;
    private RectTransform m_rect;
    private Vector2 kTheethExtendSpeed = new Vector2(0, 0.2f); // 1�t���[���ɉ��X�P�[���L�т邩�Ɏg�p
    private bool m_isYellowLabel = false; // ���F�̒��Ӄ��x�����o�Ă��邩�ǂ���
    [SerializeField]
    private GameObject m_yellowLabel;
    private GameObject m_yellowLabelInstance;
    private bool m_isRedLabel = false; // �Ԃ̊댯���x�����o�Ă��邩�ǂ���
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
        if (m_rect.sizeDelta.y > 500) // �������ȏ�̒����ɂȂ�����
        {
            if (!m_isYellowLabel) // ���F���x���̗L�����m�F
            {
                m_yellowLabelInstance = Instantiate(m_yellowLabel, transform);
                m_isYellowLabel = true;
                // ���ꂪ�댯�ł��邱�Ƃ�m�点��UI���V���E�J��
                m_tyuuiInstance = Instantiate(m_tyuuiUI, m_yellowLabelInstance.transform);
            }
        }
        else
        {
            if (m_isYellowLabel)
            {
                Debug.Log("Destroy�ʂ���");
                Destroy(m_yellowLabelInstance);
                m_isYellowLabel = false;
                // ���ꂪ�댯�ł��邱�Ƃ�m�点��UI���V���E���c
                Destroy(m_tyuuiInstance);
            }
        }

        if (m_rect.sizeDelta.y > 700)
        {
            if (!m_isRedLabel)
            {
                m_redLabelInstance = Instantiate(m_redLabel, transform);
                m_isRedLabel = true;
                // ���ꂪ�댯�ł��邱�Ƃ�m�点��UI���V���E�J��
                m_kikenInstance = Instantiate(m_kikenUI, m_redLabelInstance.transform);
            }
        }
        else
        {
            if (m_isRedLabel)
            {
                Destroy(m_redLabelInstance);
                m_isRedLabel = false;
                // ���ꂪ�댯�ł��邱�Ƃ�m�点��UI���V���E���c
                Destroy(m_kikenInstance);
            }
        }

        if (m_rect.sizeDelta.y > 800)
        {
            m_redLabelInstance.GetComponent<LabelController>().Stop();
            m_yellowLabelInstance.GetComponent<LabelController>().Stop();
            // ��胉�C���𒴂����玀
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
            // �������ŏ��ɂȂ肻���Ȃ�~�߂�
            // �������e�����p���`�Ȃ̂ŁA�v�`�F�b�N(�萔���͂��Ȃ��͗l)
            m_rect.sizeDelta = new Vector2(15, 10);
        }
        if (m_rect.sizeDelta.y > 800)
        {
            // �ő�ɓ͂��Ă������Ŏ~�߂�
            m_rect.sizeDelta = new Vector2(15, 800);
        }
    }

    private IEnumerator TeethDeath()
    {
        // �v���C���[�̓������~�߂�
        PlayerController.s_player.GetComponent<PlayerController>().Stop();
        // ��ʂ��Â�����
        StartCoroutine(FadeManager.instance.FadeOut(120));
        StartCoroutine(m_BGM.ReduceBGM(3));

        yield return new WaitForSeconds(3);

        // �����Ŏc�@���炵���Ⴄ
        GameVariables.s_zankiNum--;
        // �V�[���J��
        SceneManager.LoadScene("Explosion");
    }
}
