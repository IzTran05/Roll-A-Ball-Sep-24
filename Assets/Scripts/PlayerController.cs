using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
   public float speed = 1.0f;
   private Rigidbody rb;
   private int pickupCount;
   public Timer timer;
   private bool gameOver = false; 
    

    [Header("UI")]
    public TMP_Text pickupText;
    public TMP_Text timerText;
    public GameObject winPanel;
    public TMP_Text winTimeText;
    public GameObject inGamePanel;
    public GameObject gameOverScreen;


   
 
    void Start()
    {
        // Gets the rigidbody component attatched to this game object
        rb = GetComponent<Rigidbody>();
        //Gets the number of pickups in our scene
        pickupCount = GameObject.FindGameObjectsWithTag("Pickup").Length - 1;
        //Run the check pickups function
        CheckPickups();
        //Gets the time object
        //timer = FindObjectOfType<Timer>();
        timer.StartTimer();

        //Turn on in game panel
        inGamePanel.SetActive(true);

        //Turn off win panel
        winPanel.SetActive(false);

        //GameOverScreen
        gameOverScreen.SetActive(false);
    }

    private void Update()
    {
        timerText.text = "Time: " + timer.currentTime.ToString("F2");
    }

    
    void FixedUpdate()
    {
        //if (gameOver == true)
          //  return;
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

        pickupText.text = "pickups left: " + pickupCount.ToString();
        pickupText.text = "Pickups Left: " + pickupCount;
        if(pickupCount == 0)
        {
            WinGame();
        }
    }

    private void WinGame()
    {
        //Set our game over to be true
        gameOver = true;
        //Set our timer to stop
        timer.StopTimer();

        //Display the timer on our win time text
        winTimeText.text = "Your Time was: " + timer.GetTime().ToString("F2");

        //Turn off InGamePanel
        inGamePanel.SetActive(false);

        //Turn off win panel (Update*:Temporarly set to false)
        winPanel.SetActive(true);

     

        //Stop the ball from rolling
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    //Temporary restart function
    public void ResetGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}


