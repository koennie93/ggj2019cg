using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text playerOneText;
    public Text playerTwoText;

    public Text playerOneScore;
    public Text playerTwoScore;

    public Vector3 newPosition;

    public string message;

    // Start is called before the first frame update
    void Start()
    {
        playerOneScore.text = "0";
        playerTwoScore.text = "0";

        SetPlayerOneScore("1000");
    }

    void SetPlayerOneScore(string score)
    {
        playerOneScore.text = "";
        StartCoroutine(TypeText(score, playerOneScore));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            playerOneText.transform.position += newPosition;
        }
    }

    IEnumerator TypeText(string text, Text textElement)
    {
        foreach (char letter in text.ToCharArray())
        {
            textElement.text += letter;

            yield return new WaitForSeconds(1);
        }
    }
}
