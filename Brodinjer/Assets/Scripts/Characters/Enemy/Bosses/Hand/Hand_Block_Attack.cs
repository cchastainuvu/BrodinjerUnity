using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Hand_Block_Attack : MonoBehaviour
{
    public GameObject blockCollider;
    public float blockTime;
    public Animator anim;

    private void Start()
    {
        blockCollider.SetActive(false);
    }

    public void StartBlock()
    {
        StartCoroutine(Block());
    }

    private IEnumerator Block()
    {
        anim.SetTrigger("Block");
        blockCollider.SetActive(true);
        yield return new WaitForSeconds(blockTime);
        blockCollider.SetActive(false);
    }
    
}
