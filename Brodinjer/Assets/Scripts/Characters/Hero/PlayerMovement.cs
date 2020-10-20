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
        private bool moving, rotating, extrarunning;
        

        private void Start()
        {
                moving = false;
                rotating = false;
                extrarunning = false;
                _cc = GetComponent<CharacterController>();
                Init();
        }

        private void Init()
        {
                rotate.Init(transform, Camera.main.transform, targetScript);
                translate.Init(this, _cc, Camera.main.transform, targetScript, anim);
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

        public void SwapMovement(CharacterRotate newRot, CharacterTranslate newTrans, List<CharacterControlExtraBase> extras = null)
        {
                StopAll();
                rotate = newRot;
                translate = newTrans;
                extraControls = extras;
                Init();
        }

        public void StopAll()
        {
                StopMove();
                StopRotate();
                StopExtras();
        }

        public void StartAll()
        {
                StartMove();
                StartRotate();
                StartExtras();
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
                translate.canMove = true;
                translate.canRun = true;
                if (!moving)
                {
                        moving = true;
                        moveFunc = StartCoroutine(translate.Move());
                        runFunc = StartCoroutine(translate.Run());
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
                rotate.canRotate = true;
                if (!rotating)
                {
                        rotating = true;
                        rotateFunc = StartCoroutine(rotate.Rotate());
                }
        }

        public void StartExtras()
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
}
