using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour {

    // Public variables.
    public Canvas _uiCanvas;
    public Vector2 _ballImpulseUp;
    public Vector2 _ballImpulseDown;
    public PhysicsMaterial2D _physicsHasBounce;     // This material will allow the ball to bounce.
    public PhysicsMaterial2D _physicsNoBounce;      // This material will stop the balle from being able to bounce.

    // Private variables.
    private Rigidbody2D _ballRB;

    private void Start()
    {
        // Instantiate all the needed variable, materials ect... for the ball.
        _ballRB = GetComponent<Rigidbody2D>();
        _ballRB.sharedMaterial = _physicsHasBounce;
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
                // This input check will kick the ball up and apply the bounce physics to the ball.
                if (Input.GetTouch(_touchNumber).position.x < Screen.width / 2)
                {
                    _ballRB.sharedMaterial = _physicsHasBounce;
                    _ballRB.AddForce(_ballImpulseUp, ForceMode2D.Impulse);
                    Debug.Log("Ball has been kicked up");
                }
                // This input check will kick the ball down and remove the bounc physics.
                else if (Input.GetTouch(_touchNumber).position.x > Screen.width / 2)
                {
                    _ballRB.sharedMaterial = _physicsNoBounce;
                    _ballRB.AddForce(_ballImpulseDown, ForceMode2D.Impulse);
                    Debug.Log("Ball has been kicked down");
                }
            }
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Footballer enemy collision starts.

        // Footballer enemy collision ends.
    }
}
