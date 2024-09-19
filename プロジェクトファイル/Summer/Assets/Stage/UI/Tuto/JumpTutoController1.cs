using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JumpTutoController : MonoBehaviour
{
    private GameObject jumpTutoUI;

    private void Start()
    {
        jumpTutoUI = GameObject.Find("JumpTuto");
        jumpTutoUI.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            jumpTutoUI.SetActive(true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            jumpTutoUI.SetActive(false);
        }
    }
}
