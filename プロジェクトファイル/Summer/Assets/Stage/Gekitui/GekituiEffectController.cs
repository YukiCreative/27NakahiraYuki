using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GekituiEffectController : MonoBehaviour
{
    // これをアニメーションから召喚しているので忘れないで
    public void DestroyThisGameObject()
    {
        Destroy(gameObject);
    }
}
