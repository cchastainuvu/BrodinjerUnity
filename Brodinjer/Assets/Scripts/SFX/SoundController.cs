using UnityEngine;

public class SoundController : MonoBehaviour
{
    private AudioSource _mSource;

    void Start()
    {
        _mSource = GetComponent<AudioSource>();
    }
    
    public void Play()
    {
        if (_mSource.isPlaying)
        {
            _mSource.Stop();
        }
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
