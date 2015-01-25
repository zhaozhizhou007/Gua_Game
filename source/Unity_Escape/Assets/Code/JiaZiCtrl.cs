using UnityEngine;
using System.Collections;

public class JiaZiCtrl : MonoBehaviour
{

	private float time = 2.5f;
	Animator anim;

	void Start ()
	{
		
		anim = GetComponent<Animator> ();

	}
	
	void Update ()
	{
	
	}

	void OnTriggerEnter (Collider other)
	{

		if (other.tag == Tags.Player) {
			GameManager.I.HeroAttr.BloodMinus ();
			GameManager.I.HeroCtrl.CanCtrl = false;
			DoClose ();
			StartCoroutine (SoHeroClose ());
		}
		else if(other.tag == Tags.Monster)
		{
			MonsterCtrl mc = other.GetComponent<MonsterCtrl> ();
			mc.CanCtrl = false;
			mc.Blood = 1;
			DoClose ();
			StartCoroutine (SoMonsterClose (mc));
		}

	}

	void DoClose ()
	{
		SoundManager.I.Play (SoundManager.I.Trop_Close);
		anim.SetBool ("DoClose", true);
		this.collider.enabled = false;


	}

	IEnumerator SoHeroClose ()
	{
		yield return new WaitForSeconds (time);
		GameManager.I.HeroCtrl.CanCtrl = true;

		Destroy (this.gameObject);

	}
	IEnumerator SoMonsterClose (MonsterCtrl mc)
	{
		yield return new WaitForSeconds (time);
		mc.CanCtrl = true;

		Destroy (this.gameObject);

	}

}
