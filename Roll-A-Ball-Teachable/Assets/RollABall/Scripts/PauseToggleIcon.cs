using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseToggleIcon : MonoBehaviour
{
    public Sprite pauseIcon;
    public Sprite playIcon;

    private UnityEngine.UI.Image image;

    void Start()
    {
        image = GetComponent<UnityEngine.UI.Image>();
    }

    public void TogglePauseIcon(bool isPaused)
    {
        image.sprite = isPaused ? playIcon : pauseIcon;
    }
}
