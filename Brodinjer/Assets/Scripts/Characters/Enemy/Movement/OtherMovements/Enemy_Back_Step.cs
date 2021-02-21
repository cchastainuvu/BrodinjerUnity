using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Enemy_Back_Step : NavMesh_Enemy_Base
{
    public float distanceAway;
    public bool turnAway;
    public SoundController walkSound;
    public float MinFootstep, MaxFootstep;
    protected override void Init()
    {
        base.Init();
        if (!turnAway)
        {
            agent.updateRotation = false;
        }
        else
        {
            agent.updateRotation = true;
        }
    }

    public override IEnumerator Move()
    {
        StartCoroutine(WalkSound());
        agent.speed = Speed;
        while (moving)
        {
            if(agent.enabled)
                agent.destination = player.transform.position + (agent.transform.forward * -distanceAway);
            yield return fixedUpdate;
        }
    }

    private IEnumerator WalkSound()
    {
        while (moving)
        {
            if (agent.velocity.magnitude >= .1f)
            {
                walkSound.Play();
                yield return new WaitForSeconds(GeneralFunctions.ConvertRange(0, Speed, MaxFootstep, MinFootstep, agent.velocity.magnitude));
            }
            yield return new WaitForFixedUpdate();
        }
    }
}
