using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour {

    // Public variables.

    // Private variables.
    private GameObject _spawnObject;                 // This stores a reference to the spawn object.
    private Vector2 _spawnPosition;
    private Quaternion _spawnRotation;
    private List<GameObject> _spawnedEnemyList;     // This will hold a reference to any enemys that have been spawned into the level.

    // Serialized so we can add prefabs.
    [SerializeField]
    private List<GameObject> _enemyPrefabList;      // This is a list of the avaliable enemys to be spawned

	void Start ()
    {
        _spawnPosition = _spawnObject.transform.position;
        _spawnRotation = _spawnObject.transform.rotation;
	}
	
	void Update ()
    {
		
	}

    private void SpawnAnEnemy()
    {
        // Lets spawn an enemy.
        GameObject Enemy = Instantiate(_enemyPrefabList[Random.Range(0, _enemyPrefabList.Count)],_spawnPosition,_spawnRotation,null);

        // Add anything else that needs to be instantiated here.

        _spawnedEnemyList.Add(Enemy);
    }

    public void RemoveEnemyFromlist(GameObject _enemy)
    {
        _spawnedEnemyList.Remove(_enemy);
    }
}
