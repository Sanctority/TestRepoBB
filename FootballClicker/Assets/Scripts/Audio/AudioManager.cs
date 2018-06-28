using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    // Public variables
    public AudioClip _mainMenuMusic;                // Main menu audio clip
    public AudioClip _gameMusic;                    // Main game audio clip
    public AudioClip _shopMusic;                    // Main shop audio clip

    // Private variables
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = 0.2f;                         // Set audio to a low amount, its still loud, might want to clamp it
        _audioSource.loop = true;                           // Set audio to loop
        _audioSource.clip = _mainMenuMusic;                 // Audio Source will load on main menu when game loads, which does not call the activeSceneChanged event, so assign it here
        DontDestroyOnLoad(gameObject);                      // Make sure the Audio Source stays alive through scene changes
        SceneManager.activeSceneChanged += SceneChanged;    // Subscribe to the activeSceneChanged event
    }

    private void SceneChanged(Scene current, Scene next)
    {
        if (next.buildIndex == 0)               // Check if next scene is Main menu
        {
            _audioSource.Stop();
            _audioSource.clip = _mainMenuMusic; // Change the clip
            _audioSource.Play();
        }
        else
        if (next.buildIndex == 1)               // Check if scene is Main game
        {
            _audioSource.Stop();
            _audioSource.clip = _gameMusic;     // Change the clip
            _audioSource.Play();
        }
        else
        if (next.buildIndex == 2)               // Check if scene is Shop
        {
            _audioSource.Stop();
            _audioSource.clip = _shopMusic;     // Change the clip
            _audioSource.Play();
        }
    }

    public void ChangeVolume(float _vol)
    {
        _audioSource.volume = _vol;             // Exposed function for setting volume which we can clamp
    }
}
