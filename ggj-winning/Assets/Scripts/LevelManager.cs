using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [SerializeField]
    private Level[] _Levels =  new Level[3];
    private int maxLevels = 0;

    private void Awake()
    {
        if (!Instance) {
            Instance = this;
        }
        // TODO Selecting level
        //   -- Switching to next level
        //   -- Create levels

        maxLevels = _Levels.Length;
        // for (int i = 0; i < maxLevels; i += 1) {
        //     _Levels[i] = new Level(i);
        // }

        Instance.SetUpLevel(0);
    }

    public void SetUpLevel (int levelId)
    {
        if (levelId < maxLevels) {
            _Levels[levelId].SetUp();
        }
    }
}