using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingScript : MonoBehaviour {

    public float _scrollSpeed;


    private void Update()
    {
        Vector2 _change = new Vector2(Time.time * _scrollSpeed, 0);

        GetComponent<Renderer>().material.mainTextureOffset = _change;
    }
}
