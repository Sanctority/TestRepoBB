using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MuteDriver : MonoBehaviour {
    [SerializeField]
    private GameObject _volOff;
    [SerializeField]
    private GameObject _volOn;

    private void Start()
    {
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
    public void Mute(bool _mute)
    {
        FindObjectOfType<AudioManager>().Mute(_mute);
    }
}
