using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum PowerupType { Grow, Shrink }

    public PowerupType myPowerup;
    public float powerupDuration = 7f;
    PlayerController playerController;

 void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    public void UsePowerUp()
    {
        if (myPowerup == PowerupType.Grow)
        {
            Vector3 temp = playerController.gameObject.transform.position;
            temp.y = 1;
            playerController.gameObject.transform.position = temp;
            playerController.gameObject.transform.localScale = Vector3.one * 2;
        }

        if (myPowerup == PowerupType.Shrink)
            playerController.gameObject.transform.localScale = Vector3.one / 2;

        StartCoroutine(ResetPowerUp());

    }

    IEnumerator ResetPowerUp()
    {
        yield return new WaitForSeconds(powerupDuration);
        if (myPowerup == PowerupType.Grow || myPowerup == PowerupType.Shrink)
        {
            playerController.gameObject.transform.localScale = Vector3.one;
        }
    }
    
}
