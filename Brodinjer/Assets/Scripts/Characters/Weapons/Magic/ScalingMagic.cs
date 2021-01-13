using System.Collections;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class ScalingMagic : MonoBehaviour
{
    //Add Local scale point
    public PlayerMovement movement;
    public WeaponManager wm;
    private Transform ScalingObj;
    public float ScaleTime;
    private float timeLeft;
    private WaitForFixedUpdate _fixedUpdate;
    [HideInInspector]
    public ScalableObject scaleObj;
    private Vector3 newScale, scaleIncrease;
    public float IncreaseAmount;
    public string stopButton;
    [HideInInspector] public bool hitObj = false;
    public GameObject art;
    public LimitFloatData MagicAmount;
    public BoolData MagicInUse;
    public float decreaseSpeed;
    public ScalingScript scalescript;
    public GameObject VFX;
    public string ScaleAxis;
    public GameObject MagicCollider, VFXCollider;

    private void Start()
    {
        hitObj = false;
        _fixedUpdate = new WaitForFixedUpdate();
        scaleIncrease = new Vector3(IncreaseAmount, IncreaseAmount, IncreaseAmount);


    }
    
    public void Fire()
    {
        MagicCollider.SetActive(true);
        VFXCollider.SetActive(true); 
        GetComponentInParent<Look_At_Script>().StopLookAt();
        Debug.Log("Fire");
    }

    private IEnumerator OnTriggerEnter(Collider other)
    {
        if (!hitObj)
        {
            if (other.CompareTag("Scalable"))
            {
                scalescript.SpellHit(true);
                Debug.Log("Hit obj");
                hitObj = true;
                ScalingObj = other.gameObject.transform;
                scaleObj = ScalingObj.GetComponent<ScalableObject>();
                art.SetActive(false);
                StartCoroutine(Scale());
            }
            else
            {
                yield return new WaitForSeconds(.05f);
                if (!hitObj)
                {
                    scalescript.SpellHit(false);
                    MagicInUse.value = false;
                    Destroy(this);
                }
            }


        }

    }

    private IEnumerator Scale()
    {
        //movement.StopAll();
        timeLeft = ScaleTime;
        scaleObj.highlightFX.Highlight();
        scalescript.inUse = true;
        while (MagicAmount.value > 0 /*&& timeLeft > 0*/ && scalescript.currWeapon)
        {
            newScale = ScalingObj.localScale;
            if (Input.GetAxis(ScaleAxis) > 0)
            {
                MagicAmount.SubFloat(decreaseSpeed*Time.deltaTime);
                scaleObj.ScaleUp(true);
            }
            else if (Input.GetAxis(ScaleAxis) < 0)
            {
                MagicAmount.SubFloat(decreaseSpeed*Time.deltaTime);
                scaleObj.ScaleDown(true);
            }
            if (Input.GetButtonDown(stopButton))
            {
                Destroy(this.gameObject);

            }
            timeLeft -= Time.deltaTime;
            yield return _fixedUpdate;
        }
        Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        MagicInUse.value = false;
        if(scaleObj!= null)
            scaleObj.highlightFX.UnHighlight();
    }
}
