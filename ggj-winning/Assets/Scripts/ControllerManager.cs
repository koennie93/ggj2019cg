using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum State
{
    MAIN_MENU,
    PLAYING
}

public class ControllerManager : MonoBehaviour
{
    private State state = State.MAIN_MENU;

    public Dictionary<int, PlayerControl> controlDictionary = new Dictionary<int, PlayerControl>();
    public PlayerControl[] playerControls = new PlayerControl[4];
    public Text[] readyText = new Text[4];

    private int joined = 0;

    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.MAIN_MENU && Input.GetKeyDown(KeyCode.JoystickButton9) && joined >= 2)
        {
            Debug.Log("START THE GAME");
            StartCoroutine(FadeMenu());
            
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button1))
        {
            Debug.Log("X button was pressed.");
            // if menu and not pressed X
            if(state == State.MAIN_MENU && !controlDictionary.ContainsKey(1))
            {
                controlDictionary.Add(1, playerControls[joined]);
                PlayerReady(joined);
                joined++;
                Debug.Log("Controller " + 1 + "Connected");
            }
            else
            {
            }
        }

        if (Input.GetKeyDown(KeyCode.Joystick2Button1))
        {
            Debug.Log("X button was pressed.");
            if (state == State.MAIN_MENU && !controlDictionary.ContainsKey(2))
            {
                controlDictionary.Add(2, playerControls[joined]);
                PlayerReady(joined);
                joined++;
                Debug.Log("Controller " + 2 + "Connected");
            }
            else
            {
            }
        }

        if (Input.GetKeyDown(KeyCode.Joystick3Button1))
        {
            Debug.Log("X button was pressed.");
            if (state == State.MAIN_MENU && !controlDictionary.ContainsKey(3))
            {
                controlDictionary.Add(3, playerControls[joined]);
                PlayerReady(joined);
                joined++;
                Debug.Log("Controller " + 3 + "Connected");
            }
            else
            {
                controlDictionary[3].XButtonPressed(true);
            }
        }
        else if (Input.GetKeyUp(KeyCode.Joystick3Button1))
        {
            controlDictionary[3].XButtonPressed(false);
        }

        if (Input.GetKeyDown(KeyCode.Joystick4Button1))
        {
            Debug.Log("X button was pressed.");
            if (state == State.MAIN_MENU && !controlDictionary.ContainsKey(4))
            {
                controlDictionary.Add(4, playerControls[joined]);
                PlayerReady(joined);
                joined++;
                Debug.Log("Controller " + 4 + "Connected");
            }
            else
            {
                controlDictionary[4].XButtonPressed(true);
            }
        } else if (Input.GetKeyUp(KeyCode.Joystick4Button1))
        {
            controlDictionary[4].XButtonPressed(false);
        }

            for (int i = 0; i < 4; i++)
        {
            if (Mathf.Abs(Input.GetAxis("Joy" + i + "X")) > 0.2 ||
                Mathf.Abs(Input.GetAxis("Joy" + i + "Y")) > 0.2)
            {
                
                controlDictionary[i+1].Move(i);
            }
        }
    }

    private void PlayerReady(int playerNumber)
    {
        readyText[playerNumber].color = new Color(0, 255, 0);
        playerControls[playerNumber].gameObject.transform.position = new Vector3(0, 0, 0);
    }

    private IEnumerator FadeMenu()
    {
        CanvasGroup canvasGroup = GameObject.FindGameObjectWithTag("MainMenu").GetComponent<CanvasGroup>();
        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= Time.deltaTime;
            yield return null;
        }
        //Application.LoadLevel("Scene2");
    }
}
