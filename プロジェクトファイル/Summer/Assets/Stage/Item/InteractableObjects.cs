using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class InteractableObjects: MonoBehaviour
{
    // �R���C�_�[������
    // �R���C�_�[�Ƀv���C���[�������Ă��āA
    // S�L�[�������ꂽ�Ƃ��AIntaract���\�b�h�����s����

    protected bool m_ifInPlayer = false;
    protected GameObject m_player;
    protected bool m_isInteractable = false;
    protected bool m_isInteractedBeforeFixedUpdate = false;

    // ���̃��\�b�h���p����ł����銴��
    protected abstract void Interact();
    protected abstract void DisInteract();

    protected virtual void Start()
    {
        m_player = GameObject.Find("Player");
    }

    protected void Update()
    {
        // �����œ��͎���Ă����̂�
        // �v���C���[��������������������̂ł�
        Keyboard keyboard = Keyboard.current;

        if (keyboard == null) return;

        if (keyboard[Key.S].isPressed && m_ifInPlayer)
        {
            m_isInteractable = true;
        }
        else
        {
            m_isInteractable = false;
        }
    }

    protected void FixedUpdate()
    {
        Debug.Log(m_isInteractable);
        if (m_isInteractable)
        {
            Debug.Log("Interact���ꂽ");
            Interact();
            m_isInteractedBeforeFixedUpdate = true;
        }
        else if (m_isInteractedBeforeFixedUpdate)
        {
            Debug.Log("Disinteract���ꂽ");
            // �g��Ȃ����͓K����pass���Ƃ��Ă�
            DisInteract();
            m_isInteractedBeforeFixedUpdate = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerMouth"))
        {
            //Debug.Log("�����Ă܂�");
            m_ifInPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerMouth"))
        {
            m_ifInPlayer = false;
        }
    }
}
