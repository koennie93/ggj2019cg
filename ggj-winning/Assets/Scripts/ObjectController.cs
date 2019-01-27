using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Linq;

public enum ObjectState
{
    IDLE,
    SHOOK,
    DESTROYED
}

public class ObjectController : MonoBehaviour
{
    [SerializeField]
    private GameObject explosionPrefab;

    [SerializeField]
    private SpriteRenderer sprite;

    [SerializeField]
    private Vector3 endRotation;
    private Vector3 originalRotation;
    private float shakeDuration = 0.1f;
    [SerializeField]
    public float MaxHP;
    public float currentHP;
    public float dmgPerHit;
    public Image healthBar; 

    [HideInInspector]
    public ObjectState state;

    // Start is called before the first frame update
    void Start()
    {
        state = ObjectState.IDLE;
        currentHP = MaxHP;
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

        s.Append(sprite.transform.DORotate(originalRotation + endRotation / 3, shakeDuration).SetEase(Ease.InQuad).SetLoops(5, LoopType.Yoyo));
        s.Append(sprite.transform.DORotate(originalRotation, shakeDuration));

        StartCoroutine(Shake(attackAmount));
    }

    public void ChangeHP(float addedValue)
    {
        if(addedValue < 0)
        {
            AudioManager.Instance.PlaySound(gameObject, gameObject.name + "Sound");
            GameObject healthTextObject = Instantiate(ObjectsManager.Instance.HealthTextObjectPrefab, sprite.transform.position, Quaternion.identity);
            healthTextObject.GetComponent<TextMesh>().text = addedValue.ToString();
            healthTextObject.GetComponent<TextMesh>().color = Color.red;
        }

        currentHP += addedValue;
        healthBar.DOFillAmount((1 / MaxHP * currentHP), 0.5f);
        if (currentHP > MaxHP) currentHP = MaxHP;
        if (currentHP <= 0)
        {

            AudioManager.Instance.PlaySound(gameObject, "explosionSound", 1.0f, false);
            Instantiate(explosionPrefab, sprite.transform.position, Quaternion.identity);
            sprite.gameObject.SetActive(false);
            healthBar.transform.parent.gameObject.SetActive(false);
            state = ObjectState.DESTROYED;
            currentHP = 0;

            SpookyAIManager.Instance.livesLeftText.text = string.Format("{0}\tLives Left", SpookyAIManager.Instance.objectLifes - SpookyAIManager.Instance.allObjects.Where(obj => obj.state == ObjectState.DESTROYED).Count());

            if (SpookyAIManager.Instance.allObjects.Where(obj => obj.state == ObjectState.DESTROYED).Count() >= SpookyAIManager.Instance.objectLifes)
            {
                Debug.Log("Game Over.");
                LevelManager.Instance.StartCoroutine("FadeEndScreen");
            }
            //TO-DO Call Destroy? method.
        }      
    }

    private IEnumerator Shake(int attackAmount)
    {
        while(attackAmount > 0)
        {
            yield return new WaitForSeconds(SpookyAIManager.Instance.attackDelay);
            attackAmount--;
            ChangeHP(-dmgPerHit);

            originalRotation = sprite.transform.localRotation.eulerAngles;
            Sequence s = DOTween.Sequence();
            s.Append(sprite.transform.DORotate(originalRotation + endRotation, shakeDuration).SetEase(Ease.InQuad).SetLoops(5, LoopType.Yoyo));
            s.Append(sprite.transform.DORotate(originalRotation, shakeDuration));

            if (state == ObjectState.DESTROYED)
                break;
        }

        yield return new WaitForSeconds(10);

        if (state != ObjectState.DESTROYED)
            state = ObjectState.IDLE;
    }

    
}