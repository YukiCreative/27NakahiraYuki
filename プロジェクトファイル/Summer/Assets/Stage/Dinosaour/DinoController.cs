using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Scripting.APIUpdating;

public class DinoController : MonoBehaviour
{
    private int m_hp = 5000;
    private Animator m_animator;
    private float m_jumpTimer = 0;
    private const int kInitJumpInterval = 3;
    private const int kRandomRange = 2;
    private int m_jumpInterval;
    private Rigidbody2D m_Rigidbody;
    private Vector3 m_jumpPow = new Vector3(0, 5, 0);
    [SerializeField]
    private AudioClip m_audioClip;
    // Start is called before the first frame update
    void Start()
    {
        m_animator = GetComponent<Animator>();
        ResetInterval();
        m_Rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        

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
    public IEnumerator Damage(int damage)
    {
        Debug.Log(damage);
        SEGenerator.InstantiateSE(m_audioClip);
        m_hp -= damage;
        if (m_hp < 0)
        {
            // 脂肪モーションからのエンディング
            m_animator.SetTrigger("Death");

            yield return new WaitForSeconds(3);

            SceneManager.LoadScene("Clear");
        }
    }

    private void Jump()
    {
        m_Rigidbody.AddForce(m_jumpPow, ForceMode2D.Impulse);
    }
}
