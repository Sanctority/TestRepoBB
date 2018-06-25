using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{

    // Public variables.

    // Private variables.
    [SerializeField]
    private float _speed;

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

        GameObject ground = GameObject.FindGameObjectWithTag("Ground"); // Find the ground object and get a reference to it - Code readability

        _spawnPosition = GameObject.Find("SpawnPos").transform.position;// Find the spawn pos GameObject
    }

    void Update()
    {

        SpawnHandler();

        for (int i = 0; i < _spawnedEnemyList.Count; i++)
        {
            _spawnedEnemyList[i].transform.position += (Vector3.left * Time.deltaTime) * _speed;
            if (!_spawnedEnemyList[i].GetComponent<Renderer>().isVisible && _spawnedEnemyList[i].transform.position.x < Camera.main.transform.position.x)
            {
                RemoveEnemyFromlist(_spawnedEnemyList[i]);
                Debug.Log("Removed");
            }
        }
    }

    private void SpawnHandler()
    {
        if (_spawnedEnemyList.Count < 1)
        {
            SpawnAnEnemy();
        }
    }

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
