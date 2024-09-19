using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapybaraBoardSoundController : MonoBehaviour
{
    // ���̃X�N���v�g��n�ʂ̕��ɃA�^�b�`
    [SerializeField]
    private AudioClip m_SE1;
    [SerializeField]
    private AudioClip m_SE2;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        float inpactSpeed = collision.relativeVelocity.sqrMagnitude;
        if (inpactSpeed > 14)
        {
            int random = Random.Range(0, 2);
            if (random == 0)
            {
                SEGenerator.InstantiateSE(m_SE1);
            }
            else
            {
                SEGenerator.InstantiateSE(m_SE2);
            }
        }

    }
}
