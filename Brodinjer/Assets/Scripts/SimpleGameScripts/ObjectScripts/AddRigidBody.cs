using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRigidBody : MonoBehaviour
{
    public float Mass;
    public List<GameObject> Objs;
    
    public void AddRigidbodies()
    {
        foreach (var obj in Objs)
        {
            Rigidbody rigid = obj.AddComponent<Rigidbody>();
            rigid.mass = Mass;
            rigid.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;

        }   
    }
}
