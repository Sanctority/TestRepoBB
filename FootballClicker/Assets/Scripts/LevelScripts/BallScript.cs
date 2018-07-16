using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BallScript : MonoBehaviour
{

    // Public variables.
    public Canvas _uiCanvas;
    public Vector2 _ballImpulseUp;
    public Vector2 _ballImpulseDown;
    public PhysicsMaterial2D _physicsHasBounce;     // This material will allow the ball to bounce.
    public PhysicsMaterial2D _physicsNoBounce;      // This material will stop the balle from being able to bounce.
    public int _bounceLimitUp;
    public int _bounceLimitDown;
    public int _rotationSpeedOfBall;

    // Private variables.
    private Rigidbody2D _ballRB;
    private int _numOfBouncesUp;
    private int _numOfBouncesDown;
    private int _gemsEarned;
    private int _numOfBounces;

    

    private void Start()
    {
        // Instantiate all the needed variable, materials ect... for the ball.
        _ballRB = GetComponent<Rigidbody2D>();
        _ballRB.sharedMaterial = _physicsHasBounce;
        _numOfBouncesUp = 0;

        
    }

    private void Update()
    {

        BallRotation();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_numOfBouncesUp < _bounceLimitUp)
            {
                _ballRB.AddForce(_ballImpulseUp, ForceMode2D.Impulse);
                Debug.Log("Ball has been kicked");
                _numOfBouncesUp++;
                return;
            }
        }
        // This is used to get touch input for the left and right side of the device.
        for (int _touchNumber = 0; _touchNumber < Input.touchCount; _touchNumber++)
        {
            if (Input.GetTouch(_touchNumber).phase == TouchPhase.Began)
            {
                // This input check will kick the ball up and apply the bounce physics to the ball.
                if (Input.GetTouch(_touchNumber).position.x < Screen.width / 2)
                {
                    if(_numOfBouncesUp < _bounceLimitUp)
                    {
                        AchievementIncrementKicker();

                        _ballRB.sharedMaterial = _physicsHasBounce;
                        _ballRB.AddForce(_ballImpulseUp, ForceMode2D.Impulse);
                        Debug.Log("Ball has been kicked up");
                        _numOfBouncesUp++;
                    }
                    
                }
                // This input check will kick the ball down and remove the bounc physics.
                else if (Input.GetTouch(_touchNumber).position.x > Screen.width / 2)
                {
                    if(_numOfBouncesDown < _bounceLimitDown)
                    {
                        AchievementIncrementKicker();

                        _ballRB.sharedMaterial = _physicsNoBounce;
                        _ballRB.AddForce(_ballImpulseDown, ForceMode2D.Impulse);
                        Debug.Log("Ball has been kicked down");
                        _numOfBouncesDown++;
                    }
                    
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Footballer enemy collision starts.
        if (collision.gameObject.tag == "Enemy")
        {
            GameOver();
        }

        if(collision.gameObject.tag == "Ground")
        {
            _numOfBouncesUp = 0;
            _numOfBouncesDown = 0;
        }

        if(collision.gameObject.tag == "Gem")
        {
            Destroy(collision.gameObject);
            _gemsEarned++;
        }
    }

    private void GameOver()
    {
        _uiCanvas.gameObject.GetComponent<MainLevel.UiScript>().GameOver();
        PlayerPrefs.SetInt("Bounces", _numOfBounces);
        PlayerPrefs.SetInt("GemsEarned", _gemsEarned);
        SceneController._instance.SetLastScene(SceneManager.GetActiveScene().buildIndex);
        SceneController._instance.SwapScene(3); //Game over scene
        
    }

    // This will be used to handle the achievments for kicking the ball
    private void AchievementIncrementKicker()
    {
        _numOfBounces++;
    }

    private void BallRotation()
    {
        transform.Rotate(0, 0, Time.deltaTime * -_rotationSpeedOfBall);
    }
}
