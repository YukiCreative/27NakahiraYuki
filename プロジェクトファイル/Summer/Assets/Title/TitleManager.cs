using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class TitleManager : MonoBehaviour
{
    private bool m_isClicked = false;
    private CanvasGroup m_canvasGroup;
    private AudioSource m_BGMAudio;
    private GameObject m_ground;
    private GameObject m_player;
    [SerializeField]
    private AudioClip m_startSE;

    private void Start()
    {
        // ����ϕ����񂾂ƕێ炵�ɂ�����ȁ[
        m_canvasGroup = GameObject.Find("Canvas").GetComponent<CanvasGroup>();
        m_BGMAudio = GameObject.Find("TitleBGM").GetComponent<AudioSource>();
        m_ground = GameObject.Find("InvisibleGround");
        m_player = GameObject.Find("Player");
        // Static�ȕϐ��ւ̑��(�������ɋ߂�)
        GameVariables.s_zankiNum = 3;
        GameVariables.s_stageNum = 1;
        for (int i = 0; i < GameVariables.s_OneUpCornFlag.Length; i++)
        {
            GameVariables.s_OneUpCornFlag[i] = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // �}�E�X�擾
        // �f�o�C�X�͂��O��邩������Ȃ��̂�Update�Ń��[�J���̕ϐ���錾���邵�擾����
        Mouse mouse = Mouse.current;
        if (mouse == null || m_isClicked) return;

        if (mouse.leftButton.wasPressedThisFrame)
        {
            m_isClicked = true;
            StartCoroutine(GameStart());
        }
    }

    private IEnumerator GameStart()
    {
        SEGenerator.InstantiateSE(m_startSE);
        // ���o�Ƃ��āA�����Ԃ������Ă��璆���̃J�s�o���𗎉�������
        // ���̑O��UI��������������
        // BGM�������悤�ɏ�����@
        yield return StartCoroutine(TransparencyUI(1));
        yield return StartCoroutine(BGMController.ReduceBGM(1));

        m_ground.SetActive(false);
        m_player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;

        yield return new WaitForSeconds(2);

        SceneManager.LoadScene("StageDispScene");
    }

    private IEnumerator TransparencyUI(float time)
    {
        float factor = 1 / time;
        while (m_canvasGroup.alpha > 0)
        {
            m_canvasGroup.alpha -= Time.deltaTime * factor;
            yield return null;
        }
    }
}
