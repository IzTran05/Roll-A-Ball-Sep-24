using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlingPin : MonoBehaviour
{
    bool knockedOver = false;
    PlayerController playerController;

   void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
       if(transform.up.y < 0.5f && !knockedOver)
        {
            playerController.PinFall();
            knockedOver = true;
        }
    }

}
