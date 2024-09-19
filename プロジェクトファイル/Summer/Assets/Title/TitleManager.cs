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
        // やっぱ文字列だと保守しにくいよなー
        m_canvasGroup = GameObject.Find("Canvas").GetComponent<CanvasGroup>();
        m_BGMAudio = GameObject.Find("TitleBGM").GetComponent<AudioSource>();
        m_ground = GameObject.Find("InvisibleGround");
        m_player = GameObject.Find("Player");
        // Staticな変数への代入(初期化に近い)
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
        // マウス取得
        // デバイスはいつ外れるか分からないのでUpdateでローカルの変数を宣言するし取得する
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
        // 演出として、少し間をおいてから中央のカピバラを落下させる
        // その前にUIがゆっくり消える
        // BGMも同じように消える　
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
