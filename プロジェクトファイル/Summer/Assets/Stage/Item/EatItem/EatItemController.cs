using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatItemController : InteractableObjects
{
    // �V���A���C�Y���ăG�f�B�^���炻�ꂼ��̒l��ݒ肷�銴����
    [SerializeField]
    private int kReducePoint = 100;
    private GameObject m_byteSEInstance;
    [SerializeField]
    private AudioClip m_byteSE;
    [SerializeField]
    private AudioClip m_completeSE;

    protected override void DisInteract()
    {
        // �������Ȃ�
    }

    protected override void Interact()
    {
        // ����ɏ������Ȃ�
        // ����炷
        // �J�s�o���̎���Z������
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
