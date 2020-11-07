using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RibCage_Wall_Movement : MonoBehaviour
{
    public float moveSpeed = 6;
    public float turnSpeed = 90;
    public float lerpSpeed = 10;
    public float gravity = 10;
    public float deltaGround = .2f;
    public float jumpRange = 10;
    public float offset = 10f;

    public Transform destination;

    private Rigidbody rigidbody;
    private BoxCollider collider;
    private Vector3 surfaceNormal;
    private Vector3 myNormal;
    private float distGround;
    private bool rotating = false;
    private float vertSpeed = 0;
    
    private Ray ray;
    private RaycastHit hit;
 
 
 private void Start()
 {
     rigidbody = GetComponent<Rigidbody>();
     collider = GetComponent<BoxCollider>();
     myNormal = transform.up;
     rigidbody.freezeRotation = true;
     distGround = collider.bounds.extents.y - collider.center.y;  
 }
 
 private void FixedUpdate(){
     rigidbody.AddForce(-gravity*rigidbody.mass*myNormal);
 }
 
 private void Update(){
     
     if (rotating) return;
         
     if (Physics.Raycast(transform.position, transform.forward, out hit, jumpRange)){
         StartCoroutine(RotateToWall(hit.point, hit.normal));
     }          
     
     if (Physics.Raycast(transform.position, -myNormal, out hit)){
         surfaceNormal = hit.normal;
     }
     else {
         surfaceNormal = Vector3.up; 
     }
     
     Vector3 targetRotation = destination.transform.position;
     targetRotation = (targetRotation - transform.position).normalized;
     Quaternion facingDirection = Quaternion.LookRotation(targetRotation);
     Quaternion YRotation = Quaternion.Euler(transform.rotation.eulerAngles.x,
         facingDirection.eulerAngles.y, transform.rotation.eulerAngles.z);
     if (!CheckDestination(transform.rotation.eulerAngles,
         YRotation.eulerAngles, offset))
     {
         Vector3 offsetToTarget = destination.position - transform.position;
         Vector3 up = transform.up;
         Quaternion desiredOrientation = TurretLookRotation(offsetToTarget, up);
         transform.rotation = Quaternion.RotateTowards(
             transform.rotation,
             desiredOrientation,
             turnSpeed * Time.deltaTime
         );
     }


     Vector3 targetDestination = transform.position + (transform.forward * moveSpeed * Time.deltaTime);
     
     transform.position =
         Vector3.MoveTowards(transform.position, targetDestination, moveSpeed * Time.deltaTime);
 }
    
    Quaternion TurretLookRotation(Vector3 approximateForward, Vector3 exactUp)
    {
        Quaternion rotateZToUp = Quaternion.LookRotation(exactUp, -approximateForward);
        Quaternion rotateYToZ = Quaternion.Euler(90f, 0f, 0f);

        return rotateZToUp * rotateYToZ;
    }
    
    

    private IEnumerator RotateToWall(Vector3 point, Vector3 normal)
    {
        rotating = true;
        rigidbody.isKinematic = true;
        Vector3 orgPos = transform.position;
        Quaternion orgRot = transform.rotation;
        Vector3 dstPos = point + (normal * (distGround + 0.5f));
        Vector3 myForward = Vector3.Cross(transform.right, normal);
        Quaternion dstRot = Quaternion.LookRotation(myForward, normal);
        float t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime;
            transform.position = Vector3.Lerp(orgPos, dstPos, t);
            transform.rotation = Quaternion.Slerp(orgRot, dstRot, t);
            yield return new WaitForFixedUpdate();
        }
        myNormal = normal;
        rigidbody.isKinematic = false;
        rotating = false;
    }
    
    public bool CheckDestination(Vector3 Dest01, Vector3 Dest02, float offset)
    {
        if ((Dest01.x >= Dest02.x - offset
             && Dest01.x <= Dest02.x + offset)
            &&(Dest01.y >= Dest02.y - offset
               && Dest01.y <= Dest02.y + offset)
            &&(Dest01.z >= Dest02.z - offset
               && Dest01.z <= Dest02.z + offset))
        {
            return true;
        }
        return false;
    }
}
