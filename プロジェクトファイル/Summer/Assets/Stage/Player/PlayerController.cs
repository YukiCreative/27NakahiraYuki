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
    private float m_idolTime = 0; // ������̎��Ԃ̃^�C�}�[
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

        // WASD�œ�����
        if (keyboard[Key.D].wasPressedThisFrame)
        {
            m_horizontalDirection++;
        }
        if (keyboard[Key.D].wasReleasedThisFrame)
        {
            // ���̈ꕶ�����܂��Ă������Ƃ�
            // �V�[���J�ڑO�ɃL�[�������Ă�����Y�ɔ�������o�O�����������
            // �����P�ǂȃv���C���[���ȒP�Ƀo�O��悤�ɂȂ����̂ŏC��
            // ���������ŃL�[�{�[�h���Ă�̂͗ǂ��Ȃ��@
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

        // �_�b�V������@S�L�[�A�N�V�������͑�l�̎���Ń_�b�V���ł��Ȃ��悤��
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
            // �W�����v����
            if (m_isGrounded)
            {
                m_canJump = true;
            }
        }

        if (keyboard[Key.S].wasPressedThisFrame)
        {
            // �n�ʂ̐H�ו���H�ׂ���A���킦����ł���
            m_munch = true;
        }
        if (keyboard[Key.S].wasReleasedThisFrame)
        {
            m_munch = false;
        }

        // �����������삳��Ă��Ȃ�������
        if (m_horizontalDirection == 0 && m_isGrounded && !m_munch)
        {
            m_idolTime += Time.deltaTime; // �^�C�}�[���Z
        }
        else
        {
            // �łȂ����
            m_idolTime = 0; // ���Z�b�g
        }

        if (m_idolTime > kIdolThreshold) // �����쎞�Ԃ���莞�ԂɒB������A
        {
            // ����A�j���[�V�����𗬂�(�Ȃ�ŃQ�[���Ɋ֌W�Ȃ����Ƃ���������邩�Ȃ�)
            m_animator.SetBool("SitDown", true);
        }
        else
        {
            m_animator.SetBool("SitDown", false);
        }
    }

    private void FixedUpdate()
    {
        // �����ɉ����ăA�j���[�V������ύX
        m_animator.SetInteger("HorizontalDirection", m_horizontalDirection);
        // �����ɉ����Ĉړ�
        if (m_isDash)
        {
            m_rigidbody.AddForce(m_dashSpeed * m_horizontalDirection);
        }
        else
        {
            m_rigidbody.AddForce(m_walkSpeed * m_horizontalDirection);
        }
        // �A�j���[�V�����̃t���O�𑀍�
        m_animator.SetBool("Dash", m_isDash);

        // �ނ���ނ���
        m_animator.SetBool("Munch", m_munch);
        // �W�����v
        if (m_canJump)
        {
            // ����炷
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
        if (inpactSpeed > 40) // 臒l�̓��e�����p���`
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
        // ���̕ӓ����
        // �ڐG���Ă���S�Ă̓_���擾����
        // ���̓_�̊p�x���擾���Ă���
        // �p�x�����͈͓̔��Ȃ�W�����v�ł���@
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

    // �����Α��X�N���v�g�Ɉڂ�����������
    private IEnumerator Death()
    {
        // BGM���~�߂�
        BGMController.StopBGM();
        // �Ȃ�ł���Ő����������ɂȂ�̂���l���킩��Ȃ�
        // ���x�̑傫������1�ɂ����ق��������̂���
        Quaternion angle = Quaternion.FromToRotation(Vector3.right, m_rigidbody.velocity);
        // ���g�̈ʒu�Ɍ��ăG�t�F�N�g��\��
        // �����͎��g�̓����Ă������
        Instantiate(m_gekituiEffect, transform.position, angle);
        m_rigidbody.simulated = false;
        // �c�@��1���炷
        GameVariables.s_zankiNum--;
        string nextLoadScene = string.Empty;
        // �c�@���Ȃ���ԂŎ��񂾂�
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
