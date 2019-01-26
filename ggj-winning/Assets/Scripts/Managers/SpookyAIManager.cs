using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpookyAIManager : MonoBehaviour
{
    [HideInInspector] public static SpookyAIManager Instance = null;

    public int objectLifes = 3;

    [Range(0.01f, 1.0f)] public float difficultyIncreaseSpeedScale = 0.5f;
    [Range(1, 10)] public float maxSimultaneousAttacks = 3;
    [Range(1.0f, 12.0f)] public float minimumAttackDelay = 4.0f, maximumAttackDelay = 8.0f;
    private float originalMaxSimultaneousAttacks,  originalMinimumAttackDelay, originalMaximumAttackDelay;

    [HideInInspector] public ObjectController[] allObjects;
    private Coroutine ai;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

        originalMaxSimultaneousAttacks = maxSimultaneousAttacks;
        originalMinimumAttackDelay = minimumAttackDelay;
        originalMaximumAttackDelay = maximumAttackDelay;

        allObjects = FindObjectsOfType<ObjectController>();
        StartAI();
    }

    public void StartAI()
    {
        StopAI();
        ai = StartCoroutine(SelectObjects(minimumAttackDelay, maximumAttackDelay, 5));
    }

    public void StopAI()
    {
        if (ai == null)
            return;

        StopCoroutine(ai);
        ai = null;

        maxSimultaneousAttacks = originalMaxSimultaneousAttacks;
        minimumAttackDelay = originalMinimumAttackDelay;
        maximumAttackDelay = originalMaximumAttackDelay;
    }

    private IEnumerator SelectObjects(float minDelay, float maxDelay, int attackAmount)
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));

            ObjectController[] idleObjects = allObjects.Where(obj => obj.state == ObjectState.IDLE).ToArray();
            int shookObjectsAmount = allObjects.Where(obj => obj.state == ObjectState.SHOOK).Count();

            if (idleObjects.Length > 0 && shookObjectsAmount < Mathf.Floor(maxSimultaneousAttacks))
            {
                idleObjects[Random.Range(0, idleObjects.Length)].BeginShake(attackAmount);

                maxSimultaneousAttacks += 0.2f + (difficultyIncreaseSpeedScale * 0.2f);
                minimumAttackDelay *= 0.99f - (difficultyIncreaseSpeedScale * 0.1f);
                maximumAttackDelay *= 0.99f - (difficultyIncreaseSpeedScale * 0.1f);
                MusicPlayer.ChangeMusicPitch(0.025f);
            }
        }
    }
}
