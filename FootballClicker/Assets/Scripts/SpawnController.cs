using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour {

    //private variables
    private List<GameObject> _pool;
    private List<GameObject> _runningList;

    //serialized so we can add prefabs
    [SerializeField]
    private List<GameObject> _spawnList;

	void Start () {
        //new list with good amount of space
        _pool = new List<GameObject>(20);
        //set running list to have the same amount just in case
        _runningList = new List<GameObject>(_pool.Capacity);

        //set up the pool with random prefabs
        for (int i = 0; i < _pool.Capacity; i++)
        {
            _pool.Add(_spawnList[Random.Range(1, _spawnList.Count) - 1]); //array starts at 0, so start at 1, get the count and -1 from the result to adjust back to 0th pos
        }
	}
	
	void Update () {
		
	}
}
