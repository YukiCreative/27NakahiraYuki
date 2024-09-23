using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToothPasteController : MonoBehaviour
{
    // �d�l�F
    // �v���C���[�ɓ��������玕��L�΂�
    // �n�ʂ��ђʂ��A�n�ʈȉ��ɗ������������@

    private TeethController m_teeth;
    private const int kIncreaseTeethValue = 50;
    [SerializeField]
    private AudioClip m_clip;
    // Start is called before the first frame update
    void Start()
    {
        m_teeth = GameObject.Find("Teeth").GetComponent<TeethController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -5)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            SEGenerator.InstantiateSE(m_clip);
            m_teeth.ReduceTeethLength(-kIncreaseTeethValue);
        }
    }
}
