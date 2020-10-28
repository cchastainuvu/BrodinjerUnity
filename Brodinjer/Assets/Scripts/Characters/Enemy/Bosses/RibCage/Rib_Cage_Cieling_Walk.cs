using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "Character/Enemy/Boss/Ribs/CielingWalk")]
public class Rib_Cage_Cieling_Walk : Enemy_Follow_Base
{
    public float HitDistance, RotateSpeed, TranslateAmount;
    public TransformPosition StartPosition;
    public float CielingHeight;
    public float standStillTime;
    public float delayTime;
    public float waitSecondsAmount;
    public float DestinationOffset;

    private RaycastHit hit;
    private float currentTime, translateSpeed, rotateySpeed, yrotation, rotatezSpeed, zrotation, xrotation;
    private Vector3 newRotation, newTranslation;
    private bool climbing, running;
    private TransformPosition currentPos, newPos;
    private Vector3 newDestination;
    private Rigidbody _rigidbody;

    public enum TransformPosition
    {
        Floor,
        WallX,
        WallZ,
        Cieling,
        Drop
    }
    
    public virtual bool CheckPosition(Vector3 position)
    {
        if ((agent.transform.position.x >= position.x - DestinationOffset
              && agent.transform.position.x <= position.x + DestinationOffset)
            && (agent.transform.position.z >= position.z - DestinationOffset
                 && agent.transform.position.z <= position.z + DestinationOffset))
        {
            return true;
        }
        return false;
    }
    
    public override Enemy_Movement GetClone()
    {
        Rib_Cage_Cieling_Walk temp = CreateInstance<Rib_Cage_Cieling_Walk>();
        
        temp.Speed = Speed;
        temp.HitDistance = HitDistance;
        temp.RotateSpeed = RotateSpeed;
        temp.TranslateAmount = TranslateAmount;
        temp.StartPosition = StartPosition;
        temp.CielingHeight = CielingHeight;
        temp.DestinationOffset = DestinationOffset;
        temp.delayTime = delayTime;
        temp.standStillTime = standStillTime;
        temp.waitSecondsAmount = waitSecondsAmount;
        
        return temp;
    }

    protected override void Init(NavMeshAgent agent, MonoBehaviour caller, Transform FollowObj, Animator anim)
    {
        climbing = false;
        currentPos = StartPosition;
        base.Init(agent, caller, FollowObj, anim);
        _rigidbody = agent.GetComponent<Rigidbody>();
        if (_rigidbody == null)
        {
            _rigidbody = agent.gameObject.AddComponent<Rigidbody>();
        }

        _rigidbody.isKinematic = true;
        _rigidbody.useGravity = false;
    }

    private void SetAgentDestination()
    {
        if (agent.enabled)
        {
            newDestination = followObj.transform.position;
            newDestination.y += CielingHeight;
            agent.destination = newDestination;
        }
    }

    private IEnumerator Drop()
    {
        agent.enabled = false;
        _rigidbody.isKinematic = false;
        _rigidbody.useGravity = true;
        currentTime = 90 / (RotateSpeed);
        while (currentTime >= 0)
        {
            agent.transform.Rotate(-RotateSpeed * Time.deltaTime, 0, 0,
                Space.Self);
            currentTime -= Time.deltaTime;
            yield return new WaitForFixedUpdate();
        } 
    }

    public override IEnumerator Move()
    {
        if (!running)
        {
            agent.enabled = true;
            _rigidbody.isKinematic = true;
            _rigidbody.useGravity = false;
            running = true;
            while (!CheckPosition(followObj.transform.position))
            {
                if (!climbing)
                    SetAgentDestination();
                if (Physics.Raycast(agent.transform.position, agent.transform.forward, out hit, HitDistance))
                    caller.StartCoroutine(Call());
                yield return new WaitForFixedUpdate();
            }

            currentTime = 0;
            while (currentTime < standStillTime && CheckPosition(followObj.transform.position))
            {
                currentTime += waitSecondsAmount;
                yield return new WaitForSeconds(waitSecondsAmount);
            }

            yield return new WaitForSeconds(delayTime);
            caller.StartCoroutine(Drop());
            yield return new WaitForFixedUpdate();
            running = false;
        }
    }

