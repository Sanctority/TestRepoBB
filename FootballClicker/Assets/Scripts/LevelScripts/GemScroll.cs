using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemScroll : PickupBase {

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        transform.position += (Vector3.left * Time.deltaTime) * _speed;     //Basic movement that should be consistant
    }

    public override void Activate()
    {
        FindObjectOfType<BallScript>().IncrementGems();
    }
}
