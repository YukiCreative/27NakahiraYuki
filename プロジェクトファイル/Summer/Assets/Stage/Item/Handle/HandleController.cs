using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandleController : InteractableObjects
{
    private Rigidbody2D m_parentRigidBody;
    private Vector2 krightPower = new Vector2(6, 0);
    private Vector2 kleftPower = new Vector2(-6, 0);

    protected override void Start()
    {
        base.Start();
        m_parentRigidBody = transform.parent.gameObject.GetComponent<Rigidbody2D>();
    }

    protected override void Interact()
    {
        // これが実行されているかつ
        // 移動キーが押されたら
        // 子オブジェクトもろとも移動　かな
        Keyboard keyboard = Keyboard.current;
        if (keyboard == null) return;
        if (keyboard[Key.A].isPressed)
        {
            m_parentRigidBody.AddForce(kleftPower);
        }
        if (keyboard[Key.D].isPressed)
        {
            m_parentRigidBody.AddForce(krightPower);
        }
    }

    protected override void DisInteract()
    {
        // 何もしない
    }
}
