using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    // Public variables.
    public float _minSpawnTime;
    public float _maxSpawnTime;
    public float _maxEnemies;

    // Private variables.

    private GameObject _spawnObject;                 // This stores a reference to the spawn object.
    private Vector2 _spawnPosition;
    private Quaternion _spawnRotation;
    private List<GameObject> _spawnedEnemyList;     // This will hold a reference to any enemys that have been spawned into the level.

    // Serialized so we can add prefabs.
    [SerializeField]
    private List<GameObject> _enemyPrefabList;      // This is a list of the avaliable enemys to be spawned

    void Start()
    {
        //_spawnPosition = _spawnObject.transform.position;
        //_spawnRotation = _spawnObject.transform.rotation;

        _spawnedEnemyList = new List<GameObject>(); // Instantiate empty list
        _spawnRotation = Quaternion.identity;       // Zeroize the rotation - We can change this if there is something specific to one of the enemies

        _spawnPosition = GameObject.Find("SpawnPos").transform.position;// Find the spawn pos GameObject

        Invoke("SpawnHandler", Random.Range(_minSpawnTime, _maxSpawnTime)); // Start Spawning an enemy after a random time
    }

    void Update()
    {
 
    }

    private void SpawnHandler()
    {
        if (_spawnedEnemyList.Count < _maxEnemies)                          // Keep a maximum amount just in case - Might be pointless but hey
        {
            SpawnAnEnemy();                                                 
        }

        Invoke("SpawnHandler", Random.Range(_minSpawnTime, _maxSpawnTime)); // Pretty sure this isnt recursion... will use a coroutine if it turns out to be a massive drain
    }                                                                       // Calls the SpawnHandler function after a random amount of time

    private void SpawnAnEnemy()
    {
        // Lets spawn an enemy.
        GameObject Enemy = Instantiate(_enemyPrefabList[Random.Range(0, _enemyPrefabList.Count)], _spawnPosition, _spawnRotation, null);

        // Add anything else that needs to be instantiated here.

        _spawnedEnemyList.Add(Enemy);
    }

    public void RemoveEnemyFromlist(GameObject _enemy)
    {
        Destroy(_enemy);
        _spawnedEnemyList.Remove(_enemy);
    }
}
