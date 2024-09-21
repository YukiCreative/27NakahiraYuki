using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BossBattleTrigger : MonoBehaviour
{
    // �߂�ǂ���������S���G�f�B�^�ŕt����
    [SerializeField]
    GameObject m_wall;
    // �f�敗�ɂ��邽�߂̍���UI
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


    // �{�X��̃��[�r�[�������ɏ���
    private IEnumerator Movie()
    {
        // �܂��f�敗�̍����A�����o��
        m_movieMask.GetComponent<MovieMaskController>().VisibleMask();
        yield return null;
    }
}
