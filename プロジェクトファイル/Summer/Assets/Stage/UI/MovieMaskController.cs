using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovieMaskController : MonoBehaviour
{
    private Vector3 m_invisibleScale = new Vector3(1, 1.5f, 1); 
    public void VisibleMask()
    {
        transform.DOComplete();
        transform.DOScale(Vector3.one, 1f);
    }
    public void InvisibleMask()
    {
        transform.DOComplete();
        transform.DOScale(m_invisibleScale, 1f);
    }
}
