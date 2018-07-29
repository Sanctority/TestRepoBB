using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPickup : PickupBase {

    CircleCollider2D _circleCollider2D;
	// Use this for initialization
	public override void Start () {
        base.Start();
        _circleCollider2D = GetComponent<CircleCollider2D>();
        _circleCollider2D.radius = _player.GetComponent<CircleCollider2D>().radius + _player.GetComponent<CircleCollider2D>().radius*0.01f;
	}
	
	// Update is called once per frame
	public override void FixedUpdate () {
        base.Start();
	}

    public override void Activate()
    {
        _player.GetComponent<BallScript>()._protected = true;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.collider.enabled = false;
            _player.GetComponent<BallScript>()._protected = false;
            DestroyImmediate(this);
        }
    }

}
