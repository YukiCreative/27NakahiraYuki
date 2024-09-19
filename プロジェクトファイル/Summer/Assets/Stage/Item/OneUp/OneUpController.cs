using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneUpController : InteractableObjects
{
    // ��{�I�ɂ�EatItem�ƈꏏ
    // �H�׏I�������c�@���ݒ肳�ꂽ�l�񕜁@
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
        // �����Ɋ��蓖�Ă�ꂽ�ԍ���bool�l�����āAfalse�Ȃ�ޏ�
        if (!GameVariables.s_OneUpCornFlag[m_boolIndex])
        {
            Destroy(gameObject);
        }
    }

    protected override void DisInteract()
    {
        // �������Ȃ�
    }

    protected override void Interact()
    {
        // ���Ȃ炵�[��
        if (m_SEInstace == null)
        {
            m_SEInstace = SEGenerator.InstantiateSE(m_byteSE);
        }
        // �T�C�Y�ς��\��
        transform.localScale -= m_reduceValue;
        if (transform.localScale.x < 0.01f)
        {
            SEGenerator.InstantiateSE(m_oneUpSE);
            // �c�@�ǉ�
            IncreaseZanki(m_increaseZanki);
            // ���蓖�Ă�ꂽbool�l��false��
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
