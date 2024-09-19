using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class InteractableObjects: MonoBehaviour
{
    // コライダーを持つ
    // コライダーにプレイヤーが入っていて、
    // Sキーが押されたとき、Intaractメソッドを実行する

    protected bool m_ifInPlayer = false;
    protected GameObject m_player;
    protected bool m_isInteractable = false;
    protected bool m_isInteractedBeforeFixedUpdate = false;

    // このメソッドを継承先でいじる感じ
    protected abstract void Interact();
    protected abstract void DisInteract();

    protected virtual void Start()
    {
        m_player = GameObject.Find("Player");
    }

    protected void Update()
    {
        // ここで入力取っていいのか
        // プレイヤーが働きかける方がいいのでは
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
            Debug.Log("Interact流れた");
            Interact();
            m_isInteractedBeforeFixedUpdate = true;
        }
        else if (m_isInteractedBeforeFixedUpdate)
        {
            Debug.Log("Disinteract流れた");
            // 使わない時は適当にpassしといてね
            DisInteract();
            m_isInteractedBeforeFixedUpdate = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerMouth"))
        {
            //Debug.Log("入ってます");
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
