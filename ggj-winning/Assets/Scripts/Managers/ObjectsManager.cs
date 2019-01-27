using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsManager : MonoBehaviour
{
    [HideInInspector] public static ObjectsManager Instance = null;

    [Serializable]
    public struct Object
    {
        public string name;
        public ObjectController objectItem;
    }

    public Object[] tutorials;
    public Dictionary<string, ObjectController> tutorialDictionary = new Dictionary<string, ObjectController>();
    public GameObject HealthTextObjectPrefab, spellPrefab;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
