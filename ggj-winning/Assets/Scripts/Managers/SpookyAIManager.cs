using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpookyAIManager : MonoBehaviour
{
    [HideInInspector] public static SpookyAIManager Instance = null;

    public int objectLifes = 3;
    public TextMesh livesLeftText, introText0, introText1;

    [Range(0.01f, 1.0f)] public float difficultyIncreaseSpeedScale = 0.5f;
    [Range(1, 10)] public float maxSimultaneousAttacks = 3;
    [Range(1.0f, 12.0f)] public float attackDelay = 4.0f;
    private float originalMaxSimultaneousAttacks,  originalMinimumAttackDelay;

    [HideInInspector] public ObjectController[] allObjects;
    private Coroutine ai;

    private int minTime = 7, maxTime = 7;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

        originalMaxSimultaneousAttacks = maxSimultaneousAttacks;
        originalMinimumAttackDelay = attackDelay;

        allObjects = FindObjectsOfType<ObjectController>();
        StartAI();
    }

    public void StartAI()
    {
        StopAI();
        ai = StartCoroutine(SelectObjects(attackDelay, 5));
        livesLeftText.text = string.Format("{0}\tLives Left", objectLifes);
        StartCoroutine(FadeInIntroText());
    }

    public void StopAI()
    {
        if (ai == null)
            return;

        StopCoroutine(ai);
        ai = null;

        maxSimultaneousAttacks = originalMaxSimultaneousAttacks;
        attackDelay = originalMinimumAttackDelay;
    }

    private IEnumerator SelectObjects(float minDelay, int attackAmount)
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minTime, maxTime));
            minTime = 4;
            maxTime = 8;
            ObjectController[] idleObjects = allObjects.Where(obj => obj.state == ObjectState.IDLE).ToArray();
            int shookObjectsAmount = allObjects.Where(obj => obj.state == ObjectState.SHOOK).Count();

            if (idleObjects.Length > 0 && shookObjectsAmount < Mathf.Floor(maxSimultaneousAttacks))
            {
                idleObjects[Random.Range(0, idleObjects.Length)].BeginShake(attackAmount);
                idleObjects[Random.Range(0, idleObjects.Length)].dmgPerHit *= 1.1f;
                maxSimultaneousAttacks += 0.2f + (difficultyIncreaseSpeedScale * 0.2f);
                attackDelay = Mathf.Clamp(attackDelay * (0.99f - (difficultyIncreaseSpeedScale * 0.1f)), 2.5f, Mathf.Infinity);

                MusicPlayer.ChangeMusicPitch(0.025f);
            }
        }
    }

    IEnumerator FadeInIntroText ()
    {
        // Text 0
        introText0.gameObject.SetActive(true);
        while(introText0.color.a < 1)
        {
            yield return new WaitForEndOfFrame();
            Color temp = introText0.color;
            temp.a += Time.deltaTime;
            introText0.color = temp;
        }
        yield return new WaitForSeconds(1.5f);
        while (introText0.color.a > 0)
        {
            yield return new WaitForEndOfFrame();
            Color temp = introText0.color;
            temp.a -= Time.deltaTime;
            introText0.color = temp;
        }
        introText0.gameObject.SetActive(false);

        // Text 1
        introText1.gameObject.SetActive(true);
        while (introText1.color.a < 1)
        {
            yield return new WaitForEndOfFrame();
            Color temp = introText1.color;
            temp.a += Time.deltaTime;
            introText1.color = temp;
        }
        yield return new WaitForSeconds(1.5f);
        while (introText1.color.a > 0)
        {
            yield return new WaitForEndOfFrame();
            Color temp = introText1.color;
            temp.a -= Time.deltaTime;
            introText1.color = temp;
        }
        introText1.gameObject.SetActive(false);
    }
}
