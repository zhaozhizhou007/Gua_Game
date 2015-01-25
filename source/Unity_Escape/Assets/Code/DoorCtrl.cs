using UnityEngine;
using System.Collections;

public class DoorCtrl : MonoBehaviour
{

	private float time = 1f;
	Animator anim;

	void Start ()
	{
		
		anim = GetComponent<Animator> ();
		SoundManager.I.Play (SoundManager.I.Door_come);
	}
	
	void Update ()
	{
	
	}

	void OnTriggerEnter (Collider other)
	{

		if (other.tag == Tags.Player) {
			DoOpen();
		}

	}

	void DoOpen ()
	{

		anim.SetBool ("Open", true);
		this.collider.enabled = false;
		GameManager.I.HeroCtrl.CanCtrl = false;
		StartCoroutine (SoClose ());
	}

	IEnumerator SoClose ()
	{
		yield return new WaitForSeconds (time);
		GameManager.I.GameOver (true);
	}

}
