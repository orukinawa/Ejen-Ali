using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RoboBakar : MonoBehaviour
{
	bool isLeft;
	float delay;
	Transform mTransform;
	public float offset;
	public float duration;
	public GameObject targetObject;

	void Start ()
	{
		//mTransform = transform;
		//isLeft = Random.Range (0, 1) == 0 ? false : true;
		//delay = Random.Range (0.0f, 0.5f);
		//StartCoroutine (startAfterDelay ());
	}

	IEnumerator startAfterDelay ()
	{
		yield return new WaitForSeconds (delay);
		if (isLeft)
			GoLeft ();
		else
			GoRight ();
	}

	void GoLeft ()
	{
		mTransform.DOLocalMoveX (mTransform.position.x - offset, duration).OnComplete (GoRight);
	}

	void GoRight ()
	{
		mTransform.DOLocalMoveX (mTransform.position.x + offset, duration).OnComplete (GoLeft);
	}

	public void Freeze ()
	{
		DOTween.PauseAll ();
		targetObject.SetActive (true);
	}

	public void Unfreeze ()
	{
		delay = 0.0f;
		targetObject.SetActive (false);
		StartCoroutine (startAfterDelay ());
	}
}