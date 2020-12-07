using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GearRotation : MonoBehaviour
{
    public float InitRotationSpeed;
    public bool Clockwise;
    public bool Rotating;
    public Vector3 RotationAxis;
    public bool InitGear;
    private GearRotation tempConnection, rotatingConnection;
    public List<GearRotation> connectedGears;
    private bool clogged;
    private List<GearRotation> previousConnections;
    public bool FinalGear;
    public bool FinalGearClockwise;
    public UnityEvent GearCompleted;
    private bool completed;

   // public float ScaleSpeed;

    private void Awake()
    {
        if (InitGear)
        {
            Rotating = true;
        }

        clogged = false;
        previousConnections = connectedGears;
        completed = false;
    }

    private void FixedUpdate()
    {
        if(!InitGear)
            CheckRotation();
        if (Rotating && !clogged)
        {
            if (Clockwise)
                transform.Rotate(RotationAxis* (InitRotationSpeed)*Time.deltaTime);
            else
                transform.Rotate(-RotationAxis* (InitRotationSpeed)*Time.deltaTime);
        }

        if (FinalGear)
        {
            if (Rotating && !clogged && !completed)
            {
                if ((Clockwise == FinalGearClockwise))
                {
                    completed = true;
                    GearCompleted.Invoke();
                }
            }
        }
    }

    private bool CheckRotation()
    {
        if (connectedGears.Count <= 2)
        {
            List<GearRotation> rotatingGears = new List<GearRotation>();
            foreach (var gear in connectedGears)
            {
                if (gear.Rotating)
                {
                    rotatingGears.Add(gear);
                }
            }

            if (rotatingGears.Count <= 0)
            {
                Rotating = false;
                return false;
            }
            if (rotatingGears.Count == 1)
            {
                if (CheckInit(new List<GearRotation>() {this}, InitGear))
                {
                    rotatingConnection = rotatingGears[0];
                    Rotating = true;
                    Clockwise = !rotatingConnection.Clockwise;
                    return true;
                }
                else
                {
                    Rotating = false;
                    return false;
                }
            }
            if (rotatingGears.Count == 2)
            {
                if (rotatingGears[0].Clockwise == rotatingGears[1].Clockwise)
                {
                    if (CheckInit(new List<GearRotation>() {this}, InitGear))
                    {
                        rotatingConnection = rotatingGears[0];
                        Rotating = true;
                        Clockwise = !rotatingConnection.Clockwise;
                        return true;
                    }
                    else
                    {
                        Rotating = false;
                        return false;
                    }
                }
                else
                {
                    Clog(new List<GearRotation>() {this});
                    Rotating = false;
                    return false;
                }
            }

            return false;
        }
        else
        {
            Clog(new List<GearRotation>() {this});
            Rotating = false;
            return false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!InitGear)
        {
            if (other.CompareTag("Gear"))
            {
                tempConnection = other.GetComponentInParent<GearRotation>();
                if (tempConnection != null)
                {
                    if(!connectedGears.Contains(tempConnection)&& tempConnection != this)
                        connectedGears.Add(tempConnection);
                }
                else
                {
                    Debug.Log("Not A Gear");
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!InitGear)
        {
            if (other.CompareTag("Gear"))
            {
                tempConnection = other.GetComponentInParent<GearRotation>();
                if (tempConnection != null)
                {
                    previousConnections = connectedGears;
                    UnClog(new List<GearRotation>(){this});
                    connectedGears.Remove(tempConnection);
                }
                else
                {
                    Debug.Log("Not A Gear");
                }
            }
        }
    }

    public void Clog(List<GearRotation> changedList)
    {
        clogged = true;
        foreach (var gear in connectedGears)
        {
            if (!changedList.Contains(gear))
            {
                changedList.Add(gear);
                gear.Clog(changedList);
            }
        }

    }

    public void UnClog(List<GearRotation> changedList)
    {
        clogged = false;
        foreach (var gear in previousConnections)
        {
            if (!changedList.Contains(gear))
            {
                changedList.Add(gear);
                gear.UnClog(changedList);
            }
        }
        Debug.Log("Unclog: " + gameObject.name);
    }

    public bool CheckInit(List<GearRotation> checkedList, bool isInit)
    {
        foreach (var gear in connectedGears)
        {
            if (!checkedList.Contains(gear))
            {
                checkedList.Add(gear);
                isInit = gear.CheckInit(checkedList, isInit);
            }
        }
        return (isInit || InitGear);
    }

    public void Rotation(List<GearRotation> checkedList)
    {
        CheckRotation();
        foreach (var gear in connectedGears)
        {
            if (!checkedList.Contains(gear))
            {
                checkedList.Add(gear);
                gear.CheckRotation();
            }
        }
    }


}
