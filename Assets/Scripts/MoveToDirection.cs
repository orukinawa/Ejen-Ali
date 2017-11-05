using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToDirection : MonoBehaviour
{
    public float _speed;
    public Vector3 _direction;
	
	void Update ()
    {
        transform.Translate(_direction.x * _speed, _direction.y * _speed, _direction.z * _speed);	
	}
}
