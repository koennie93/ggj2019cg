using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text playerOneScore;
    public Text playerTwoScore;
    
    public string message;

    // Start is called before the first frame update
    void Start()
    {
        playerOneScore.text = "0";
        playerTwoScore.text = "0";
    }

    public void SetPlayerOneScore(string score)
    {
        playerOneScore.text = score;
    }

    public void SetPlayerTwoScore(string score)
    {
        playerTwoScore.text = score;
    }

    // Update is called once per frame
    void Update()
    {
    }

}
