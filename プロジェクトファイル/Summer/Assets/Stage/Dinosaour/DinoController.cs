using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.UI;

public class DinoController : MonoBehaviour
{
    private const int kInitHp = 500;
    private int m_hp = kInitHp;
    private Animator m_animator;
    private float m_jumpTimer = 0;
    private const int kInitJumpInterval = 3;
    private const int kRandomRange = 2;
    private int m_jumpInterval;
    private Rigidbody2D m_Rigidbody;
    private Vector3 m_jumpPow = new Vector3(0, 5, 0);
    [SerializeField]
    private AudioClip m_audioClip;
    [SerializeField]
    private Slider m_hpSlider;
    [SerializeField]
    private GameObject m_damageNum;
    private GameObject m_canvas;
    public bool m_conMove = false;
    // Start is called before the first frame update
    void Start()
    {
        m_animator = GetComponent<Animator>();
        ResetInterval();
        m_Rigidbody = GetComponent<Rigidbody2D>();
        m_canvas = GameObject.Find("Canvas");
    }

    private void FixedUpdate()
    {
        // ����ł��瓮���Ȃ����
        if (!m_conMove) return;

        // ���E�ɓ������莞�X�W�����v������
        m_jumpTimer += Time.fixedDeltaTime;
        if (m_jumpTimer > m_jumpInterval)
        {
            Jump();
            m_jumpTimer = 0;
            ResetInterval();
        }
    }

    private void ResetInterval()
    {
        m_jumpInterval = kInitJumpInterval + Random.Range(-kRandomRange, kRandomRange);
    }
    public void Damage(int damage)
    {
        // ����ł��玀�ȂȂ����
        if (!m_conMove) return;

        SEGenerator.InstantiateSE(m_audioClip);
        m_hp -= damage;
        float barReducevalue = (float)damage / kInitHp;
        // �_���[�W���o�[�ɔ��f
        ReduceHpBar(barReducevalue);
        // �_���[�W�̐����̃I�u�W�F�N�g�𐶐�
        GameObject instancce = Instantiate(m_damageNum, transform.position, Quaternion.identity, m_canvas.transform);
        instancce.GetComponent<DamageNumberController>().SetNum(damage);
        if (m_hp < 0)
        {
            StartCoroutine(Death());
        }
    }

    private void ReduceHpBar(float value)
    {
        // ��x���s����DO���������Ă���DOValue�����s
        m_hpSlider.DOComplete();
        m_hpSlider.DOValue(m_hpSlider.value - value, 1);
    }

    private IEnumerator Death()
    {
        m_conMove = false;
        // ���b���[�V��������̃G���f�B���O
        m_animator.SetTrigger("Death");
        // �����蔻��̃R���C�_�[����������
        // ������Get���Ă�����
        transform.GetChild(0).gameObject.SetActive(false);

        yield return new WaitForSeconds(3);

        SceneManager.LoadScene("Clear");
    }

    private void Jump()
    {
        m_Rigidbody.AddForce(m_jumpPow, ForceMode2D.Impulse);
    }

    public void SetMove(bool value)
    {
        m_conMove = value;
    }
}
