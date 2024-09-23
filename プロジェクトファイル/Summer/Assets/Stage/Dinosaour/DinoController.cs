using DG.Tweening;
using System.Collections;
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
    public bool m_canMove = false;
    private float m_moveSpeed = 1.0f;
    private float m_moveDirInterval;
    private float m_moveTimer = 0;
    private float m_attackInterval;
    private float m_attackTimer = 0;
    [SerializeField]
    GameObject m_hamigakiko;
    [SerializeField]
    GameObject m_yasai;
    private BGMController m_BossBGM;
    // Start is called before the first frame update
    void Start()
    {
        m_animator = GetComponent<Animator>();
        ResetJumpInterval();
        ResetMoveInterval();
        ResetAttackInterval();
        m_Rigidbody = GetComponent<Rigidbody2D>();
        m_canvas = GameObject.Find("Canvas");
        m_BossBGM = GameObject.Find("BossBGM").GetComponent<BGMController>();
    }

    private void FixedUpdate()
    {
        // ����ł��瓮���Ȃ����
        if (!m_canMove) return;

        // ���E�ɓ������莞�X�W�����v������
        m_moveTimer += Time.fixedDeltaTime;
        if (m_moveTimer > m_moveDirInterval)
        {
            // �����_���Ȏ��ԂŔ��]
            m_moveTimer = 0;
            m_moveSpeed *= -1;
            // ���Ԃ������_����
            ResetMoveInterval();
        }
        transform.Translate(m_moveSpeed * Time.fixedDeltaTime, 0, 0);

        m_jumpTimer += Time.fixedDeltaTime;
        if (m_jumpTimer > m_jumpInterval)
        {
            Jump();
            m_jumpTimer = 0;
            ResetJumpInterval();
        }

        m_attackTimer += Time.fixedDeltaTime;
        if (m_attackTimer > m_attackInterval)
        {
            ResetAttackInterval();
            Attack();
            m_attackTimer = 0;
        }
    }

    private void Attack()
    {
        GameObject instance;
        // ���܂ɖ�؂��o�Ă���
        if (Random.Range(0, 6) == 0)
        {
            instance = Instantiate(m_yasai, transform.position, Quaternion.identity);
        }
        else
        {
            instance = Instantiate(m_hamigakiko, transform.position, Quaternion.identity);
        }
        // �U����i�Ƃ��āA���������𓊂���
        // �v���C���[�����������玕���L�т�
        
        // �K���ɔ�΂�
        instance.GetComponent<Rigidbody2D>().AddForce(
            new Vector2(Random.Range(-4, 4), 4), ForceMode2D.Impulse);
        // �A�j���[�V����
        m_animator.SetTrigger("Attack");
    }

    private void ResetJumpInterval()
    {
        m_jumpInterval = kInitJumpInterval + Random.Range(-kRandomRange, kRandomRange);
    }
    private void ResetMoveInterval()
    {
        m_moveDirInterval = Random.Range(1, 4);
    }
    private void ResetAttackInterval()
    {
        m_attackInterval = Random.Range(1, 4);
    }
    public void Damage(int damage)
    {
        // ����ł��玀�ȂȂ����
        if (!m_canMove) return;

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
        m_canMove = false;
        // ���b���[�V��������̃G���f�B���O
        m_animator.SetTrigger("Death");
        // �����蔻��̃R���C�_�[����������
        // ������Get���Ă�����
        transform.GetChild(0).gameObject.SetActive(false);

        yield return new WaitForSeconds(1);

        // BGM�Ɖ�ʂ��t�F�[�h�A�E�g���Ă��烍�[�h
        // �t���[���ƕb�����ߓ����Ăĕ�����ɂ���
        // �}�W�ŉߋ��̎����ɎE����Ă�@
        StartCoroutine(FadeManager.instance.FadeOut(180));
        StartCoroutine(m_BossBGM.ReduceBGM(3));
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Clear");
    }

    private void Jump()
    {
        m_Rigidbody.AddForce(m_jumpPow, ForceMode2D.Impulse);
    }

    public void SetMove(bool value)
    {
        m_canMove = value;
    }
}
