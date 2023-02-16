using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI countText;
   
    // public GameObject winTextObject;

    private AudioSource coinSound;
    public Rigidbody rb;
    private float movementX;
    private float movementY;
    public int count;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        coinSound = GetComponent<AudioSource>();
        count = 0;
        SetCountText();
        // winTextObject.SetActive(false);
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

}
