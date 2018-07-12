using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScroll : MonoBehaviour
{

    public float _minJumpTime;      // Minimum time before an enemy can jump
    public float _maxJumpTime;      // Maximum time for an enemy to jump
    public Vector2 _enemyImpulseUp; // Force applied to the jumping enemy

    [SerializeField]
    public static float _speed;           // Basic movement speed // static so it changes in all instances of the class
    private bool _canJump;

    private Rigidbody2D _enemyRB;

    void Start()
    {
        _canJump = Random.Range(0, 2) == 0;                                 //50 50 chance to jump
        _enemyRB = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        transform.position += (Vector3.left * Time.deltaTime) * _speed;     //Basic movement that should be consistant
    }

    public void Jump()
    {
        if (_canJump)
        {
            _enemyRB.AddForce(_enemyImpulseUp, ForceMode2D.Impulse);
            _canJump = false;
        }
    }                                                                       // Invokes the Jump function after it has finished jumping to execute with a delay of random time
}
