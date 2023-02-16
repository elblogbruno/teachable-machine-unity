using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MenuManager : MonoBehaviour
{
    public GameObject gestureHelpPanel;
    public GameObject pauseMenuPanel;

    public bool isMainMenu = false;

    public GameObject pauseMenuButton;

    public bool isPaused
    {
        get
        {
            return pauseMenuPanel.activeSelf;
        }
    }

    // event for when the play button is pressed
    [Header("When the play button is pressed")]
    public UnityEvent OnPlayButtonPressed;
    
    void Start()
    {
        gestureHelpPanel.SetActive(false);
        pauseMenuPanel.SetActive(isMainMenu);
        pauseMenuButton.SetActive(!isMainMenu);
    }

    public void ToggleGestureHelpPanel()
    {
        gestureHelpPanel.SetActive(!gestureHelpPanel.activeSelf);
    }

    public void TogglePauseMenuPanel()
    {
        pauseMenuPanel.SetActive(!pauseMenuPanel.activeSelf);
    }


    public void LoadScene(int sceneIndex)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneIndex);
    }

    public void OnPlayButton()
    {
        Debug.Log("Play button pressed");
        if (OnPlayButtonPressed != null)
        {
            OnPlayButtonPressed.Invoke();
        }
    }

    public void OnQuitButton()
    {
        Debug.Log("Quit button pressed");
        Application.Quit();
    }

    // on disable, make sure the gesture help panel is hidden
    void OnDisable()
    {
        gestureHelpPanel.SetActive(false);
    }
}
