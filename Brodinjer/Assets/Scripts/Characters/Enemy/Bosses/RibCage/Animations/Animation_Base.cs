using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Animation_Base : ScriptableObject
{
    protected Animator anim;
    protected Transform player;
    protected MonoBehaviour caller;
    protected NavMeshAgent agent;

    public void Init(MonoBehaviour caller, Animator anim, Transform player, NavMeshAgent agent)
    {
        this.anim = anim;
        this.player = player;
        this.caller = caller;
        this.agent = agent;
    }
    
    public abstract void StartAnimation();
    public abstract void StopAnimation();
    
    protected float ConvertRange(float origMinRange, float origMaxRange, float newMinRange, float newMaxRange, float value)
    {
        return (value - origMinRange) * (newMaxRange - newMinRange) / (origMaxRange - origMinRange) + newMinRange;
    }
}