    private IEnumerator Call()
    {
        if (!climbing)
        {
            climbing = true;
            agent.updateRotation = false;
            agent.enabled = false;
            switch (currentPos)
            {
                
                case TransformPosition.Floor:
                    newPos = CheckTransformDirection();
                    currentTime = 90 / RotateSpeed;
                    translateSpeed = TranslateAmount / currentTime;
                    yrotation = agent.transform.rotation.eulerAngles.y % 360;

                    if (yrotation > 180)
                        yrotation -= 360;

                    rotateySpeed = yrotation / currentTime;
                    while (currentTime >= 0)
                    {
                        agent.transform.Rotate(-RotateSpeed * Time.deltaTime, 0, 0,
                            Space.Self);
                        agent.transform.Translate(0, 0, translateSpeed * Time.deltaTime, agent.transform);
                        currentTime -= Time.deltaTime;
                        yield return new WaitForFixedUpdate();
                    }

                    agent.transform.Rotate(0, 0, -yrotation, Space.Self);
                    currentPos = newPos;
                    break;
                
                case TransformPosition.WallX:
                    newPos = CheckTransformDirection();
                    switch (newPos)
                    {
                        
                        case TransformPosition.WallZ:
                            currentTime = 90 / RotateSpeed;
                            translateSpeed = TranslateAmount / currentTime;
                            yrotation = agent.transform.rotation.eulerAngles.y % 360;
                            if (yrotation > 180)
                                yrotation -= 360;

                            rotateySpeed = yrotation / currentTime;
                            while (currentTime >= 0)
                            {
                                agent.transform.Rotate(-RotateSpeed * Time.deltaTime, 0, 0,
                                    Space.Self);
                                agent.transform.Translate(0, 0, translateSpeed * Time.deltaTime, agent.transform);
                                currentTime -= Time.deltaTime;
                                yield return new WaitForFixedUpdate();
                            }

                            agent.transform.Rotate(0, 0, -yrotation, Space.Self);
                            break;
                        
                        case TransformPosition.Cieling:
                            xrotation = (-180 - agent.transform.rotation.eulerAngles.x) % 360;
                            if (xrotation > 180)
                                xrotation -= 360;
                            currentTime = Mathf.Abs(xrotation / RotateSpeed);
                            translateSpeed = TranslateAmount / currentTime;
                            yrotation = (agent.transform.rotation.eulerAngles.y % 360) + 90 + (agent.transform.rotation.eulerAngles.x%90);
                            if (yrotation > 180)
                                yrotation -= 360;
                            zrotation = -90;
                            if (zrotation > 180)
                                zrotation -= 360;
                            while (currentTime >= 0)
                            {
                                agent.transform.Rotate(-1*RotateSpeed * Time.deltaTime, 0, 0,
                                    Space.Self);
                                agent.transform.Translate(0, 0, translateSpeed * Time.deltaTime, agent.transform);
                                currentTime -= Time.deltaTime;
                                yield return new WaitForFixedUpdate();
                            }
                            break;
                        
                        case TransformPosition.Floor:
                            xrotation = (180 - agent.transform.rotation.eulerAngles.x) % 360;
                            if (xrotation > 180)
                                xrotation -= 360;
                            currentTime = Mathf.Abs(xrotation / RotateSpeed);
                            translateSpeed = TranslateAmount / currentTime;
                            yrotation = (agent.transform.rotation.eulerAngles.y % 360) + 90 + (agent.transform.rotation.eulerAngles.x%90);
                            if (yrotation > 180)
                                yrotation -= 360;
                            zrotation = -90;
                            if (zrotation > 180)
                                zrotation -= 360;
                            while (currentTime >= 0)
                            {
                                agent.transform.Rotate(-1*RotateSpeed * Time.deltaTime, 0, 0,
                                    Space.Self);
                                agent.transform.Translate(0, 0, translateSpeed * Time.deltaTime, agent.transform);
                                currentTime -= Time.deltaTime;
                                yield return new WaitForFixedUpdate();
                            }
                            break;                        
                    }

                    currentPos = newPos;
                    break;
                
                case TransformPosition.WallZ:
                    newPos = CheckTransformDirection();
                    switch (newPos)
                    {
                        
                        case TransformPosition.WallX:
                            currentTime = 90 / RotateSpeed;
                            translateSpeed = TranslateAmount / currentTime;
                            yrotation = agent.transform.rotation.eulerAngles.y % 360;
                            if (yrotation > 180)
                                yrotation -= 360;
                            rotateySpeed = yrotation / currentTime;
                            while (currentTime >= 0)
                            {
                                agent.transform.Rotate(-RotateSpeed * Time.deltaTime, 0, 0,
                                    Space.Self);
                                agent.transform.Translate(0, 0, translateSpeed * Time.deltaTime, agent.transform);
                                currentTime -= Time.deltaTime;
                                yield return new WaitForFixedUpdate();
                            }

                            break;
                        
                        case TransformPosition.Cieling:
                            xrotation = (-180 - agent.transform.rotation.eulerAngles.x) % 360;
                            if (xrotation > 180)
                                xrotation -= 360;
                            currentTime = Mathf.Abs(xrotation / RotateSpeed);
                            translateSpeed = TranslateAmount / currentTime;
                            yrotation = (agent.transform.rotation.eulerAngles.y % 360) + 90 + (agent.transform.rotation.eulerAngles.x%90);
                            if (yrotation > 180)
                                yrotation -= 360;
                            zrotation = -90;
                            if (zrotation > 180)
                                zrotation -= 360;
                            while (currentTime >= 0)
                            {
                                agent.transform.Rotate(-1*RotateSpeed * Time.deltaTime, 0, 0,
                                    Space.Self);
                                agent.transform.Translate(0, 0, translateSpeed * Time.deltaTime, agent.transform);
                                currentTime -= Time.deltaTime;
                                yield return new WaitForFixedUpdate();
                            }
                            break;  
                        
                        case TransformPosition.Floor:
                            xrotation = (180 - agent.transform.rotation.eulerAngles.x) % 360;
                            if (xrotation > 180)
                                xrotation -= 360;
                            currentTime = Mathf.Abs(xrotation / RotateSpeed);
                            translateSpeed = TranslateAmount / currentTime;
                            yrotation = (agent.transform.rotation.eulerAngles.y % 360) + 90 + (agent.transform.rotation.eulerAngles.x%90);
                            if (yrotation > 180)
                                yrotation -= 360;
                            zrotation = -90;
                            if (zrotation > 180)
                                zrotation -= 360;
                            while (currentTime >= 0)
                            {
                                agent.transform.Rotate(-1*RotateSpeed * Time.deltaTime, 0, 0,
                                    Space.Self);
                                agent.transform.Translate(0, 0, translateSpeed * Time.deltaTime, agent.transform);
                                currentTime -= Time.deltaTime;
                                yield return new WaitForFixedUpdate();
                            }
                            break;  
                    }
                    currentPos = newPos;
                    break;
                
                case TransformPosition.Cieling:
                    newPos = CheckTransformDirection();
                    currentTime = 90 / RotateSpeed;
                    translateSpeed = TranslateAmount / currentTime;
                    yrotation = agent.transform.rotation.eulerAngles.y % 360;
                    if (yrotation > 180)
                        yrotation -= 360;
                    rotateySpeed = yrotation / currentTime;
                    while (currentTime >= 0)
                    {
                        agent.transform.Rotate(-RotateSpeed * Time.deltaTime, 0, 0,
                            Space.Self);
                        agent.transform.Translate(0, 0, translateSpeed * Time.deltaTime, agent.transform);
                        currentTime -= Time.deltaTime;
                        yield return new WaitForFixedUpdate();
                    }
                    agent.transform.Rotate(0, 0, -yrotation, Space.Self);
                    currentPos = newPos;
                    break;
            }
            
            agent.updateRotation = true;
            agent.enabled = true;
            SetAgentDestination();
            climbing = false;
        }
    }

