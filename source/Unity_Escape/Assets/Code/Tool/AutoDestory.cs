using UnityEngine;
using System.Collections;

public class AutoDestory : MonoBehaviour {
	
	public float Time = 2f;

	void Start () {
		Invoke ("DoDestory",Time);
	}
	
	void DoDestory()
	{
		Destroy (this.gameObject);
	}


}
