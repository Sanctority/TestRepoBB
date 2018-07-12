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
        if (collision.tag == "Enemy")
        {
            _spawnController.RemoveEnemyFromlist(collision.gameObject);
        }
        else if(collision.tag == "Gem")
        {
            _spawnController.RemoveGem(collision.gameObject);
        }
    }
}
