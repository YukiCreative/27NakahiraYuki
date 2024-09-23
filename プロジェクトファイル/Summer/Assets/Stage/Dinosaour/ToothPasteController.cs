using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToothPasteController : MonoBehaviour
{
    // 仕様：
    // プレイヤーに当たったら歯を伸ばす
    // 地面を貫通し、地面以下に落ちたら消える　

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
