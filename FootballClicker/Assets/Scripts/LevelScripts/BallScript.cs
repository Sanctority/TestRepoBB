using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour {

    // Public variables.
    public Vector2 _ballImpulseUp;
    public Vector2 _ballImpulseDown;

    // Private variables.
    private Rigidbody2D _ballRB;

    private void Start()
    {
        _ballRB = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _ballRB.AddForce(_ballImpulseUp, ForceMode2D.Impulse);
            Debug.Log("Ball has been kicked");
        }

        // This is used to get touch input for the left and right side of the device.
        for (int _touchNumber = 0; _touchNumber < Input.touchCount; _touchNumber++)
        {
            if (Input.GetTouch(_touchNumber).phase == TouchPhase.Began)
            {
                if (Input.GetTouch(_touchNumber).position.x < Screen.width / 2)
                {
                    _ballRB.AddForce(_ballImpulseUp, ForceMode2D.Impulse);
                    Debug.Log("Ball has been kicked up");
                }
                else if (Input.GetTouch(_touchNumber).position.x > Screen.width / 2)
                {
                    _ballRB.AddForce(_ballImpulseDown, ForceMode2D.Impulse);
                    Debug.Log("Ball has been kicked down");
                }
            }
        }
        
    }
}
