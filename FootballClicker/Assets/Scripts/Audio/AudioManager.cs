using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    // Public variables
    public static AudioManager _instance = null;

    public List<AudioClip> _audioClips;

    // Private variables
    private AudioSource _audioSource;
    private int _audioClipsCounter;

    //Singleton
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        _audioClipsCounter = 0;
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = 0.4f;
    }
    private void Update()
    {
        if (!_audioSource.isPlaying)
        {
            PlayNextTrack();
        }

    }

    public void ChangeVolume(float _vol)
    {
        _audioSource.volume = _vol;
    }

    public void PlayNextTrack()
    {
        _audioSource.clip = _audioClips[_audioClipsCounter];
        _audioSource.PlayDelayed(0.25f);

        if (_audioClipsCounter+1 >=  _audioClips.Count)
        {
            _audioClipsCounter = 0;
        }
        else
        {
            _audioClipsCounter++;
        }
    }
}
