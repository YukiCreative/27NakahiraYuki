using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class OnsenController : MonoBehaviour
{
    private Tweener m_animation;
    [SerializeField]
    private AudioClip m_SE;
    private BGMController m_BGM;

    private void Start()
    {
        m_BGM = GameObject.Find("BGM").GetComponent<BGMController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (m_animation != null) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            // �܂��X�e�[�W�̐i�s�󋵂�+1����
            GameVariables.s_stageNum++;
            // �v���C���[�������Ȃ����ē����Ȃ�����(�����Ȃ������Ŕ��E��)
            PlayerController.s_player.GetComponent<Rigidbody2D>().simulated = false;
            PlayerController.s_player.GetComponent<SpriteRenderer>().enabled = false;
            // BGM���~�߂�
            m_BGM.StopBGM();
            // SE�炵��
            SEGenerator.InstantiateSE(m_SE);
            // DOTween�ŃA�j���[�V��������(�N�\�L���C���f���g����)
            m_animation = transform.DOScale(new Vector3(2, 2, 1), 0.1f)
            .OnComplete
            (() =>
                transform.DOScale(Vector3.one, 2).SetEase(Ease.OutElastic)
                .OnComplete
                (() =>
                    SceneManager.LoadScene("StageDispScene")
                )
            );
        }
    }
}
