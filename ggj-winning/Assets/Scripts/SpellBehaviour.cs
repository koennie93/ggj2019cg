using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellBehaviour : MonoBehaviour
{
    void Start()
    {
        StartCoroutine("Destroyer");
    }

    private IEnumerator Destroyer()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
