using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {
	
	public static SoundManager I;

	public AudioClip Death,Attack,Bomb,Boss_Come,Boss_Attack,Trop_Close,AddBlood,Door_come;

	void Awake()
	{
		I = this;
		
	}

	void Start ()
	{
	
	}
	
	void Update ()
	{
	
	}
	public void Play(AudioClip clip)
	{
		AudioSource.PlayClipAtPoint (clip, Camera.main.transform.position, 1);
	}

}
