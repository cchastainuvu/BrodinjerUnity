using UnityEngine;

public class RandomSoundController : MonoBehaviour
{
    private AudioSource _mSource;
    private int _clipLength;
    public AudioClip[] _mClips;
    

    void Start()
    {
        _mSource = GetComponent<AudioSource>();
        _clipLength = _mClips.Length;
    }
    
    public void Play()
    {
        if (_mSource.isPlaying)
        {
            _mSource.Stop();
        }
        _mSource.clip = _mClips[Random.Range(0, _clipLength - 1)];
        _mSource.Play();
    }

    public void Stop()
    {
        if (_mSource.isPlaying)
        {
            _mSource.Stop();
        }
    }
}
