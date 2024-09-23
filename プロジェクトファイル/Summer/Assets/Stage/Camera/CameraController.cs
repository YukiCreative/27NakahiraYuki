using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameObject m_player;
    public static CameraController instance;
    private float m_goalPos;
    public bool m_trackPlayer = true;
    // Start is called before the first frame update
    void Start()
    {
        instance = this; // やはりこれに限る
        m_player = GameObject.Find("Player");
        m_goalPos = GameObject.Find("Onsen").transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_trackPlayer) return;
        Vector3 playerPos = m_player.transform.position;
        // プレイヤーの位置が特定の位置以外なら
        if (m_player.transform.position.x >= 0
            && m_player.transform.position.x <= m_goalPos)
        {
            // プレイヤーに追従
            transform.position = new Vector3(playerPos.x, transform.position.y, -10);
        }
    }

    public void CameraMoveDown()
    {
        transform.DOMoveY(-3f, 0.5f);
    }

    public void CameraMoveNomal()
    {
        transform.DOMoveY(0, 0.5f);
    }

    public void CameraMoveUp()
    {
        transform.DOMoveY(2, 0.5f);
    }
}
