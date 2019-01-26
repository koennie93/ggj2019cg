using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScreen : MonoBehaviour
{

    public TextMesh player1;
    public TextMesh player2;
    public TextMesh player3;
    public TextMesh player4;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.JoystickButton9))
        {
            LevelManager.Instance.StartCoroutine("ResetGame");
        }
    }

    public void SetText(int rank, string text)
    {
        switch (rank)
        {
            case 1:
                player1.gameObject.SetActive(true);
                player1.text = text;
                break;
            case 2:
                player2.gameObject.SetActive(true);
                player2.text = text;
                break;
            case 3:
                player3.gameObject.SetActive(true);
                player3.text = text;
                break;
            case 4:
                player4.gameObject.SetActive(true);
                player4.text = text;
                break;
            default:
                break;
        }
    }
}
