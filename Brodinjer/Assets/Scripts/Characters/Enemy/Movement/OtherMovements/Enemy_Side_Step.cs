using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[CreateAssetMenu(menuName = "Character/Enemy/Movement/SideStep")]
public class Enemy_Side_Step : Enemy_Movement
{
    public float sideAmount;
    public float DestinationOffset;
    private Vector3 right_dest, left_dest, _currDest;
    private bool _right = true;
    public float minTimeWait, maxTimeWait;
    public bool RotateToPlayer;

    protected override void Init(NavMeshAgent agent, MonoBehaviour caller, Animator anim)
    {
        base.Init(agent, caller, anim);
        right_dest = enemy.transform.position;
        right_dest += enemy.transform.right * sideAmount;
        left_dest = enemy.transform.position;
        left_dest += enemy.transform.right * -sideAmount;
        _currDest = right_dest;
        agent.destination = _currDest;
        _right = true;
    }

    public override IEnumerator Move()
    {
        agent.updateRotation = false;
        while (moving)
        {
            if (CheckPosition(_currDest))
            {
                caller.StartCoroutine(ChangeDest());
            }

            if (RotateToPlayer)
            {
                agent.gameObject.transform.LookAt(followObj);
            }
            yield return new WaitForSeconds(.1f);
        }
    }

    public override Enemy_Movement GetClone()
    {
        Enemy_Side_Step temp = CreateInstance<Enemy_Side_Step>();
        temp.sideAmount = sideAmount;
        temp.DestinationOffset = DestinationOffset;
        temp.minTimeWait = minTimeWait;
        temp.maxTimeWait = maxTimeWait;
        temp.Speed = Speed;
        temp.AngularSpeed = AngularSpeed;
        return temp;
    }
    
    public virtual bool CheckPosition(Vector3 position)
    {
        if ((enemy.transform.position.x >= position.x - DestinationOffset
              && enemy.transform.position.x <= position.x + DestinationOffset)
            && (enemy.transform.position.z >= position.z - DestinationOffset
                 && enemy.transform.position.z <= position.z + DestinationOffset))
        {
            return true;
        }
        return false;
    }

    public virtual IEnumerator ChangeDest()
    {
        Debug.Log("Change Dest");
        agent.speed = 0;
        if (_right)
        {
            _currDest = left_dest;
            _right = false;
        }
        else
        {
            _currDest = right_dest;
            _right = true;
        }
        yield return new WaitForSeconds(GetTime());
        agent.destination = _currDest;
        agent.speed = Speed;

    }

    public float GetTime()
    {
        return Random.Range(minTimeWait, maxTimeWait);
    }

    
    
}
