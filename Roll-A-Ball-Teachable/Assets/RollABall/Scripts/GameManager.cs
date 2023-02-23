using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public MenuManager pauseMenu;
    public UnityEvent<bool> OnPauseButtonPressed;

    private Connection connection;

    public float initialTimeToPickupObject = 1.0f;
    public int maxPickupObjects = 16;

    private bool hasEnd = false;

    [Header("UI")]
    public TMPro.TMP_Text timerText;
    public TMPro.TMP_Text speedText;

    public GameObject winTextObject, loseTextObject, hudCanvas;

    public PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        connection = GetComponent<Connection>();
        connection.ToggleWebsocketControl();

        // set the initial state of the timerText to the initialTimeToPickupObject
        timerText.text = initialTimeToPickupObject.ToString("F2");

        // set the initial state of the speedText to 0
        speedText.text = "0.00 m/s";

        // set the initial state of the winTextObject to false
        winTextObject.SetActive(false);
        loseTextObject.SetActive(false);
    }

    public void OnPauseButton()
    {
        Debug.Log("Pause button pressed");

        Time.timeScale = Time.timeScale == 0 ? 1 : 0;

        pauseMenu.TogglePauseMenuPanel();

        bool isPaused = pauseMenu.isPaused; // get the current state of the pause menu
        connection.ToggleWebsocketControl(isPaused);

        pauseMenu.ShouldReset(isPaused);

        if (OnPauseButtonPressed != null)
        {
            OnPauseButtonPressed.Invoke(isPaused);
        }
    }

    public void OnPauseButton(bool isPaused)
    {
        Debug.Log("Pause button pressed");

        Time.timeScale = isPaused ? 0 : 1;

        pauseMenu.TogglePauseMenuPanel();

        connection.ToggleWebsocketControl(isPaused);

        pauseMenu.ShouldReset(isPaused);

        if (OnPauseButtonPressed != null)
        {
            OnPauseButtonPressed.Invoke(isPaused);
        }
    }

    public void OnPlayButton()
    {
        Debug.Log("Play button pressed");

        if (hasEnd) // we move the user to the main menu if the game has ended (win or lose) 
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }

        OnPauseButton();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            OnPauseButton();
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            connection.ToggleWebsocketControl();
        }

        // if the timer is not 0, then count down
        if (initialTimeToPickupObject > 0)
        {
            initialTimeToPickupObject -= Time.deltaTime;
            timerText.text = initialTimeToPickupObject.ToString("F2") + " s";

            if (playerController.count >= maxPickupObjects)
            {
                EndGame();
            }
        }
        else
        {
            timerText.text = "0.00 s";
            // if the timer is 0, then end the game
            EndGame();
        }

        if (playerController.rb.transform.position.y < 0)
        {
            EndGame();
        }

        // calculate speed and display it
        float speedRB = playerController.rb.velocity.magnitude;

        speedText.text = speedRB.ToString("F2") + " m/s";
    }

    IEnumerator WaitForSecondsThenStop(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        // stop the game
        Time.timeScale = 0;

        // stop the websocket connection
        connection.ToggleWebsocketControl(false);

        pauseMenu.TogglePauseMenuPanel();

        hudCanvas.SetActive(false);

        hasEnd = true;
    }

    public void EndGame()
    {
        if (!hasEnd)
        {
            if (playerController.count < maxPickupObjects)
            {
                Debug.Log("You lost!");
                loseTextObject.SetActive(true);
            }
            else
            {
                Debug.Log("You won!");
                winTextObject.SetActive(true);
            }

            // wait for 2 seconds
            StartCoroutine(WaitForSecondsThenStop(2));   
        }
    }

    public void OnRestartButton()
    {
        Debug.Log("Restart button pressed");
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
