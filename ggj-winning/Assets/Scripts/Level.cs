using UnityEngine;

public class Level : MonoBehaviour {

    [SerializeField]
    private int _id;
    public int id { get { return _id; } }
    private float _targetTime = 10.0f;
    private bool _started = false;
    
    // public Level (int id)
    // {
    //     _id = id;
    //     Debug.Log("Level "+_id+" is created!");
    // }

    void Update ()
    {
        if (!_started) return;
        Debug.Log("time left " + _targetTime + " in level " + id);

        // TODO Remove the timer to a real end of the level
        _targetTime -= Time.deltaTime;
        if (_targetTime <= 0.0f) {
            _Finished();
            _started = false;
        }
    }

    private void _DestroyLevel ()
    {
        // TODO Remove level stuff
        Debug.Log("Destroyed level "+id);
    }

    private void _Finished ()
    {
        Debug.Log("Level "+id+" is finished");
        _DestroyLevel();
        LevelManager.Instance.SetUpLevel(id+1);
    }

    public void SetUp ()
    {
        _started = true;
    }
}