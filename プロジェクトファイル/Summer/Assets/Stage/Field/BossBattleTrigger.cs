using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Tilemaps;

public class BossBattleTrigger : MonoBehaviour
{
    // めんどくさいから全部エディタで付ける
    [SerializeField]
    GameObject m_wall;
    // 映画風にするための黒いUI
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


    // ボス戦のムービーをここに書く
    private void Movie()
    {
        // とりあえずトリガーを消す
        GetComponent<TilemapCollider2D>().enabled = false;

        // カメラがプレイヤーに追従する機能をオフにする
        CameraController.s_trackPlayer = false;

        // プレイヤーを動かなくする
        m_playerController.Stop();

        // シークエンスを登録
        Sequence sequence = DOTween.Sequence();
        // まずカメラを初期位置に移動
        sequence.Append(m_cameraObject.transform.DOMove(new Vector3(53, 0, -10), 3));
        // かつ映画風の黒いアレを出す
        sequence.Join(m_movieMask.transform.DOScale(Vector3.one, 1));
        // 次に壁が生成されるのを映す
        sequence.Append(m_cameraComponent.DOOrthoSize(3, 1));
        sequence.Join(m_cameraObject.transform.DOMove(new Vector3(49, -1, -10), 1)
            .OnComplete(() => m_wall.SetActive(true)));
        // ちょっと待つ
        sequence.AppendInterval(2);
        //敵を映す。カメラのサイズは戻す
        sequence.Append(m_cameraComponent.DOOrthoSize(4, 1));
        sequence.Join(m_cameraObject.transform.DOMove(new Vector3(58, 0, -10), 3));
        // 敵の黒いのをなくす
        sequence.Append(m_dinosaurSprite.DOColor(Color.white, 3)
            .OnComplete(() => m_dinosaurController.m_conMove = true));
        // かつ敵のHPバーを出現させる
        sequence.Join(m_hpBarGroup.DOFade(1, 3));
        // マスクを消す。プレイヤーを操作可能にする
        sequence.Append(m_movieMask.transform.DOScale(new Vector3(1, 1.5f, 1), 1)
            .OnComplete(m_playerController.Move));
        // 実行
        sequence.Play();
    }
}
