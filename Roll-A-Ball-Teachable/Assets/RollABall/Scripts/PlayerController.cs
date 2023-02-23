using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI countText;
    public bool isGrounded;
    public float jumpSpeed = 3;


    private AudioSource coinSound;
    public Rigidbody rb;
    private float movementX;
    private float movementY;
    public int count;

    public float timer = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        coinSound = GetComponent<AudioSource>();
        count = 0;
        SetCountText();


    }

    private void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }


    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        // if(count == 12)
        // {
        //     winTextObject.SetActive(true);
        // }
    }

    void OnJump()
    {
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    // private void Update()
    // {
    //     // get Jump input and add force to the player
    //     // var jump =  Keyboard.current.spaceKey.wasPressedThisFrame;

        
        
    //     // if (Input.GetKeyDown("space") && isGrounded)
    //     // {
    //     //     rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
    //     //     isGrounded = false;
    //     // }

    //     // check if the player is free falling for more than 1 second and if so, reset the game
    //     if (rb.velocity.y < 0)
    //     {
    //         timer += Time.deltaTime;
    //         if (timer > 4.0f)
    //         {
    //             // reset the game
    //             // reset the timer
    //             timer = 0.0f;
    //             // reset the player position
    //             transform.position = new Vector3(0, 0.5f, 0);
    //         }
    //     }
    //     else
    //     {
    //         timer = 0.0f;
    //     }

    // }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);   
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter");
        if(other.gameObject.CompareTag("Pick Up"))
        {
            other.GetComponent<PickUpObject>().UnHighlight();
            // other.gameObject.SetActive(false);
            count++;
            coinSound.Play();
            SetCountText();
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        isGrounded = true;
    }

}
