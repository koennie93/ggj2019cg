using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ObjectController : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer sprite;

    private float currentHP;
    private float dmgPerHit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            BeginShake();
        }
    }

    void BeginShake()
    {
        //sprite.transform.DOMoveX(5, 2);
        //sprite.transform.DOMoveX(0, 1).From();
        Sequence s = DOTween.Sequence();

        s.Append(transform.DORotate(new Vector3(0, 50, 0), 2));
    }
}
