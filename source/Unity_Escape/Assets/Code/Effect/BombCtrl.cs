using UnityEngine;
using System.Collections;


/// <summary>
/// 爆炸物体控制.
/// </summary>
public class BombCtrl : MonoBehaviour {
	
	public GameObject Effect;
	public float Timing = 2f;
	void Start () {
		Invoke ("DoBomb",Timing);
	}
	
	void Update () {
	
	}

	void DoBomb()
	{	

		SoundManager.I.Play (SoundManager.I.Bomb);
		GameObject go = Instantiate (Effect,transform.position,Quaternion.identity) as GameObject;
		go.AddComponent<AutoDestory> ().Time = 2f;

		//范围在圆形范围内 就爆到
		if(GameManager.I.DistanceOfHero (gameObject) < 1.5f)
		{
			//扣血.
			GameManager.I.Hero.GetComponent<PlayerAttr> ().BloodMinus ();
		}

		Destroy (gameObject);

	}

}
