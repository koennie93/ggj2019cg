using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum ObjectState
{
    IDLE,
    SHOOK,
    DESTROYED
}

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

    [HideInInspector]
    public ObjectState state;

    // Start is called before the first frame update
    void Start()
    {
        state = ObjectState.IDLE;
        currentHP = MaxHP;
        HPText.text = currentHP + "/" + MaxHP;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            BeginShake(0);
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

    public void BeginShake(int attackAmount)
    {
        state = ObjectState.SHOOK;
        originalRotation = sprite.transform.localRotation.eulerAngles;
        Sequence s = DOTween.Sequence();

        s.Append(sprite.transform.DORotate(originalRotation + endRotation, shakeDuration).SetEase(Ease.InQuad).SetLoops(5, LoopType.Yoyo));
        s.Append(sprite.transform.DORotate(originalRotation, shakeDuration));

        StartCoroutine(Shake(attackAmount));
    }

    public void ChangeHP(float addedValue)
    {
        currentHP += addedValue;
        if (currentHP > MaxHP) currentHP = MaxHP;
        if (currentHP <= 0)
        {
            state = ObjectState.DESTROYED;
            currentHP = 0;
            //TO-DO Call Destroy? method.
        }

        HPText.text = Mathf.Round(currentHP) + "/" + MaxHP;
    }

    private IEnumerator Shake(int attackAmount)
    {
        while(attackAmount > 0)
        {
            yield return new WaitForSeconds(3);
            attackAmount--;
            ChangeHP(-dmgPerHit);

            originalRotation = sprite.transform.localRotation.eulerAngles;
            Sequence s = DOTween.Sequence();
            s.Append(sprite.transform.DORotate(originalRotation + endRotation / 3, shakeDuration).SetEase(Ease.InQuad).SetLoops(5, LoopType.Yoyo));
            s.Append(sprite.transform.DORotate(originalRotation, shakeDuration));

            if (state == ObjectState.DESTROYED)
                break;
        }

        yield return new WaitForSeconds(SpookyAIManager.Instance.maximumAttackDelay + 0.1f);

        if (state != ObjectState.DESTROYED)
            state = ObjectState.IDLE;
    }
    
}