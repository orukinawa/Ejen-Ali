using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{
	static TransitionManager sInstance;

	public static TransitionManager Instance { get { return sInstance; } }

	public Image transitionImg;

	public float fadeDuration;

	void Awake ()
	{
		sInstance = this;
		transitionImg.fillAmount = 1.0f;
	}

	void Start ()
	{
		transitionImg.DOFillAmount (0.0f, fadeDuration);
	}

	public void LoadMenu ()
	{
		transitionImg.DOFillAmount (1.0f, fadeDuration).OnComplete (() => {
			SceneManager.LoadScene (0);
		});
	}

	public void LoadGame ()
	{
		transitionImg.DOFillAmount (1.0f, fadeDuration).OnComplete (() => {
			SceneManager.LoadScene (1);
		});
	}
}
