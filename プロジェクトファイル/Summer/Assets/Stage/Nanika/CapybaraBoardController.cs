using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapybaraBoardController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        if (PlayerController.s_player.transform.position.x > 22 &&
            PlayerController.s_player.transform.position.x < 43)
        {
            transform.position = new Vector3(
                PlayerController.s_player.transform.position.x,
                transform.position.y,
                transform.position.z);
            if (transform.position.x < 24)
            {
                transform.position = new Vector3(24,
                    transform.position.y, transform.position.z);
            }
        }
    }
}
