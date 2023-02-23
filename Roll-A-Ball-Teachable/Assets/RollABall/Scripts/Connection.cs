using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

using NativeWebSocket;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;

public class Connection : MonoBehaviour
{
  WebSocket websocket;
  // public Rigidbody rbBall;
  public TMPro.TMP_Text commandText;

  public RawImage imageRawImage; 

  InputDevice device;

  bool isWebsocketControl = false;

  GameManager gameManager;


  // Start is called before the first frame update
  async void Start()
  {
    // get the device
    device = InputSystem.GetDevice<Keyboard>();

    gameManager = GetComponent<GameManager>();

    websocket = new WebSocket("ws://localhost:8080");

    websocket.OnOpen += () =>
    {
      Debug.Log("Connection open!");
    };

    websocket.OnError += (e) =>
    {
      Debug.Log("Error! " + e);
    };

    websocket.OnClose += (e) =>
    {
      Debug.Log("Connection closed!");
    };

    websocket.OnMessage += (bytes) =>
    {
      // Debug.Log("OnMessage!");
      Debug.Log("OnMessage! " + bytes.Length);

      // getting the message as a string
      var message = System.Text.Encoding.UTF8.GetString(bytes);
      // Debug.Log("OnMessage! " + message);

      if (bytes.Length > 100)
      {
          Debug.Log("Image received from server");
          // bytes are the image in base64
          // convert to texture2d

          // data:image/png;base64,
          // remove the first 22 characters
          message = message.Substring(22);
          // convert to byte array

          byte[]  imageBytes = Convert.FromBase64String(message);
          // convert to texture2d
          
          var image = new Texture2D(320, 240);
          image.LoadImage(imageBytes);
          imageRawImage.texture = image;

          return;
      }
      

      

      if (!string.IsNullOrEmpty(message) && isWebsocketControl)
      {
        commandText.text = message;

        switch (message)
        {
          case "Forward":
            // rbBall.AddForce(Vector3.forward * 3);
            
            // use unity input system to move the ball
            // https://docs.unity3d.com/Manual/class-InputManager.html

            // move the ball forward
            InputSystem.QueueStateEvent(device, new KeyboardState(Key.W));
            InputSystem.Update();


            break;
          case "Backward":
            // rbBall.AddForce(Vector3.back * 3);

            // move the ball backward
            InputSystem.QueueStateEvent(device, new KeyboardState(Key.S));
            InputSystem.Update();

            break;
          case "Left":
            // rbBall.AddForce(Vector3.left * 3);

            // move the ball left
            InputSystem.QueueStateEvent(device, new KeyboardState(Key.A));
            InputSystem.Update();

            break;
          case "Right":
            // rbBall.AddForce(Vector3.right * 3);

            // move the ball right
            InputSystem.QueueStateEvent(device, new KeyboardState(Key.D));
            InputSystem.Update();

            break;
          case "ShowPauseMenu":

            // show the pause menu
            // pauseMenuPanel.SetActive(true);
            gameManager.OnPauseButton(true);

            break;
          case "HidePauseMenu":
            
              // hide the pause menu
              // pauseMenuPanel.SetActive(false);
              gameManager.OnPauseButton(false);
  
              break;
          case "Jump":
            //  press space to jump
            InputSystem.QueueStateEvent(device, new KeyboardState(Key.Space));
            InputSystem.Update();

            break;
          default:
            Debug.Log("Neutral");



            break;
        }
      }
    };

    // Keep sending messages at every 0.3s
    InvokeRepeating("SendWebSocketMessage", 0.0f, 0.3f);

    // waiting for messages
    await websocket.Connect();
  }

  public void ToggleWebsocketControl()
  {
    isWebsocketControl = !isWebsocketControl; // toggle if we want also to control the ball with the websocket input

    commandText.text = isWebsocketControl ? "Hand" :  "WASD";
  }

   public void ToggleWebsocketControl(bool isPaused)
  {
    isWebsocketControl = isPaused; // toggle if we want also to control the ball with the websocket input

    commandText.text = isWebsocketControl ? "Hand" :  "WASD";
  }

  void Update()
  {
    #if !UNITY_WEBGL || UNITY_EDITOR
      websocket.DispatchMessageQueue();
    #endif
  }

  async void SendWebSocketMessage()
  {
    if (websocket.State == WebSocketState.Open)
    {
      // Sending bytes
      await websocket.Send(new byte[] { 10, 20, 30 });

      // Sending plain text
      await websocket.SendText("plain text message");
    }
  }

  private async void OnApplicationQuit()
  {
    await websocket.Close();
  }

}