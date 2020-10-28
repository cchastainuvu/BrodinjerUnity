using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "Character/Enemy/Boss/Ribs/Drop")]
public class Rib_Cage_Drop : Enemy_Patrol
{
    public float standStillTime;
    public float delayTime;
    public float waitSecondsAmount;
    private float currentTime;
    private Rigidbody _rigidbody;
    public float RotateSpeed;
    private Vector3 destination;

    protected override void Init(NavMeshAgent agent, MonoBehaviour caller, Animator anim)
    {
        base.Init(agent, caller, anim);
        _rigidbody = agent.GetComponent<Rigidbody>();
        if (_rigidbody == null)
        {
            _rigidbody = agent.gameObject.AddComponent<Rigidbody>();
        }

        _rigidbody.isKinematic = true;
        _rigidbody.useGravity = false;

    }

    public override IEnumerator Move()
    {
        while (moving)
        {
            while (!CheckPosition(followObj.transform.position))
            {
                destination = followObj.transform.position;
                destination.y = agent.transform.position.y;
                agent.destination = destination;
                yield return new WaitForFixedUpdate();
            }
            currentTime = 0;
            while (currentTime < standStillTime && CheckPosition(followObj.transform.position))
            {
                currentTime += waitSecondsAmount;
                yield return new WaitForSeconds(waitSecondsAmount);
            }
            yield return new WaitForSeconds(delayTime);
            Drop();
            yield return new WaitForFixedUpdate();
        }
    }

    public void Drop()
    {
        agent.enabled = false;
        _rigidbody.isKinematic = false;
        _rigidbody.useGravity = true;
        caller.StartCoroutine(Rotate());
    }

    private IEnumerator Rotate()
    {
        currentTime = 180 / RotateSpeed;
        while (currentTime >= 0)
        {
            agent.transform.Rotate(-RotateSpeed * Time.deltaTime, 0, 0,
                Space.Self);
            currentTime -= Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }   
    }

    public override Enemy_Movement GetClone()
    {
        Rib_Cage_Drop temp = CreateInstance<Rib_Cage_Drop>();
        temp.Speed = Speed;
        temp.delayTime = delayTime;
        temp.RotateSpeed = RotateSpeed;
        temp.standStillTime = standStillTime;
        temp.waitSecondsAmount = waitSecondsAmount;
        temp.checkX = checkX;
        temp.checkY = checkY;
        temp.checkZ = checkZ;
        temp.DestinationOffset = DestinationOffset;
        return temp;
    }

    public override IEnumerator ChangeDest()
    {
        agent.enabled = false;
        agent.GetComponent<Rigidbody>();
        yield return new WaitForFixedUpdate();
    }
    
}
