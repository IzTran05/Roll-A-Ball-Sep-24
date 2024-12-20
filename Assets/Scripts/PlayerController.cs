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
    GameObject resetPoint;
    bool resetting = false;
    Color originalColor;
    public bool grounded = true;
    private bool onJumpPad;
    public float launchSpeed = 20f;
   
    //Controllers
    GameController gameController;
    

    [Header("UI")]
    public TMP_Text pickupText;
    public TMP_Text timerText;
    public GameObject winPanel;
    public TMP_Text winTimeText;
    public GameObject inGamePanel;
    public GameObject gameOverScreen;
    public int count = 0;





    void Start()
    {
        //Addition for pause screen
        Time.timeScale = 1;
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

        //The reset zone
        resetPoint = GameObject.Find("Reset Point");
        originalColor = GetComponent<Renderer>().material.color;
        gameController = FindObjectOfType<GameController>();

        pickupCount = GameObject.FindGameObjectsWithTag("Pickup").Length
                      + GameObject.FindGameObjectsWithTag("Bowling Pin").Length;
    }

    private void Update()
    {
        timerText.text = "Time: " + timer.currentTime.ToString("F2");

        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("W");
        }
    }

    
    void FixedUpdate()
    {
        //restart function
        if (resetting)
            return;

        //if (gameController.controlType == ControlType.WorldTilt)
            //return;


        //if (gameOver == true)
          //  return;
        // Store the horizontal axis value in a float
        float moveHorizontal = Input.GetAxis("Horizontal");
        // Store the vertical axis value in a float
        float moveVertical = Input.GetAxis("Vertical");


        // Create a new Vector 3 based on the horizontal and vertical values
        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);

        // Add force to our rigidbody from our movement vector * spped variable
        rb.AddForce(movement * speed * Time.deltaTime);

        if (onJumpPad == true  && grounded)
        {
            onJumpPad = false;
            JumpPad(movement);
        }
        
       
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

        if(other.gameObject.CompareTag("PowerUp"))
        {
            other.GetComponent<PowerUp>().UsePowerUp();
            other.gameObject.transform.position = Vector3.down * 1000;
        }

        if(other.gameObject.CompareTag("Jump Pad"))
        {
            onJumpPad = true;
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

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Respawn"))
        {
            StartCoroutine(ResetPlayer()); 
        }
        
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.CompareTag("Grounded"))
            grounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Grounded"))
            grounded = false;
    }





    public IEnumerator ResetPlayer()
    {
        resetting = true;
        GetComponent<Renderer>().material.color = Color.black;
        rb.velocity = Vector3.zero;
        Vector3 startPos = transform.position;
        float resetSpeed = 2f;
        var i = 0.0f;
        var rate = 1.0f / resetSpeed;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            transform.position = Vector3.Lerp(startPos, resetPoint.transform.position, i);
            yield return null;
        }
       
        GetComponent<Renderer>().material.color = originalColor;
        resetting = false;
    }



    //Temporary restart function
    public void ResetGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    public void PinFall()
    {
        count += 1;
        SetCountText();
        if (count >= 5)
        {
            KnockedDown();
        }


    }

    private void SetCountText()
    {

    }

    private void KnockedDown()
    {
        Debug.Log("Pins Knocked Down: ");
    }

    private void JumpPad(Vector3 _movement)
    {
        Vector3 jumpForce = (_movement + Vector3.up) * launchSpeed;
        rb.AddForce(jumpForce, ForceMode.Impulse);
    }
    


}


