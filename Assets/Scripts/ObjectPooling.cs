using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    public static ObjectPooling _objectPooling;
    public GameObject _pooledObject;
    public int _pooledAmount;
    public bool _isGrow = true;

    List<GameObject> _pooledObjects;

    private void Awake()
    {
        _objectPooling = this;
    }

    void Start ()
    {
        _pooledObjects = new List<GameObject>();

        for(int i = 0; i < _pooledAmount; i++)
        {
            GameObject _obj = (GameObject)Instantiate(_pooledObject);
            _obj.SetActive(false);
            _pooledObjects.Add(_obj);
        }
	}
	
    public GameObject GetPooledObject()
    {
        for (int i = 0; i < _pooledAmount; i++)
        {
            if(!_pooledObjects[i].activeInHierarchy)
            {
                return _pooledObjects[i];
            }
        }

        if (_isGrow)
        {
            GameObject _obj = (GameObject)Instantiate(_pooledObject);
            _pooledObjects.Add(_obj);
            return _obj;
        }

        return null;
    }
}
