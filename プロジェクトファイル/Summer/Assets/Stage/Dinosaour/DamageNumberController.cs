using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageNumberController : MonoBehaviour
{
    private Rigidbody2D m_rigid;
    private const int kDeletePosY = -5;
    private TextMeshProUGUI m_textMeshProUGUI;
    // Start is called before the first frame update
    void Start()
    {
        m_rigid = GetComponent<Rigidbody2D>();
        m_rigid.AddForce(new Vector2(Random.Range(2, 4), Random.Range(2, 4)), ForceMode2D.Impulse);
        m_textMeshProUGUI = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        // ˆê’èƒ‰ƒCƒ“‚ğ’´‚¦‚½‚çÁ‚¦‚é
        if (transform.position.y < kDeletePosY)
        {
            Destroy(gameObject);
        }
    }

    public void SetNum(int damage)
    {
        m_textMeshProUGUI.text = damage.ToString();
    }
}
