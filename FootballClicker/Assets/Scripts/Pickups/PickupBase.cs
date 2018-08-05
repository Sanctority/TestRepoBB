using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PickupBase : MonoBehaviour
{
    [SerializeField]
    protected float _speed = 4f;
    protected GameObject _player;
    protected bool _collected;
    public virtual void Start()
    {
        _player = FindObjectOfType<BallScript>().gameObject;
        _collected = false;
    }

    public virtual void FixedUpdate()
    {
        if (!_collected)
        {
            transform.position += (Vector3.left * Time.deltaTime) * _speed;
        }
    }

    public abstract void Activate();

}
