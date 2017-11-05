using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
	static MainManager sInstance;

	public static MainManager Instance { get { return sInstance; } }

	void Awake ()
	{
		sInstance = this;
	}
}
