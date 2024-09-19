using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCentroller : MonoBehaviour
{
    private Rigidbody2D m_rigidbody2D;
    [SerializeField]
    private AudioClip m_dragSE;
    private GameObject m_SEInstance = null;
    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        //Debug.Log(m_rigidbody2D.velocity.sqrMagnitude);
        if (m_rigidbody2D.velocity.sqrMagnitude > 1)
        {
            // SE‚ÍˆêŒÂ‚¾‚¯o‚é
            if (m_SEInstance == null)
            {
                m_SEInstance = SEGenerator.InstantiateSE(m_dragSE);
            }
        }
    }
}
