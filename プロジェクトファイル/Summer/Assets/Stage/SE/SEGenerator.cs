using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEGenerator : MonoBehaviour
{
    private static GameObject m_SEPrefab;

    private void Start()
    {
        m_SEPrefab = (GameObject)Resources.Load("PointSEObject");
        //Debug.Log(m_SEPrefab);
    }

    public static GameObject InstantiateSE(AudioClip audioClip)
    {
        GameObject prefab = Instantiate(m_SEPrefab);
        prefab.GetComponent<AudioSource>().clip = audioClip;
        return prefab;
    }
}
