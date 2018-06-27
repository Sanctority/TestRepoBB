using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScroll : MonoBehaviour
{

    public float _minJumpTime;      // Minimum time before an enemy can jump
    public float _maxJumpTime;      // Maximum time for an enemy to jump
    public Vector2 _enemyImpulseUp; // Force applied to the jumping enemy

    [SerializeField]
    private float _speed;           // Basic movement speed

    private Rigidbody2D _enemyRB;

    void Start()
    {
        _enemyRB = GetComponent<Rigidbody2D>();
        Invoke("Jump", Random.Range(_minJumpTime, _maxJumpTime));           //Start the enemy jump function with a delay of random time
    }

    void FixedUpdate()
    {
        transform.position += (Vector3.left * Time.deltaTime) * _speed;     //Basic movement that should be consistant
    }

    private void Jump()
    {
        _enemyRB.AddForce(_enemyImpulseUp, ForceMode2D.Impulse);            
        Invoke("Jump", Random.Range(_minJumpTime, _maxJumpTime));           // Pretty sure this isnt recursion... will use a coroutine if it turns out to be a massive drain
    }                                                                       // Invokes the Jump function after it has finished jumping to execute with a delay of random time
}
