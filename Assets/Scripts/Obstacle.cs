using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float _speed;

	void Update ()
    {
        transform.Translate(_speed * Time.deltaTime, 0.0f, 0.0f);
	}
}
