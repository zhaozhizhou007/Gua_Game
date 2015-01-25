using UnityEngine;
using System.Collections;

public class AddBloodCtrl : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other)
    {

    	if(other.tag == Tags.Player)
    	{
    		GameManager.I.HeroAttr.BloodAdd ();
			Destroy (this.gameObject);
    	}

    }

}
