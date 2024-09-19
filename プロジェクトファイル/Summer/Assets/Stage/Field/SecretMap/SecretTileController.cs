using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SecretTileController : MonoBehaviour
{
    private bool m_inVisible = false;
    private Tilemap m_tilemap;
    private Color m_alpha = new Color(0, 0, 0, 0.01f);
    // Start is called before the first frame update
    void Start()
    {
        m_tilemap = GetComponent<Tilemap>();
    }

    private void FixedUpdate()
    {
        // ƒvƒŒƒCƒ„[‚ªÚG‚·‚é‚Æ‚ä‚Á‚­‚è‚Æ“§‰ß‚µ‚Ä‚¢‚­
        if (m_inVisible)
        {
            if (m_tilemap.color.a > 0)
            {
                m_tilemap.color -= m_alpha;
            }
        }
        else
        {
            if (m_tilemap.color.a < 1)
            {
                m_tilemap.color += m_alpha;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            m_inVisible = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            m_inVisible = false;
        }
    }
}
