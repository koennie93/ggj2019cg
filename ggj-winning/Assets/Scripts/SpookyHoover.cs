using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpookyHoover : MonoBehaviour
{
    Vector3 originalPos;

    private void Start()
    {
        originalPos = transform.position;
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x, originalPos.y + Mathf.Sin(Time.time), transform.position.z);
    }
}
