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

        transform.Translate(5.12f, 0.0f, 0.0f);
         _obj.transform.position = transform.position;
         _obj.SetActive(true);
    }
}
