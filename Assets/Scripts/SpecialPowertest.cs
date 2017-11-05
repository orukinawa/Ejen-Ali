using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchScript;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class SpecialPowertest : MonoBehaviour
{
	public Transform yoyoRoot;
	public RoboBakar[] bakars;
	List<RoboBakar> bakarList;

	bool isSpecialActive;

	public LineRenderer lineRend;

	public LineRenderer yoyoRend;

	public Transform yoyoTrans;

	public Image specialBar;

	//public float specialDuration;

	//bool isTouchDown;

	List<RoboBakar> selectBakar;

	public GameObject specialTint;

	List<Vector3> lineRendererVertices;

	bool isYoyoing;

	public void OnResetPress ()
	{
		SceneManager.LoadScene (0);
	}

	void Start ()
	{
		bakarList = new List<RoboBakar> ();
		for (int i = 0; i < bakars.Length; ++i) {
			bakarList.Add (bakars [i]);
		}
	}

	public void OnSpecialPress ()
	{
		if (isSpecialActive)
			return;
		isSpecialActive = true;
		for (int i = 0; i < bakarList.Count; ++i) {
			bakarList [i].Freeze ();
		}
		specialTint.SetActive (true);
		lineRendererVertices = new List<Vector3> ();
		lineRendererVertices.Add (yoyoRoot.position);
		selectBakar = new List<RoboBakar> ();
	}

	void OnEnable ()
	{
		if (TouchManager.Instance != null) {
			TouchManager.Instance.PointersPressed += pointersPressedHandler;
			TouchManager.Instance.PointersReleased += pointersReleasedHandler;
		}
			
	}

	void OnDisable ()
	{
		if (TouchManager.Instance != null) {
			TouchManager.Instance.PointersPressed -= pointersPressedHandler;
			TouchManager.Instance.PointersReleased -= pointersReleasedHandler;
		}
	}

	void pointersPressedHandler (object sender, PointerEventArgs e)
	{
		if (!isSpecialActive)
			return;
		if (HasPressedPointer)
			OnSpecialInputStart ();
	}

	void pointersReleasedHandler (object sender, PointerEventArgs e)
	{
		if (!isSpecialActive)
			return;
		if (!HasPressedPointer) {
			OnSpecialFinish ();
		}
	}

	bool HasPressedPointer {
		get {
			return TouchManager.Instance.PressedPointersCount > 0;
		}
	}

	Vector3 GetPointerWorldPos {
		get {
			Vector3 v = Camera.main.ScreenToWorldPoint (TouchManager.Instance.PressedPointers [0].Position);
			v.z = 0.0f;
			return v;
		}
	}

	void OnSpecialInputStart ()
	{
		lineRend.positionCount = 2;
		lineRend.SetPosition (0, yoyoRoot.position);
		lineRend.SetPosition (1, GetPointerWorldPos);
	}

	void UpdateLastVertex ()
	{
		lineRend.SetPosition (lineRend.positionCount - 1, GetPointerWorldPos);
	}

	void AddBakar (RoboBakar baka)
	{
		selectBakar.Add (baka);
		++lineRend.positionCount;
		lineRend.SetPosition (lineRend.positionCount - 2, baka.targetObject.transform.position);
		UpdateLastVertex ();
	}

	void OnSpecialFinish ()
	{
		if (selectBakar.Count > 0) {
			StartCoroutine (FireYoyo ());
		} else {
			EndYoyo ();
		}
		//lineRend.positionCount = 0;
		//selectBakar = new List<RoboBakar> ();

	}

	public float minDistance;
	public float yoyoSpeed;

	void EndYoyo ()
	{
		isYoyoing = false;
		isSpecialActive = false;
		for (int i = 0; i < bakarList.Count; ++i) {
			bakarList [i].Unfreeze ();
		}
		specialTint.SetActive (false);
		yoyoRend.positionCount = 0;
		lineRend.positionCount = 0;
		lineRend.gameObject.SetActive (true);
		yoyoTrans.gameObject.SetActive (false);
		yoyoTrans.position = yoyoRoot.position;
	}

	void ReturnYoyo ()
	{
		for (int i = 0; i < selectBakar.Count; ++i) {
			bakarList.Remove (selectBakar [i]);
			Destroy (selectBakar [i].gameObject);
		}
		StartCoroutine (RunReturnYoyo ());
	}

	IEnumerator	RunReturnYoyo ()
	{
		lineRend.gameObject.SetActive (false);
		for (int i = lineRend.positionCount - 1; i > 0; --i) {
			yield return StartCoroutine (ReturnTo (lineRend.GetPosition (i - 1)));
		}
		EndYoyo ();
	}

	IEnumerator ReturnTo (Vector3 target)
	{
		while (Vector3.SqrMagnitude (yoyoTrans.position - target) > minDistance) {
			Vector3 newPos = Vector3.MoveTowards (yoyoTrans.position, target, yoyoSpeed * Time.deltaTime);
			yoyoTrans.position = newPos;
			yoyoRend.SetPosition (yoyoRend.positionCount - 1, newPos);
			yield return null;
		}
		yoyoTrans.position = target;
		yoyoRend.SetPosition (yoyoRend.positionCount - 1, target);
		--yoyoRend.positionCount;
	}

	IEnumerator FireYoyo ()
	{
		isYoyoing = true;
		yoyoTrans.gameObject.SetActive (true);
		--lineRend.positionCount;
		yoyoRend.positionCount = 2;
		yoyoRend.SetPosition (0, yoyoRoot.position);
		for (int i = 0; i < lineRend.positionCount - 1; ++i) {
			yield return StartCoroutine (MoveTo (lineRend.GetPosition (i + 1)));
		}
		ReturnYoyo ();
	}

	IEnumerator MoveTo (Vector3 target)
	{
		while (Vector3.SqrMagnitude (yoyoTrans.position - target) > minDistance) {
			Vector3 newPos = Vector3.MoveTowards (yoyoTrans.position, target, yoyoSpeed * Time.deltaTime);
			yoyoTrans.position = newPos;
			yoyoRend.SetPosition (yoyoRend.positionCount - 1, newPos);
			yield return null;
		}
		yoyoTrans.position = target;
		yoyoRend.SetPosition (yoyoRend.positionCount - 1, target);
		++yoyoRend.positionCount;
	}

	void Update ()
	{
		if (isYoyoing)
			return;
		if (isSpecialActive && lineRend.positionCount > 1) {

			//! Raycast
			Vector3 cachedPointerWorldPos = Camera.main.ScreenToWorldPoint (TouchManager.Instance.PressedPointers [0].Position);
			RaycastHit2D hit = Physics2D.Raycast (cachedPointerWorldPos, Vector2.zero);
			if (hit.collider != null) {
				RoboBakar baka = hit.collider.gameObject.GetComponent<RoboBakar> ();
				if (baka != null) {
					if (!selectBakar.Contains (baka)) {
						AddBakar (baka);
					}
				}
			}
			UpdateLastVertex ();
		}
	}
}