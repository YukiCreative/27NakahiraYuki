using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class ZankiSceneManager : MonoBehaviour
{
    private RectTransform m_UITransform;
    private Vector3 m_UIPos = new Vector3(910, 490, 0);

    // Start is called before the first frame update
    IEnumerator Start()
    {
        m_UITransform = GameObject.Find("GeneralUI").GetComponent<RectTransform>();

        yield return new WaitForSeconds(2);

        m_UITransform.DOLocalMove(m_UIPos, 1).OnComplete(() =>
        {
            SceneManager.LoadScene(GameVariables.s_stageNum.ToString());
        });
    }
}
