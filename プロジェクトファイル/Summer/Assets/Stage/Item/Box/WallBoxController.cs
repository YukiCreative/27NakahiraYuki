using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBoxController : MonoBehaviour
{
    private Rigidbody2D m_rigid;
    private SpriteRenderer m_spriteRenderer;
    private Color m_alpha = new Color(0, 0, 0, 0.01f);
    // Start is called before the first frame update
    void Start()
    {
        m_rigid = GetComponent<Rigidbody2D>();
        m_spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (m_spriteRenderer.color.a == 1) return;

        m_spriteRenderer.color += m_alpha;
    }
}
