using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    private Animator m_animator;
    private int m_horizontalDirection = 0;
    private float m_idolTime = 0; // 無操作の時間のタイマー
    private const float kIdolThreshold = 10;
    private Rigidbody2D m_rigidbody;
    private Vector3 m_walkSpeed = new Vector3(6, 0, 0);
    private Vector3 m_dashSpeed = new Vector3(9, 0, 0);
    private Vector3 m_jumpPower = new Vector3(0, 6, 0);
    private bool m_isGrounded = false;
    private bool m_canJump = false;
    private GameObject m_gekituiEffect;
    private bool m_munch = false;
    [SerializeField]
    private AudioClip m_jumpSE;
    [SerializeField]
    private AudioClip m_landingSE;
    [SerializeField]
    private AudioClip m_strongLandingSE;
    private bool m_isDash = false;
    public static GameObject s_player;
    public static GameObject s_mouth;
    private HingeJoint2D m_mouseHinge;
    private DistanceJoint2D m_mouseDistance;
    private HingeJoint2D m_playerHinge;
    private (int, int) m_jumpAbleAngle = (45, 135);
    public bool m_canMove = true;
    // Start is called before the first frame update
    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_gekituiEffect = (GameObject)Resources.Load("GekituiEffect");
        s_player = gameObject;
        s_mouth = transform.GetChild(0).gameObject;
        m_playerHinge = GetComponent<HingeJoint2D>();
        m_mouseHinge = s_mouth.GetComponent<HingeJoint2D>();
        m_mouseDistance = s_mouth.GetComponent<DistanceJoint2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_canMove) return;

        Keyboard keyboard = Keyboard.current;
        if (keyboard == null) return;

        // WASDで動かす
        if (keyboard[Key.D].wasPressedThisFrame)
        {
            m_horizontalDirection++;
        }
        if (keyboard[Key.D].wasReleasedThisFrame)
        {
            // この一文をかませておくことで
            // シーン遷移前にキーを押していた野郎に発生するバグが解消される
            // だが善良なプレイヤーも簡単にバグるようになったので修正
            // 多分ここでキーボード見てるのは良くない　
            if (!(m_horizontalDirection == 0 && !keyboard[Key.A].isPressed))
            {
                m_horizontalDirection--;
            }
        }
        if (keyboard[Key.A].wasPressedThisFrame)
        {
            m_horizontalDirection--;
        }
        if (keyboard[Key.A].wasReleasedThisFrame)
        {
            if (!(m_horizontalDirection == 0 && !keyboard[Key.D].isPressed))
            {
                m_horizontalDirection++;
            }
        }

        // ダッシュ判定　Sキーアクション中は大人の事情でダッシュできないように
        if ((keyboard[Key.LeftShift].isPressed || keyboard[Key.RightShift].isPressed) && !m_munch)
        {
            m_isDash = true;
        }
        else
        {
            m_isDash = false;
        }
        
        if (keyboard[Key.Space].wasPressedThisFrame || keyboard[Key.W].wasPressedThisFrame)
        {
            // ジャンプする
            if (m_isGrounded)
            {
                m_canJump = true;
            }
        }

        if (keyboard[Key.S].wasPressedThisFrame)
        {
            // 地面の食べ物を食べたり、くわえたりできる
            m_munch = true;
        }
        if (keyboard[Key.S].wasReleasedThisFrame)
        {
            m_munch = false;
        }

        // もし何も操作されていなかったら
        if (m_horizontalDirection == 0 && m_isGrounded && !m_munch)
        {
            m_idolTime += Time.deltaTime; // タイマー加算
        }
        else
        {
            // でなければ
            m_idolTime = 0; // リセット
        }

        if (m_idolTime > kIdolThreshold) // 無操作時間が一定時間に達したら、
        {
            // 座るアニメーションを流す(なんでゲームに関係ないことから実装するかなあ)
            m_animator.SetBool("SitDown", true);
        }
        else
        {
            m_animator.SetBool("SitDown", false);
        }
    }

    private void FixedUpdate()
    {
        // 向きに応じてアニメーションを変更
        m_animator.SetInteger("HorizontalDirection", m_horizontalDirection);
        // 向きに応じて移動
        if (m_isDash)
        {
            m_rigidbody.AddForce(m_dashSpeed * m_horizontalDirection);
        }
        else
        {
            m_rigidbody.AddForce(m_walkSpeed * m_horizontalDirection);
        }
        // アニメーションのフラグを操作
        m_animator.SetBool("Dash", m_isDash);

        // むしゃむしゃ
        m_animator.SetBool("Munch", m_munch);
        // ジャンプ
        if (m_canJump)
        {
            // 音を鳴らす
            SEGenerator.InstantiateSE(m_jumpSE);
            m_rigidbody.AddForce(m_jumpPower, ForceMode2D.Impulse);
            m_canJump = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        string tag = collision.tag;
        if (tag == "Death")
        {
            StartCoroutine(Death());
        }
        else if (tag == "CameraMoveDown")
        {
            CameraController.instance.CameraMoveDown();
        }
        else if (tag == "CameraMoveUp")
        {
            CameraController.instance.CameraMoveUp();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        float inpactSpeed = collision.relativeVelocity.sqrMagnitude;
        //Debug.Log(inpactSpeed);
        if (inpactSpeed > 40) // 閾値はリテラルパンチ
        {
            SEGenerator.InstantiateSE(m_strongLandingSE);
        }
        else if (inpactSpeed > 14)
        {
            SEGenerator.InstantiateSE(m_landingSE);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // この辺難しいな
        // 接触している全ての点を取得して
        // その点の角度を取得している
        // 角度が一定の範囲内ならジャンプできる　
        int contacts = collision.contactCount;
        for (int i = 0; i < contacts; ++i)
        {
            ContactPoint2D point = collision.GetContact(i);
            //Debug.Log(Vector2.Angle(Vector2.right, point.normal));
            float contactAngle = Vector2.Angle(Vector2.right, point.normal);
            if (contactAngle >= m_jumpAbleAngle.Item1 && contactAngle <= m_jumpAbleAngle.Item2)
            {
                m_isGrounded = true;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        m_isGrounded = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        string tag = collision.tag;
        if (tag == "CameraMoveDown")
        {
            CameraController.instance.CameraMoveNomal();
        }
    }

    // これ絶対他スクリプトに移した方がいい
    private IEnumerator Death()
    {
        // BGMを止める
        BGMController.StopBGM();
        // なんでこれで正しい向きになるのかわ僕もわからなぃ
        // 速度の大きさって1にしたほうがいいのかな
        Quaternion angle = Quaternion.FromToRotation(Vector3.right, m_rigidbody.velocity);
        // 自身の位置に撃墜エフェクトを表示
        // 向きは自身の動いている向き
        Instantiate(m_gekituiEffect, transform.position, angle);
        m_rigidbody.simulated = false;
        // 残機を1減らす
        GameVariables.s_zankiNum--;
        string nextLoadScene = string.Empty;
        // 残機がない状態で死んだら
        if (GameVariables.s_zankiNum < 0)
        {
            nextLoadScene = "GameOverScene";
        }
        else
        {
            nextLoadScene = "ZankiDispScene";
        }

        yield return new WaitForSeconds(3);

        SceneManager.LoadScene(nextLoadScene);
    }
}
