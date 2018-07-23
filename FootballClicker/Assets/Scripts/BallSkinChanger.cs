using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSkinChanger : MonoBehaviour {

    [SerializeField]
    private GameObject[] _ballSkins;

    private string _selectedBall;

    private void Start()
    {

        _selectedBall = PlayerPrefs.GetString("Equipped");

        _ballSkins = GameObject.FindGameObjectsWithTag("Ball");

        foreach (GameObject Skinners in _ballSkins)
        {
            Skinners.SetActive(false);
        }

        Debug.Log("Ball "+PlayerPrefs.GetString("Equipped")+" equipped");

        foreach(GameObject MoreSkinners in _ballSkins)
        {
            if(MoreSkinners.GetComponent<BallScript>().ReturnBallID() == PlayerPrefs.GetString("Equipped"))
            {
                MoreSkinners.SetActive(true);
                Debug.Log("Ball activated: " + MoreSkinners.GetComponent<BallScript>().ReturnBallID());
                //break;
            }
        }
    }
}
