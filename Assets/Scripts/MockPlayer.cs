using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MockPlayer : MonoBehaviour
{
	public float speed;

	void Update ()
	{
		Vector3 newPos = transform.position;
		newPos.x += speed * Time.deltaTime;
		transform.position = newPos;
	}
}
