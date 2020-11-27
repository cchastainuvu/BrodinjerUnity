using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiate_Obj : MonoBehaviour
{
    public GameObject ObjectToInstantiate;

    public virtual void InstantiateObj()
    {
        Instantiate(ObjectToInstantiate).SetActive(true);
    }

    public void InstantiateObj(Transform objPos)
    {
        Instantiate(ObjectToInstantiate, objPos.position, objPos.rotation).SetActive(true);
    }
    
    public void InstantiateObj(Vector3 objPos)
    {
        Instantiate(ObjectToInstantiate, objPos, ObjectToInstantiate.transform.rotation).SetActive(true);
    }

    public void InstantiateObj(Vector3 objPos, Quaternion rotation)
    {
        Instantiate(ObjectToInstantiate, objPos, rotation).SetActive(true);
    }
    
}
