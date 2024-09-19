using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpYakusoController : InteractableObjects
{
    [SerializeField]
    private GameObject m_toWarp;
    private float m_timer = 0;
    private const float kWarpTime = 1; //食ってからワープするまで
    private GameObject m_byteSEInstance;
    [SerializeField]
    private AudioClip m_byteSE;
    [SerializeField]
    private AudioClip m_warpSE;
    protected override void DisInteract()
    {
        // タイマーを0に
        m_timer = 0;
    }

    protected override void Interact()
    {
        if (m_byteSEInstance == null)
        {
            m_byteSEInstance = SEGenerator.InstantiateSE(m_byteSE);
        }
        m_timer += Time.fixedDeltaTime;
        Debug.Log(m_timer);
        if (m_timer > kWarpTime)
        {
            m_player.transform.position = m_toWarp.transform.position;
            SEGenerator.InstantiateSE(m_warpSE);
        }
    }
}
