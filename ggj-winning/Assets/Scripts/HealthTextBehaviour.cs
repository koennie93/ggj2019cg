using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthTextBehaviour : MonoBehaviour
{
    private TextMesh tm;

    // Start is called before the first frame update
    void Start()
    {
        tm = GetComponent<TextMesh>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate((tm.color == Color.red ? Vector3.down : Vector3.up) * Time.deltaTime);

        Color tempCol = tm.color;
        tempCol.a -= Time.deltaTime / 2;
        tm.color = tempCol;

        if (tm.color.a <= 0)
            Destroy(gameObject);
    }

}
