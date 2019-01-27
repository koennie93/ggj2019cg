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
    public static ControllerManager Instance = null;

    public State state = State.MAIN_MENU;

    public Dictionary<int, PlayerControl> controlDictionary = new Dictionary<int, PlayerControl>();
    public PlayerControl[] playerControls = new PlayerControl[4];
    public GameObject[] readyText = new GameObject[2];
    public GameObject[] playerImage = new GameObject[2];

    private int joined = 0;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.MAIN_MENU && Input.GetKeyDown(KeyCode.JoystickButton9) && joined >= 2)
        {
            Debug.Log("START THE GAME");
            LevelManager.Instance.StartCoroutine("FadeMenu");
            state = State.PLAYING;
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button1))
        {
            // if menu and not pressed X
            if (state == State.MAIN_MENU && !controlDictionary.ContainsKey(1))
            {
                controlDictionary.Add(1, playerControls[joined]);
                PlayerReady(joined);
                joined++;
                Debug.Log("Controller " + 1 + "Connected");
            }
            else
            {
                controlDictionary[1].XButtonPressed(true);
                controlDictionary[1].GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f);
            }
        }
        else if (Input.GetKeyUp(KeyCode.Joystick1Button1))
        {
            controlDictionary[1].XButtonPressed(false);
            controlDictionary[1].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
        }

        if (Input.GetKeyDown(KeyCode.Joystick2Button1))
        {
            if (state == State.MAIN_MENU && !controlDictionary.ContainsKey(2))
            {
                controlDictionary.Add(2, playerControls[joined]);
                PlayerReady(joined);
                joined++;
                Debug.Log("Controller " + 2 + "Connected");
            }
            else
            {
                controlDictionary[2].XButtonPressed(true);
                controlDictionary[2].GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f);
            }
        }
        else if (Input.GetKeyUp(KeyCode.Joystick2Button1))
        {
            controlDictionary[2].XButtonPressed(false);
            controlDictionary[2].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
        }

        if (Input.GetKeyDown(KeyCode.Joystick3Button1))
        {
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
                controlDictionary[3].GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f);
            }
        }
        else if (Input.GetKeyUp(KeyCode.Joystick3Button1))
        {
            controlDictionary[3].XButtonPressed(false);
            controlDictionary[3].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
        }

        if (Input.GetKeyDown(KeyCode.Joystick4Button1))
        {
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
                controlDictionary[1].GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f);
            }
        }
        else if (Input.GetKeyUp(KeyCode.Joystick4Button1))
        {
            controlDictionary[4].XButtonPressed(false);
            controlDictionary[4].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
        }

        for (int i = 0; i < 4; i++)
        {
            if (Mathf.Abs(Input.GetAxis("Joy" + i + "X")) > 0.2 ||
                Mathf.Abs(Input.GetAxis("Joy" + i + "Y")) > 0.2)
            {

                controlDictionary[i + 1].Move(i);
            }
        }
    }

    private void PlayerReady(int playerNumber)
    {
        readyText[playerNumber].transform.Find("T_Player").GetComponent<Text>().color = new Color(0, 255, 0);
        readyText[playerNumber].transform.Find("JoinText").gameObject.SetActive(false);
        playerImage[playerNumber].SetActive(true);
        //playerControls[playerNumber].gameObject.transform.position = new Vector3(0, 0, 0);
    }
}
