using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionTutoController : MonoBehaviour
{
    private GameObject actionTutoUI;

    private void Start()
    {
        actionTutoUI = GameObject.Find("ActionTuto");
        actionTutoUI.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            actionTutoUI.SetActive(true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            actionTutoUI.SetActive(false);
        }
    }
}
