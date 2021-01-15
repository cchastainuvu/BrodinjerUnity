using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
        public CharacterRotate rotate;
        public CharacterTranslate translate;
        public List<CharacterControlExtraBase> extraControls = new List<CharacterControlExtraBase>();
        public Targeting targetScript;
        private Coroutine rotateFunc, moveFunc, runFunc;
        private CharacterController _cc;
        public Transform CharacterScalar;
        public Animator anim;
        public bool MoveOnStart;
        private bool moving, rotating, extrarunning, active, stunned;
        public Transform DirectionReference;
        public BoolData paused;
        private bool pauseInits;
        private ResetTriggers triggerReset;
        public string IdleTrigger = "Idle";
        public CharacterTranslate deadTranslate;
        public CharacterRotate deadRotate;
        private bool dead;

        private void Awake()
        {
                _cc = GetComponent<CharacterController>();
                triggerReset = anim.GetComponent<ResetTriggers>();
        }

        private void Start()
        {
                dead = false;
                active = true;
                moving = false;
                rotating = false;
                extrarunning = false;
                pauseInits = false;
                Init();
        }

        public void Die()
        {
                Deactivate();
                translate = deadTranslate;
                translate.Init(this, _cc, DirectionReference, targetScript, anim);
                rotate = deadRotate;
                rotate.Init(transform, DirectionReference, targetScript);
                translate.canMove = true;
                translate.canRun = true;
                if (!moving)
                {
                        moving = true;
                        moveFunc = StartCoroutine(translate.Move());
                        runFunc = StartCoroutine(translate.Run());
                }
                rotate.canRotate = true;
                if (!rotating)
                {
                        rotating = true;
                        rotateFunc = StartCoroutine(rotate.Rotate());
                }
                Debug.Log("Dead");
        }

        private void FixedUpdate()
        {
                if (paused.value && !pauseInits)
                {
                        pauseInits = true;
                        StopAll();
                }
                else if (!paused.value && pauseInits)
                {
                        pauseInits = false;
                        StartAll();
                }
        }

        public void Stun()
        {
                stunned = true;
                StopAll();
        }

        public void UnStun()
        {
                stunned = false;
                StartAll();
        }

        private void Init()
        {
                rotate.Init(transform, DirectionReference, targetScript);
                translate.Init(this, _cc, DirectionReference, targetScript, anim);
                if (extraControls != null)
                {
                        foreach (var extra in extraControls)
                        {
                                extra.Init(CharacterScalar, _cc);
                        }
                }

                if (MoveOnStart)
                {
                        StartAll();
                }
        }

        public void Deactivate()
        {
                active = false;
                StopAll();
        }

        public void EnterConv()
        {
                if(triggerReset)
                        triggerReset.ResetAllTriggers();
                anim.SetTrigger(IdleTrigger);
                Deactivate();
        }

        public void ExitConv()
        {
                Activate();
                StartAll();
        }

        public void Activate()
        {
                active = true;
        }

        public void SwapMovement(CharacterRotate newRot, CharacterTranslate newTrans, List<CharacterControlExtraBase> extras = null)
        {
                if (!dead)
                {
                        StopAll();
                        rotate = newRot;
                        translate = newTrans;
                        extraControls = extras;
                        Init();
                        StartAll();
                }
        }

        public void StopAll()
        {
                StopMove();
                StopRotate();
                StopExtras();
        }

        public void StartAll()
        {
                if (active && !stunned)
                {
                        StartMove();
                        StartRotate();
                        StartExtras();
                }
        }

        public void StopMove()
        {
                translate.canMove = false;
                translate.canRun = false;
                if(moveFunc != null)
                        StopCoroutine(moveFunc);
                if(runFunc!= null)
                        StopCoroutine(runFunc);
                moving = false;
        }

        public void StartMove()
        {
                if (active && !stunned)
                {
                        translate.canMove = true;
                        translate.canRun = true;
                        if (!moving)
                        {
                                moving = true;
                                moveFunc = StartCoroutine(translate.Move());
                                runFunc = StartCoroutine(translate.Run());
                        }
                }
        }

        public void StopRotate()
        {
                rotate.canRotate = false;
                if(rotateFunc != null)
                        StopCoroutine(rotateFunc);
                rotating = false;
        }

        public void StartRotate()
        {
                if (active && !stunned)
                {
                        rotate.canRotate = true;
                        if (!rotating)
                        {
                                rotating = true;
                                rotateFunc = StartCoroutine(rotate.Rotate());
                        }
                }
        }

        public void StartExtras()
        {
                if (active)
                {
                        if (extraControls != null)
                        {
                                foreach (CharacterControlExtraBase extra in extraControls)
                                {
                                        extra.canMove = true;
                                        if (!extrarunning)
                                        {
                                                extrarunning = true;
                                                StartCoroutine(extra.Move());
                                        }
                                }
                        }
                }
        }

        public void StopExtras()
        {
                if (extraControls != null)
                {
                        foreach (CharacterControlExtraBase extra in extraControls)
                        {
                                extra.canMove = false;
                        }
                }

                extrarunning = false;
        }

        public void SetTranslate(CharacterTranslate translate)
        {
                StopMove();
                this.translate = translate;
                if (_cc == null)
                        _cc = GetComponent<CharacterController>();
                this.translate.Init(this, _cc, DirectionReference, targetScript, anim);
                StartMove();
                
        }

        public void SetRotation(CharacterRotate rotate)
        {
                StopRotate();
                this.rotate = rotate;
                if (_cc == null)
                        _cc = GetComponent<CharacterController>();
                this.rotate.Init(transform, DirectionReference, targetScript);
                StartRotate();
        }
}
