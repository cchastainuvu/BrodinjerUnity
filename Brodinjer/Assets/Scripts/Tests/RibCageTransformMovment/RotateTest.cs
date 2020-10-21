using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RotateTest : MonoBehaviour
{
    
    private NavMeshAgent agent;
    public Transform Destination;
    private RaycastHit hit;
    public float RotateSpeed, TranslateAmount;
    private float currentTime, translateSpeed, rotateySpeed, yrotation;
    private Vector3 newRotation, newTranslation;
    private bool climbing;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.autoTraverseOffMeshLink = false;
        StartCoroutine(Call());
    }

    private IEnumerator Call()
    {
        while (true)
        {
            agent.enabled = false;
            agent.updateRotation = false;
            currentTime = 90 / RotateSpeed;
            translateSpeed = TranslateAmount / currentTime;
            yrotation = agent.transform.localRotation.eulerAngles.y % 360;
            if (yrotation > 180)
            {
                yrotation -= 360;
            }

            rotateySpeed = yrotation / currentTime;
            while (currentTime >= 0)
            {
                agent.transform.Rotate(new Vector3(-RotateSpeed*Time.deltaTime, 0, rotateySpeed * Time.deltaTime), Space.Self);
                agent.transform.Translate(new Vector3(0, 0, translateSpeed * Time.deltaTime), agent.transform);
                currentTime -= Time.deltaTime;
                yield return new WaitForFixedUpdate();
            }

            agent.updateRotation = true;
            agent.enabled = true;
            agent.SetDestination(Destination.position);
        }

    }
}
