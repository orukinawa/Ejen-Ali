using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPulser : MonoBehaviour
{
	public VectorGrid grid;
	public float force;
	public float radius;

	public void Pulse ()
	{
		grid.AddGridForce (Vector3.zero, force, radius, Color.blue, false);
	}
}
