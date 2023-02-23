using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MenuManager : MonoBehaviour
{
    public GameObject gestureHelpPanel;
    public GameObject pauseMenuPanel;

    public GameObject mainButtonsPanel;

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

    // event for when the sound button is pressed
    [Header("When the sound button is pressed")]
    public UnityEvent<bool> OnSoundButtonPressed;
    
    void Start()
    {
        gestureHelpPanel.SetActive(false);
        pauseMenuPanel.SetActive(isMainMenu);
        pauseMenuButton.SetActive(!isMainMenu);
    }

    public void ToggleGestureHelpPanel()
    {
        gestureHelpPanel.SetActive(!gestureHelpPanel.activeSelf);

        mainButtonsPanel.SetActive(!gestureHelpPanel.activeSelf);
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

    public void OnToggleAudioButton()
    {
        Debug.Log("Toggle audio button pressed");
        AudioListener.pause = !AudioListener.pause;

        if (OnSoundButtonPressed != null)
        {
            OnSoundButtonPressed.Invoke(AudioListener.pause);
        }
    }

    // on disable, make sure the gesture help panel is hidden
    void OnDisable()
    {
        gestureHelpPanel.SetActive(false);
    }

    public void ShouldReset(bool isPaused)
    {
        if (!isPaused)
        {
            // reset the menu 
            gestureHelpPanel.SetActive(false);
            mainButtonsPanel.SetActive(true);
        }
    }
}
