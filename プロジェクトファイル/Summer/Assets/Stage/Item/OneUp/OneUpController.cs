using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneUpController : InteractableObjects
{
    // 基本的にはEatItemと一緒
    // 食べ終わったら残機が設定された値回復　
    [SerializeField]
    private int m_increaseZanki = 1;
    [SerializeField]
    private AudioClip m_byteSE;
    [SerializeField]
    private AudioClip m_oneUpSE;
    private Vector3 m_reduceValue = new Vector3(0.01f, 0.01f, 0);
    private GameObject m_SEInstace;
    [SerializeField]
    private int m_boolIndex = 0;

    protected override void Start()
    {
        base.Start();
        // 自分に割り当てられた番号のbool値を見て、falseなら退場
        if (!GameVariables.s_OneUpCornFlag[m_boolIndex])
        {
            Destroy(gameObject);
        }
    }

    protected override void DisInteract()
    {
        // 何もしない
    }

    protected override void Interact()
    {
        // 音ならしーの
        if (m_SEInstace == null)
        {
            m_SEInstace = SEGenerator.InstantiateSE(m_byteSE);
        }
        // サイズ変え―の
        transform.localScale -= m_reduceValue;
        if (transform.localScale.x < 0.01f)
        {
            SEGenerator.InstantiateSE(m_oneUpSE);
            // 残機追加
            IncreaseZanki(m_increaseZanki);
            // 割り当てられたbool値をfalseに
            GameVariables.s_OneUpCornFlag[m_boolIndex] = false;
            Destroy(gameObject);
        }
    }

    private void IncreaseZanki(int increment)
    {
        GameVariables.s_zankiNum += increment;
        GameObject.Find("Zanki").GetComponent<ZankiNumTextController>().UpdateText();
    }
}
