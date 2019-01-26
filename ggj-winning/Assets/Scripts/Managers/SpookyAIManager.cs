using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpookyAIManager : MonoBehaviour
{
    [Range(1, 10)] public int maxSimultaneousAttacks = 3;
    [Range(1.0f, 12.0f)] public float minimumAttackDelay = 4.0f, maximumAttackDelay = 8.0f;

    private ObjectController[] allObjects;
    private Coroutine ai;

    private void Awake()
    {
        allObjects = FindObjectsOfType<ObjectController>();
        StartAI();
        StopAI();
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
    }

    private IEnumerator SelectObjects(float minDelay, float maxDelay, int attackAmount)
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));
            ObjectController[] idleObjects = allObjects.Where(obj => obj.state == ObjectState.IDLE).ToArray();
            int shookObjectsAmount = allObjects.Where(obj => obj.state == ObjectState.SHOOK).Count();
            if (idleObjects.Length > 0 && shookObjectsAmount < maxSimultaneousAttacks)
                idleObjects[Random.Range(0, idleObjects.Length)].BeginShake(attackAmount);
        }
    }
}
