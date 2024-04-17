using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSystemRENAME : MonoBehaviour
{
    [SerializeField] private List<Transform> _spawnPlaces = new List<Transform>();
    [SerializeField] private GameObject[] ObjectsToInteract = new GameObject[3];
    public int _numberToSpawn;
    //public int PointToWin;
    private int _spawnIndex;
    void Start()
    {
        _numberToSpawn = Random.Range(3, _spawnPlaces.Count);
        //PointToWin = _numberToSpawn;
        ObjectInstantiation();
    }

    void Update()
    {
        
    }

    void ObjectInstantiation()
    {
        for (int i = 0; i < _numberToSpawn; i++)
        {
            _spawnIndex = Random.Range(0, _spawnPlaces.Count);
            Instantiate(ObjectsToInteract[Random.Range(0, ObjectsToInteract.Length)], _spawnPlaces[_spawnIndex]);
            _spawnPlaces.RemoveAt(_spawnIndex);
            
        }
    }
}
