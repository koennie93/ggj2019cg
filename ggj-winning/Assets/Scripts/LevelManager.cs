using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [SerializeField]
    private GameObject[] _Levels =  new GameObject[3];
    private int maxLevels = 0;
    private int currentLevel;

    [SerializeField]
    private EndScreen endScreen;
    [SerializeField]
    private GameObject playerScores;
    [SerializeField]
    private GameObject healthBars;

    private void Awake()
    {
        if (Instance == null) {
            Instance = this;
        }
        // TODO Selecting level
        //   -- Switching to next level
        //   -- Create levels

        maxLevels = _Levels.Length;
        // for (int i = 0; i < maxLevels; i += 1) {
        //     _Levels[i] = new Level(i);
        // }

        
    }

    private void Start()
    {
        StartCoroutine("StartFadeIn");
    }

    public void SetUpLevel (int levelId)
    {
        _Levels[levelId].SetActive(true);
        currentLevel = levelId;
        playerScores.SetActive(true);
        healthBars.SetActive(true);

        foreach (KeyValuePair<int, PlayerControl> player in ControllerManager.Instance.controlDictionary)
        {
            player.Value.transform.position = Vector3.zero;
        }

        //if (levelId < maxLevels) {
        //    _Levels[levelId].SetUp();
        //}

    }

    public void CloseLevel()
    {
        _Levels[currentLevel].SetActive(false);
        playerScores.SetActive(false);
        healthBars.SetActive(false);

        foreach (KeyValuePair<int, PlayerControl> player in ControllerManager.Instance.controlDictionary)
        {
            player.Value.transform.position = new Vector3(100, 100, 100);
        }
    }

    private IEnumerator FadeMenu()
    {
        CanvasGroup canvasGroup = GameObject.FindGameObjectWithTag("Loading").GetComponent<CanvasGroup>();
        while (canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += Time.deltaTime;
            yield return null;
        }
        GameObject.FindGameObjectWithTag("MainMenu").SetActive(false);
        SetUpLevel(0);
        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator FadeEndScreen()
    {
        CanvasGroup canvasGroup = GameObject.FindGameObjectWithTag("Loading").GetComponent<CanvasGroup>();
        while (canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += Time.deltaTime;
            yield return null;
        }
        //GameObject.FindGameObjectWithTag("MainMenu").SetActive(false);
        CloseLevel();
        SetEndScreen();
        endScreen.gameObject.SetActive(true);
        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= Time.deltaTime;
            yield return null;
        }

        //Application.LoadLevel("Scene2");
    }

    private IEnumerator StartFadeIn()
    {
        CanvasGroup canvasGroup = GameObject.FindGameObjectWithTag("Loading").GetComponent<CanvasGroup>();
        
        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= Time.deltaTime;
            yield return null;
        }

        //Application.LoadLevel("Scene2");
    }

    private IEnumerator ResetGame()
    {
        CanvasGroup canvasGroup = GameObject.FindGameObjectWithTag("Loading").GetComponent<CanvasGroup>();
        while (canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += Time.deltaTime;
            yield return null;
        }
        Application.LoadLevel(Application.loadedLevel);
    }

    private void SetEndScreen()
    {
        float[] scoreOrder = ControllerManager.Instance.controlDictionary.OrderByDescending(c => c.Value.score).Select(c => c.Value.score).ToArray();
        Debug.Log(scoreOrder.Length);
        for (int i = 0; i < scoreOrder.Length; i++)
        {
            for (int j = 0; j < ControllerManager.Instance.controlDictionary.Count; j++)    
            {
                if (scoreOrder[i] == ControllerManager.Instance.controlDictionary.ElementAt(j).Value.score)
                {
                    endScreen.SetText(i + 1, "Player" + (j + 1) + " - " + scoreOrder[i] + " Points");
                }
            }
        }
    }
}