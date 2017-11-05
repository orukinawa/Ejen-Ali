using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPulser : MonoBehaviour
{
	public VectorGrid grid;
	public float force;
	public float radius;

	IEnumerator Pulse ()
	{
		while (true) {			
			grid.AddGridForce (Vector3.zero, force, radius, Color.blue, false);
			yield return new WaitForSeconds (1.0f);
		}
	}

	void Start ()
	{
		StartCoroutine (Pulse ());
	}
}
