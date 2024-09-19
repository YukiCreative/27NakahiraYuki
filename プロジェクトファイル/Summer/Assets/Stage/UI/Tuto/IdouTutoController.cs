using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IdouTutoController : MonoBehaviour
{
    private GameObject idouTutoUI;

    private void Start()
    {
        idouTutoUI = GameObject.Find("Idou");
        idouTutoUI.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            idouTutoUI.SetActive(true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            idouTutoUI.SetActive(false);
        }
    }
}
