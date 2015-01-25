using UnityEngine;
using System.Collections;

public class PlayerAttr : MonoBehaviour {
	
	public static int BloodMax = 5;

	private int Blood = 5;

	PlayerCtrl pCtrl;
	void Start () {
		pCtrl = GetComponent<PlayerCtrl> ();
	}
	
	void Update () {
	
	}

	public void BloodAdd()
	{	
		SoundManager.I.Play (SoundManager.I.AddBlood);
		GameObject effect = Instantiate (GameManager.I.PreFXAddBlood, transform.position, Quaternion.identity) as GameObject;
		effect.transform.parent = transform;

		if(Blood < BloodMax)
		{
			Blood ++;
			UIManager.I.BloodAdd ();
		}

	}

	public void BloodMinus()
	{
		
		Blood--;
		UIManager.I.BloodMinus ();
		if(Blood == 0){
			pCtrl.DoDeath ();
			GameManager.I.GameOver (false);
		}

	}

}
