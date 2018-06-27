using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDestroyer : MonoBehaviour {
    private SpawnController _spawnController;

    private void Start()
    {
        _spawnController = GameObject.FindObjectOfType<SpawnController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _spawnController.RemoveEnemyFromlist(collision.gameObject);
    }
}
