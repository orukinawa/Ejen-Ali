using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject _enemy;
    public int _pooledAmount;
    public float _spawnIntervalTime;


    List<GameObject> _enemies;

    // Use this for initialization
    void Start()
    {
        _enemies = new List<GameObject>();

        for (int i = 0; i < _pooledAmount; i++)
        {
            GameObject _obj = (GameObject)Instantiate(_enemy);
            _obj.SetActive(false);
            _enemies.Add(_obj);
        }

        InvokeRepeating("Spawn", _spawnIntervalTime, _spawnIntervalTime);
    }

    void Spawn()
    {
        for (int i = 0; i < _pooledAmount; i++)
        {
            if (!_enemies[i].activeInHierarchy)
            {
                _enemies[i].transform.position = new Vector3(transform.position.x, transform.position.y - 5.0f, transform.position.z);
                _enemies[i].SetActive(true);
                _spawnIntervalTime = Random.Range(0.5f, 5.0f);
                break;
            }
        }
    }
}