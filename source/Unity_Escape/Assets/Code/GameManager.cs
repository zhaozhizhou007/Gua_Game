using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
	

	public static GameManager I;


	//盒子消失
	public GameObject PreCubeDis;

	//炸弹
	public GameObject PreBomb;

	//精灵
	public GameObject PreSpirit;

	//陷阱
	public GameObject PreTrap;

	//门
	public GameObject PreDoor;

	//怪物
	public GameObject PreMonster;

	//回血效果
	public GameObject PreFXAddBlood;

	public GameObject HeroPre;
	public GridController gc;
	public GameObject Hero;
	public PlayerCtrl HeroCtrl;
	public PlayerAttr HeroAttr;


	public List<GameObject> BossList;
	public List<GameObject> CubeList;

	public bool IsOver = false;
	public bool TouchEnable = true;
	void Awake()
	{	
		I = this;

		gc = this.gameObject.GetComponent<GridController> ();
		gc.Init ();

		Init ();

		//方块列表同步
		CubeList = gc.CubeList;
		BossList = new List<GameObject> ();

	}


	void Init()
	{
		Vector3 heroPos = gc.GetCenter ();
		heroPos.y = 0.2f;
		Hero = Instantiate (HeroPre, heroPos, Quaternion.identity) as GameObject;
		HeroCtrl = Hero.GetComponent<PlayerCtrl> ();
		HeroAttr = Hero.GetComponent<PlayerAttr> ();
	}

	void Start () {
		
	}
	
	void Update () {

		if(Input.GetKeyUp (KeyCode.A))
		{
			Camera.main.isOrthoGraphic = true;
		}
		if(Input.GetKeyUp (KeyCode.B))
		{
			Camera.main.isOrthoGraphic = false;
		}


	}
	/// <summary>
	/// 计算物体和英雄的距离.
	/// </summary>
	/// <returns>The of hero.</returns>
	/// <param name="ori">Ori.</param>
	public float DistanceOfHero(GameObject ori)
	{
		return Vector3.Distance (ori.transform.position,Hero.transform.position);
	}


	public void GameOver(bool flag)
	{
		IsOver = true;
		UIManager.I.Over (flag);
	
	}

}
