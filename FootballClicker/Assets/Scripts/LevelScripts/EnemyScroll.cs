using MainLevel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScroll : MonoBehaviour
{

    static UiScript _uiScript;

    public float _minJumpTime;      // Minimum time before an enemy can jump
    public float _maxJumpTime;      // Maximum time for an enemy to jump
    public float _enemyLowestImpuls; // Force applied to the jumping enemy
    public float _enemyHighestImpulse;
    public float _jumpChanceOutOf100;

    [SerializeField]
    public float _speed;           // Basic movement speed
    private bool _canJump;

    private Rigidbody2D _enemyRB;

    void Start()
    {
        _uiScript = FindObjectOfType<UiScript>();
        if (Random.Range(0, 101) >= _jumpChanceOutOf100)
        {
            _canJump = true;
        }
        _enemyRB = GetComponent<Rigidbody2D>();
        _speed += _uiScript.ReturnScoreFloat() / 110f; //was 125            //see UiScript.ReturnScoreFloat
    }

    void FixedUpdate()
    {
        transform.position += (Vector3.left * Time.deltaTime) * _speed;     //Basic movement that should be consistant

    }

    public void Jump()
    {
        if (_canJump)
        {
            _enemyRB.AddForce(new Vector2(0,Random.Range(_enemyLowestImpuls,_enemyHighestImpulse)), ForceMode2D.Impulse);
            _canJump = false;
        }
    }                                                                       // Invokes the Jump function after it has finished jumping to execute with a delay of random time
}
