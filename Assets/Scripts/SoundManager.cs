using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SFXType
{
	CHANGE_LANE,
	ORB_COLLECT
}

public class SoundManager : MonoBehaviour
{
	static SoundManager sInstance;

	public static SoundManager Instance { get { return sInstance; } }

	void Awake ()
	{
		if (sInstance != null) {
			Destroy (gameObject);
		} else {
			sInstance = this;
			DontDestroyOnLoad (gameObject);
			bgmSource.Play ();
			bgmSourceBeat.Play ();
			bgmSourceBeat.volume = 0.0f;
		}
	}

	public AudioSource bgmSource;
	public AudioSource bgmSourceBeat;
	public AudioSource sfxSource;

	public AudioClip bgmClip;
	public AudioClip bgmClipBeat;

	public AudioClip[] sfxClips;

	public void PlaySFX (SFXType sfx)
	{
		sfxSource.PlayOneShot (sfxClips [(int)sfx]);
	}

	public void SetBeat (bool beatOn)
	{
		bgmSourceBeat.volume = beatOn ? 1.0f : 0.0f;
	}
}
