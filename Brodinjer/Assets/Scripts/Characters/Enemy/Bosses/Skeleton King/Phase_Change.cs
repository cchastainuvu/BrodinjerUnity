using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase_Change : MonoBehaviour
{
    public List<Rigidbody> DropObjects;
    public List<Animator> DropAnimators;
    private int currentObj;
    private int count;

    public void Drop()
    {
        StartCoroutine(DropStalactites());
    }

    public void AnimateDrop(string triggerName)
    {
        StartCoroutine(DropAnimatedStalactites(triggerName));
    }

    private IEnumerator DropStalactites()
    {
        count = DropObjects.Count;
        for(int i = 0; i < count; i++)
        {
            currentObj = Random.Range(0, DropObjects.Count);
            DropObjects[currentObj].isKinematic = false;
            DropObjects[currentObj].useGravity = true;
            DropObjects.RemoveAt(currentObj);
            yield return new WaitForSeconds(Random.Range(.1f, .75f));
        }
    }

    private IEnumerator DropAnimatedStalactites(string triggerName)
    {
        count = DropAnimators.Count;
        for (int i = 0; i < count; i++)
        {
            currentObj = Random.Range(0, DropAnimators.Count);
            DropAnimators[currentObj].SetTrigger(triggerName);
            DropAnimators.RemoveAt(currentObj);
            yield return new WaitForSeconds(Random.Range(.1f, .5f));
        }
    }

}
