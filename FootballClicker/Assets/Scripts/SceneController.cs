using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{

    public static SceneController _instance;
    private void Awake()
    {
        // Instance of scene controller start.
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
        // Instance of scene controller end.
    }

    public void SwapScene(int sceneIndex)
    {
        SceneManager.LoadSceneAsync(sceneIndex);
    }

}

