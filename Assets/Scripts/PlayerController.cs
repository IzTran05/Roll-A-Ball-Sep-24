using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
   public float speed = 1.0f;
   private Rigidbody rb;
   
 
    void Start()
    {
        // Gets the rigidbody component attatched to this game object
        rb = GetComponent<Rigidbody>();
    }

    
    void FixedUpdate()
    {
        // Store the horizontal axis value in a float
        float moveHorizontal = Input.GetAxis("Horizontal");
        // Store the vertical axis value in a float
        float moveVertical = Input.GetAxis("Horizontal");

        // Create a new Vector 3 based on the horizontal and vertical values
        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);

        // Add force to our rigidbody from our movement vector * spped variable
        rb.AddForce(movement * speed * Time.deltaTime);  
        
       
    }
}

