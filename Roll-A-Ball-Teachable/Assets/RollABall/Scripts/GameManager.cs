using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public MenuManager pauseMenu;
    public UnityEvent<bool> OnPauseButtonPressed;

    public Connection connection;

    // Start is called before the first frame update
    void Start()
    {
        connection = GetComponent<Connection>();
        connection.ToggleWebsocketControl();
    }

    public void OnPauseButton()
    {
        Debug.Log("Pause button pressed");

        Time.timeScale = Time.timeScale == 0 ? 1 : 0;

        pauseMenu.TogglePauseMenuPanel();

        bool isPaused = pauseMenu.isPaused; // get the current state of the pause menu
        connection.ToggleWebsocketControl(isPaused);

        if (OnPauseButtonPressed != null)
        {
            OnPauseButtonPressed.Invoke(isPaused);
        }
    }

    public void OnPlayButton()
    {
        Debug.Log("Play button pressed");
        pauseMenu.TogglePauseMenuPanel();
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


    }
}
