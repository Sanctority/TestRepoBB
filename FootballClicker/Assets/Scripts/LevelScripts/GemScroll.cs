using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemScroll : MonoBehaviour {

    public float _speed = 4f;           // Basic movement speed
    void FixedUpdate()
    {
        transform.position += (Vector3.left * Time.deltaTime) * _speed;     //Basic movement that should be consistant

    }

    private void OnMouseDown()
    {
        Ray _raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
        RaycastHit _raycastHit;
        if (Physics.Raycast(_raycast, out _raycastHit))
        {
            if (_raycastHit.collider.name == "Gem")
            {
                Debug.LogError("IMPLEMENT GEMS");
            }
        }
    }
}
