using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
   public float speed = 1.0f;
   private Rigidbody rb;
   private int pickupCount;
   private Timer timer;

   
 
    void Start()
    {
        // Gets the rigidbody component attatched to this game object
        rb = GetComponent<Rigidbody>();
        //Gets the number of pickups in our scene
        pickupCount = GameObject.FindGameObjectsWithTag("Pickup").Length - 1;
        //Run the check pickups function
        CheckPickups();
        //Gets the time object
        timer = FindAnyObjectByType<Timer>();
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

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Pickup")
        {
            //Destroy the collided object
            Destroy(other.gameObject);
            //Decrement the pickup count
            pickupCount--;
            //RUn the check pickups function
            CheckPickups();
            //Gets the timer object
            timer = FindObjectOfType<Timer>();
            //starts the timer 
            timer.StartTimer();

        }
    }

    private void CheckPickups()
    {

        print("Pickups Left: " + pickupCount);
        if(pickupCount == 0)
        {
            WinGame();
        }
    }

    private void WinGame()
    {
        timer.StopTimer();
        print("Yiipie! You Win! :3. Your time was: " + timer.GetTime().ToString("F2"));
    }
}


