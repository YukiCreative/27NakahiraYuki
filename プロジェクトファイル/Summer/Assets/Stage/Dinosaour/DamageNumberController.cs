using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageNumberController : MonoBehaviour
{
    private Rigidbody2D m_rigid;
    private const int kDeletePosY = -5;
    private TextMeshProUGUI m_textMeshProUGUI;

    private void Awake()
    {
        m_rigid = GetComponent<Rigidbody2D>();
        m_rigid.AddForce(new Vector2(Random.Range(-4f, 4f), Random.Range(2f, 4f)), ForceMode2D.Impulse);
        m_textMeshProUGUI = GetComponent<TextMeshProUGUI>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // àÍíËÉâÉCÉìÇí¥Ç¶ÇΩÇÁè¡Ç¶ÇÈ
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
