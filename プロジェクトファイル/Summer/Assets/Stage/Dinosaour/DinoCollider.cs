using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoCollider : MonoBehaviour
{
    DinoController controller;
    private void Start()
    {
        controller = transform.parent.GetComponent<DinoController>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // ‚±‚ñ‚È‚ñ‚Å‚¢‚¢‚â
            controller.Damage((int)collision.relativeVelocity.sqrMagnitude);
        }
    }
}
