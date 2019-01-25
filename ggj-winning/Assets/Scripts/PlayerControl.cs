using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    public float speed = 6.0f;

    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            Debug.Log("square button was pressed.");
        }

        if (Input.GetKeyUp(KeyCode.Joystick1Button1))
        {
            Debug.Log("X button was released.");
        }



        moveDirection = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f );
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection = moveDirection * speed;

        // Move the controller
        controller.Move(moveDirection * Time.deltaTime);

        for (int i = 0; i < 4; i++)
        {
            if (Mathf.Abs(Input.GetAxis("Joy" + i + "X")) > 0.1 ||
                Mathf.Abs(Input.GetAxis("Joy" + i + "Y")) > 0.1)
            {
                Debug.Log(Input.GetJoystickNames()[i] + i + " is moved");
            }
        }



    }
}
