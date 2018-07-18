using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MuteDriver : MonoBehaviour
{
    [SerializeField]
    private GameObject _volOff;
    [SerializeField]
    private GameObject _volOn;

    private void Start()
    {
            SetupMuteButton();
    }

    public void Mute(bool _mute)
    {
        FindObjectOfType<AudioManager>().Mute(_mute);
    }

    private void SetupMuteButton()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            _volOff.SetActive(false);
            _volOn.SetActive(false);
            return;
        }

        if (FindObjectOfType<AudioManager>().GetMute())
        {
            _volOff.SetActive(true);
            _volOn.SetActive(false);
        }
        else
        {
            _volOff.SetActive(false);
            _volOn.SetActive(true);
        }
    }
}
