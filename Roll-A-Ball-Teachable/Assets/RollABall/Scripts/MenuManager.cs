using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MenuManager : MonoBehaviour
{
    public GameObject gestureHelpPanel;

    // event for when the play button is pressed
    [Header("When the play button is pressed")]
    public UnityEvent OnPlayButtonPressed;
    
    void Start()
    {
        gestureHelpPanel.SetActive(false);
    }

    public void ToggleGestureHelpPanel()
    {
        gestureHelpPanel.SetActive(!gestureHelpPanel.activeSelf);
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

    // on disable, make sure the gesture help panel is hidden
    void OnDisable()
    {
        gestureHelpPanel.SetActive(false);
    }
}
