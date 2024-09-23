using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Tilemaps;

public class BossBattleTrigger : MonoBehaviour
{
    // �߂�ǂ���������S���G�f�B�^�ŕt����
    [SerializeField]
    GameObject m_wall;
    // �f�敗�ɂ��邽�߂̍���UI
    [SerializeField]
    GameObject m_movieMask;
    private GameObject m_cameraObject;
    private Camera m_cameraComponent;
    [SerializeField]
    private GameObject m_dinosaur;
    private SpriteRenderer m_dinosaurSprite;
    private DinoController m_dinosaurController;
    private PlayerController m_playerController;
    private CanvasGroup m_hpBarGroup;

    private void Start()
    {
        m_cameraComponent = Camera.main;
        m_cameraObject = m_cameraComponent.gameObject;
        m_dinosaurSprite = m_dinosaur.GetComponent<SpriteRenderer>();
        m_dinosaurController = m_dinosaur.GetComponent<DinoController>();
        m_playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        m_hpBarGroup = GameObject.Find("HPbar").GetComponent<CanvasGroup>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Movie();
        }
    }


    // �{�X��̃��[�r�[�������ɏ���
    private void Movie()
    {
        // �Ƃ肠�����g���K�[������
        GetComponent<TilemapCollider2D>().enabled = false;

        // �J�������v���C���[�ɒǏ]����@�\���I�t�ɂ���
        CameraController.s_trackPlayer = false;

        // �v���C���[�𓮂��Ȃ�����
        m_playerController.Stop();

        // �V�[�N�G���X��o�^
        Sequence sequence = DOTween.Sequence();
        // �܂��J�����������ʒu�Ɉړ�
        sequence.Append(m_cameraObject.transform.DOMove(new Vector3(53, 0, -10), 3));
        // ���f�敗�̍����A�����o��
        sequence.Join(m_movieMask.transform.DOScale(Vector3.one, 1));
        // ���ɕǂ����������̂��f��
        sequence.Append(m_cameraComponent.DOOrthoSize(3, 1));
        sequence.Join(m_cameraObject.transform.DOMove(new Vector3(49, -1, -10), 1)
            .OnComplete(() => m_wall.SetActive(true)));
        // ������Ƒ҂�
        sequence.AppendInterval(2);
        //�G���f���B�J�����̃T�C�Y�͖߂�
        sequence.Append(m_cameraComponent.DOOrthoSize(4, 1));
        sequence.Join(m_cameraObject.transform.DOMove(new Vector3(58, 0, -10), 3));
        // �G�̍����̂��Ȃ���
        sequence.Append(m_dinosaurSprite.DOColor(Color.white, 3)
            .OnComplete(() => m_dinosaurController.m_conMove = true));
        // ���G��HP�o�[���o��������
        sequence.Join(m_hpBarGroup.DOFade(1, 3));
        // �}�X�N�������B�v���C���[�𑀍�\�ɂ���
        sequence.Append(m_movieMask.transform.DOScale(new Vector3(1, 1.5f, 1), 1)
            .OnComplete(m_playerController.Move));
        // ���s
        sequence.Play();
    }
}
