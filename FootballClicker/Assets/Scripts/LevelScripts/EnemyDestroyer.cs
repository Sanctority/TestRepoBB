using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDestroyer : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SpawnController._instance.RemoveEnemyFromlist(collision.gameObject);
    }
}
