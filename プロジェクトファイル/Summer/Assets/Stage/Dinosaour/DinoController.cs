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
        // 死んでたら動かないよね
        if (!m_conMove) return;

        // 左右に動いたり時々ジャンプしたり
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
        // 死んでたら死なないよね
        if (!m_conMove) return;

        SEGenerator.InstantiateSE(m_audioClip);
        m_hp -= damage;
        float barReducevalue = (float)damage / kInitHp;
        // ダメージをバーに反映
        ReduceHpBar(barReducevalue);
        // ダメージの数字のオブジェクトを生成
        GameObject instancce = Instantiate(m_damageNum, transform.position, Quaternion.identity, m_canvas.transform);
        instancce.GetComponent<DamageNumberController>().SetNum(damage);
        if (m_hp < 0)
        {
            StartCoroutine(Death());
        }
    }

    private void ReduceHpBar(float value)
    {
        // 一度実行中のDOを完了してからDOValueを実行
        m_hpSlider.DOComplete();
        m_hpSlider.DOValue(m_hpSlider.value - value, 1);
    }

    private IEnumerator Death()
    {
        m_conMove = false;
        // 脂肪モーションからのエンディング
        m_animator.SetTrigger("Death");
        // 当たり判定のコライダーを消したい
        // ここでGetしていいか
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