    private TransformPosition CheckTransformDirection()
    {
        switch (currentPos)
        {
            case TransformPosition.Floor:
                if (Mathf.Abs(agent.transform.forward.z) > Mathf.Abs(agent.transform.forward.x))
                    return TransformPosition.WallZ;
                else
                    return TransformPosition.WallX;
            case TransformPosition.WallX:
                if (Mathf.Abs(agent.transform.forward.z) > Mathf.Abs(agent.transform.forward.y))
                    return TransformPosition.WallZ;
                else
                {
                    if (agent.transform.forward.y > 0)
                        return TransformPosition.Cieling;

                    return TransformPosition.Floor;
                }
            case TransformPosition.WallZ:
                if (Mathf.Abs(agent.transform.forward.x) > Mathf.Abs(agent.transform.forward.y))
                    return TransformPosition.WallX;
                else
                {
                    if (agent.transform.forward.y > 0)
                        return TransformPosition.Cieling;

                    return TransformPosition.Floor;
                }
            case TransformPosition.Cieling:
                if (Mathf.Abs(agent.transform.forward.z) > Mathf.Abs(agent.transform.forward.x))
                    return TransformPosition.WallZ;
                else
                    return TransformPosition.WallX;
        }

        return TransformPosition.Floor;
    }
}
