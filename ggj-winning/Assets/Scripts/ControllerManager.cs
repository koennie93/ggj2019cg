﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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

    private int joined = 0;

    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {   
        if (Input.GetKeyDown(KeyCode.Joystick1Button1))
        {
            Debug.Log("X button was pressed.");
            // if menu and not pressed X
            if(state == State.MAIN_MENU && !controlDictionary.ContainsKey(1))
            {
                controlDictionary.Add(1, playerControls[joined]);
                joined++;
                Debug.Log("Controller " + 1 + "Connected");
            }
            else
            {
                controlDictionary[1].Test(1);
            }
        }

        if (Input.GetKeyDown(KeyCode.Joystick2Button1))
        {
            Debug.Log("X button was pressed.");
            if (state == State.MAIN_MENU && !controlDictionary.ContainsKey(2))
            {
                controlDictionary.Add(2, playerControls[joined]);
                joined++;
                Debug.Log("Controller " + 2 + "Connected");
            }
            else
            {
                controlDictionary[2].Test(2);
            }
        }

        if (Input.GetKeyDown(KeyCode.Joystick3Button1))
        {
            Debug.Log("X button was pressed.");
            if (state == State.MAIN_MENU && !controlDictionary.ContainsKey(3))
            {
                controlDictionary.Add(3, playerControls[joined]);
                joined++;
                Debug.Log("Controller " + 3 + "Connected");
            }
            else
            {
                controlDictionary[3].Test(3);
            }
        }

        if (Input.GetKeyDown(KeyCode.Joystick4Button1))
        {
            Debug.Log("X button was pressed.");
            if (state == State.MAIN_MENU && !controlDictionary.ContainsKey(4))
            {
                controlDictionary.Add(4, playerControls[joined]);
                joined++;
                Debug.Log("Controller " + 4 + "Connected");
            }
            else
            {
                controlDictionary[4].Test(4);
            }
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
}