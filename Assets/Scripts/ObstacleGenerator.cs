using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    public float _spawnIntervalTime;

	void Start ()
    {
        InvokeRepeating("Spawn", _spawnIntervalTime, _spawnIntervalTime);
	}
	
	void Spawn()
    {
        GameObject _obj = ObjectPooling._objectPooling.GetPooledObject();

        if (_obj == null)
            return;

        int i = Random.Range(0, 2);

        if (i == 0)
            _obj.transform.position = transform.position;
        if (i == 1)
            _obj.transform.position = new Vector3(transform.position.x, transform.position.y + 5.0f, transform.position.z);
        if (i == 2)
            _obj.transform.position = new Vector3(transform.position.x, transform.position.y - 5.0f, transform.position.z);

        _obj.SetActive(true);
    }
}
