using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
	public Transform playerPivot;
	public Transform cameraHolder;

	Transform mTransform;

	void Start ()
	{
		mTransform = transform;
	}

	void LateUpdate ()
	{
		Vector3 newPos = Vector3.zero;
		newPos.x = playerPivot.position.x;
		mTransform.position = newPos;
	}
}
