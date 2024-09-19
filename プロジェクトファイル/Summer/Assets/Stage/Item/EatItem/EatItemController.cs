using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatItemController : InteractableObjects
{
    // シリアライズしてエディタからそれぞれの値を設定する感じで
    [SerializeField]
    private int kReducePoint = 100;
    private GameObject m_byteSEInstance;
    [SerializeField]
    private AudioClip m_byteSE;
    [SerializeField]
    private AudioClip m_completeSE;

    protected override void DisInteract()
    {
        // 何もしない
    }

    protected override void Interact()
    {
        // 次第に小さくなる
        // 音を鳴らす
        // カピバラの歯を短くする
        transform.localScale -= new Vector3(0.01f, 0.01f, 0);
        if (m_byteSEInstance == null)
        {
            m_byteSEInstance = SEGenerator.InstantiateSE(m_byteSE);
        }

        if (transform.localScale.x < 0.01f)
        {
            TeethController.instance.ReduceTeethLength(kReducePoint);
            SEGenerator.InstantiateSE(m_completeSE);
            Destroy(gameObject);
        }
    }
}
