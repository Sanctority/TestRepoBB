using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController _instance;
    private static int _lastSceneIndex;

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
    }

    public void SwapScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void SetLastScene(int _lSI)
    {
        Debug.Log("Last scene was " + SceneManager.GetSceneByBuildIndex(_lSI).name);
        _lastSceneIndex = _lSI;
    }

    public void ReplayLastLevel()
    {
        Debug.Log("Loading last scene " + SceneManager.GetSceneByBuildIndex(_lastSceneIndex).name);
        try
        {
            SceneManager.LoadScene(_lastSceneIndex);
        }
        catch (System.Exception e)
        {
            Debug.LogError(e.Message);
            SceneManager.LoadScene(0);
        }
        
    }
    
}