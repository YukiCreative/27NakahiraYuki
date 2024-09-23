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
        // 死んでたら動かないよね
        if (!m_canMove) return;

        // 左右に動いたり時々ジャンプしたり
        m_moveTimer += Time.fixedDeltaTime;
        if (m_moveTimer > m_moveDirInterval)
        {
            // ランダムな時間で反転
            m_moveTimer = 0;
            m_moveSpeed *= -1;
            // 時間をランダムに
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
        // たまに野菜が出てくる
        if (Random.Range(0, 6) == 0)
        {
            instance = Instantiate(m_yasai, transform.position, Quaternion.identity);
        }
        else
        {
            instance = Instantiate(m_hamigakiko, transform.position, Quaternion.identity);
        }
        // 攻撃手段として、歯磨き粉を投げる
        // プレイヤーが当たったら歯が伸びる
        
        // 適当に飛ばす
        instance.GetComponent<Rigidbody2D>().AddForce(
            new Vector2(Random.Range(-4, 4), 4), ForceMode2D.Impulse);
        // アニメーション
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
        // 死んでたら死なないよね
        if (!m_canMove) return;

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
        m_canMove = false;
        // 脂肪モーションからのエンディング
        m_animator.SetTrigger("Death");
        // 当たり判定のコライダーを消したい
        // ここでGetしていいか
        transform.GetChild(0).gameObject.SetActive(false);

        yield return new WaitForSeconds(1);

        // BGMと画面をフェードアウトしてからロード
        // フレームと秒数が近藤してて分かりにくい
        // マジで過去の自分に殺されてる　
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
