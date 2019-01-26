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
     
        
    }

    public void Test(int i)
    {
        Debug.Log("Koen is fcking awesome: " + i);
    }

    public void Move(int i)
    {
        moveDirection = new Vector3(Input.GetAxis("Joy" + i + "X"), Input.GetAxis("Joy" + i + "Y"), 0.0f );
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection = moveDirection * speed;

        // Move the controller
        controller.Move(moveDirection * Time.deltaTime);
    }
}
