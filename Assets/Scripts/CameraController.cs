using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;

    void Start()
    {
        //Set the offset of the camera based on the players position
        offset = transform.position - player.transform.position;
    }


    void LateUpdate()
    {
        //Make the transform position of this camera follow the players tranform position
        transform.position = player.transform.position + offset;
    }
}
