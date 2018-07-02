using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSkinChanger : MonoBehaviour {
    // Keep uysing increments of 1

    [SerializeField]
    private List<GameObject> _ballSkins;

    private void Start()
    {
        foreach (GameObject Skinners in _ballSkins)
        {
            Skinners.SetActive(false);
        }

        _ballSkins[PlayerPrefs.GetInt("Equipped")].SetActive(true);
    }
}
