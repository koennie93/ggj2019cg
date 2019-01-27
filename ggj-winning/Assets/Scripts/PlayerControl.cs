using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{

    public float speed = 0.1f;
    public bool xButton = false;
    public GameObject collidedObject = null;
    public float score = 0;
    
    private Vector3 moveDirection = Vector3.zero;

    private UIManager uiManager;
    private GameObject currentPlayer;


    // Start is called before the first frame update
    void Start()
    {
        currentPlayer = transform.root.gameObject;
        uiManager = GameObject.Find("UI").GetComponent<UIManager>();
    }

    private void SendScoreToUI(float Score)
    {
        switch (currentPlayer.name)
        {
            case "Player1":
                uiManager.SetPlayerOneScore("Player 1: " + Score.ToString());
                break;

            case "Player2":
                uiManager.SetPlayerTwoScore("Player 2: " + Score.ToString());
                break;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "RoomObject" && xButton)
        {
            if (collision.transform.parent.GetComponent<ObjectController>().currentHP < collision.transform.parent.GetComponent<ObjectController>().MaxHP && collision.transform.parent.GetComponent<ObjectController>().currentHP > 0)
            {
                collision.transform.parent.GetComponent<ObjectController>().ChangeHP(1);            
                score++;
                SendScoreToUI(score);
                AudioManager.Instance.PlaySound(gameObject, "repair");
                GameObject spell = Instantiate(ObjectsManager.Instance.spellPrefab, transform.position + new Vector3(0 , 1, 0), Quaternion.identity);
                GameObject healthTextObject = Instantiate(ObjectsManager.Instance.HealthTextObjectPrefab, transform.position, Quaternion.identity);
                healthTextObject.GetComponent<TextMesh>().text = "+1";
                healthTextObject.GetComponent<TextMesh>().color = Color.green;
            }                
            xButton = false;
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
