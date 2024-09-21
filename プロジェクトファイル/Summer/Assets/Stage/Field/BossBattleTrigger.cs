using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BossBattleTrigger : MonoBehaviour
{
    // めんどくさいから全部エディタで付ける
    [SerializeField]
    GameObject m_wall;
    // 映画風にするための黒いUI
    [SerializeField]
    GameObject m_movieMask;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            m_wall.SetActive(true);
            StartCoroutine(Movie());
        }
    }


    // ボス戦のムービーをここに書く
    private IEnumerator Movie()
    {
        // まず映画風の黒いアレを出す
        m_movieMask.GetComponent<MovieMaskController>().VisibleMask();
        yield return null;
    }
}
