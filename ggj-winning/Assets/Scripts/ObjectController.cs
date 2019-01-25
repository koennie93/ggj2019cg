using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ObjectController : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer sprite;

    [SerializeField]
    private TextMesh HPText;

    [SerializeField]
    private Vector3 endRotation;
    private Vector3 originalRotation;
    private float shakeDuration = 0.1f;
    [SerializeField]
    private float MaxHP;
    private float currentHP;
    [SerializeField]
    private float dmgPerHit;

    // Start is called before the first frame update
    void Start()
    {
        currentHP = MaxHP;
        HPText.text = currentHP + "/" + MaxHP;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            BeginShake();
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            ChangeHP(-10);
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            ChangeHP(5);
        }
    }

    void BeginShake()
    {
        originalRotation = sprite.transform.localRotation.eulerAngles;
        Sequence s = DOTween.Sequence();

        s.Append(sprite.transform.DORotate(originalRotation + endRotation, shakeDuration).SetEase(Ease.InQuad).SetLoops(5, LoopType.Yoyo));
        s.Append(sprite.transform.DORotate(originalRotation, shakeDuration));
       
    }

    void ChangeHP(float addedValue)
    {
        currentHP += addedValue;
        if (currentHP > MaxHP) currentHP = MaxHP;
        if (currentHP < 0)
        {
            currentHP = 0;
            //TO-DO Call Destroy? method.
        }

        HPText.text = currentHP + "/" + MaxHP;
    }
}
