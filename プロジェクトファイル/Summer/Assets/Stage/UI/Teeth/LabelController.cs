using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabelController : MonoBehaviour
{
    private RectTransform m_RectTransform;
    private Vector3 kmoveSpeed = new Vector3(0, -0.2f);
    private bool m_stop = false;
    private Vector3 kFirstPosition = new Vector3(-7.392f, -0.5f, 0);
    // Start is called before the first frame update
    void Start()
    {
        m_RectTransform = GetComponent<RectTransform>();
        //Debug.Log("’Ê‚Á‚½‚æ");
        m_RectTransform.localPosition = kFirstPosition;
        //Debug.Log(m_RectTransform.localPosition);
    }

    private void FixedUpdate()
    {
        if (m_stop) return;
        m_RectTransform.localPosition += kmoveSpeed;
    }

    public void Stop()
    {
        m_stop = true;
    }
}
