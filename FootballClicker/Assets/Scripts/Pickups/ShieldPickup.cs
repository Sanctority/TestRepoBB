using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPickup : PickupBase {
    CircleCollider2D _circleCollider2D;
	// Use this for initialization
	public override void Start () {
        base.Start();
        //transform.localScale = _player.transform.localScale;
        _circleCollider2D = GetComponent<CircleCollider2D>();
        //_circleCollider2D.radius = _player.GetComponent<CircleCollider2D>().radius + _player.GetComponent<CircleCollider2D>().radius*0.01f;
	}
	
	// Update is called once per frame
	public override void FixedUpdate () {

        if (!_collected)
        {
            base.FixedUpdate();
        }
        else
        {
            transform.position = _player.transform.position;
        }
	}

    public override void Activate()
    {
        _player.GetComponent<BallScript>()._protected = true;
        _collected = true;
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Enemy" && _collected)
        {
            Debug.Log("Shield hit an enemy: "+collider.name);
            collider.enabled = false;
            _player.GetComponent<BallScript>()._protected = false;
            Destroy(gameObject);
        }
        
    }

}
