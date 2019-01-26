﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    public float speed = 0.1f;
    public bool xButton = false;
    public GameObject collidedObject = null;
    public float score = 0;


    private Vector3 moveDirection = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (collidedObject != null)
        {
            Debug.Log(xButton);
            if (Vector2.Distance(collidedObject.transform.position, transform.position) < 5 && xButton)
            {
                if (collidedObject.GetComponent<ObjectController>().currentHP < 250 && collidedObject.GetComponent<ObjectController>().currentHP > 0)
                {
                    collidedObject.GetComponent<ObjectController>().ChangeHP(1);
                    score++;
                }
                xButton = false;
            }
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "RoomObject")
        {
            collidedObject = collision.transform.parent.gameObject;
        }
    }

    public void Move(int i)
    {
        moveDirection = new Vector2(Input.GetAxis("Joy" + i + "X"), Input.GetAxis("Joy" + i + "Y"));
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection = moveDirection * speed;

        transform.position += moveDirection;
    }

    public void XButtonPressed(bool status)
    {
        xButton = status;
        //Debug.Log(xButton);
        //StartCoroutine("XPress", 1);
    }

    private IEnumerator XPress()
    {
        yield return new WaitForEndOfFrame();
        xButton = false;
    }
}
