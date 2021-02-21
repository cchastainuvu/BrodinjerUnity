using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleSoundController : SoundController
{
    public SoundController sound1, sound2;
    public float InbetweenWait;

    public override void Play()
    {
        sound1.Play();
        StartCoroutine(Wait());
    }

    public override void Stop()
    {
        sound1.Stop();
        sound2.Stop();
        StopAllCoroutines();
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(InbetweenWait);
        sound2.Play();
    }
}
