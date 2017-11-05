using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
	static MainManager sInstance;

	public static MainManager Instance { get { return sInstance; } }

	public int fullBarOrb = 10;
	int currentOrb = 0;

	public Button irisButton;
	public Image barFillImage;

	void Awake ()
	{
		sInstance = this;
	}

	void Start ()
	{
		irisButton.onClick.AddListener (OnIrisPress);
	}

	public void GetOrb ()
	{
		if (currentOrb < fullBarOrb) {
			++currentOrb;
			barFillImage.fillAmount = (float)currentOrb / (float)fullBarOrb;
			if (currentOrb == fullBarOrb) {
				irisButton.gameObject.SetActive (true);
			}
		}
	}

	void OnIrisPress ()
	{
		Debug.Log ("IRIS!");
	}

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.P)) {
			GetOrb ();
		}
	}
}
