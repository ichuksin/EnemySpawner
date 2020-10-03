using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : ObjectsPool
{
    [SerializeField] private GameObject _spawnPointsConteiner;
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private float _spawnTime;
    
    private List<Transform> _spawnPoints = new List<Transform>();
    private float _elapseTime = 0;
    private int _currentPoint = 0;

    private void Start()
    {
        Initialize(_enemyPrefab);
        foreach(var item in _spawnPointsConteiner.GetComponentsInChildren<Transform>())
        {
            if (item.position != _spawnPointsConteiner.transform.position)
                _spawnPoints.Add(item);
        }
    }

    private void Update()
    {
        _elapseTime += Time.deltaTime;
        if (_elapseTime >= _spawnTime)
        {
            Spawn();
            _elapseTime = 0;
        }
        DisableObjectsAbroutScreen();
    }
    
    private void Spawn()
    {
        GameObject enemy;
        if (TryGetObject(out enemy))
        {
            enemy.transform.position = _spawnPoints[_currentPoint++].position;
            enemy.SetActive(true);
            if (_currentPoint == _spawnPoints.Count)
                _currentPoint = 0;
        }
    }
}
