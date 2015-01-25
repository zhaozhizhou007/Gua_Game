using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CubeController : MonoBehaviour {
	

	public CubeInfo Info;
	private bool IsDoor = false;


	public Dictionary<ECubeType,GameObject> PreDict;

	public void Init()
	{
		//随即生产方块类型.
		Info = new CubeInfo(true);
	}


	public void SetIsDoor()
	{
		Info.Type = ECubeType.Door;
		IsDoor = true;
	}

	void Start () {
		
		PreDict = new Dictionary<ECubeType, GameObject> ();
		PreDict.Add (ECubeType.Empty,	GameManager.I.PreCubeDis);
		PreDict.Add (ECubeType.Door,	GameManager.I.PreDoor);
		PreDict.Add (ECubeType.Bomb,	GameManager.I.PreBomb);
		PreDict.Add (ECubeType.Spirit,	GameManager.I.PreSpirit);
		PreDict.Add (ECubeType.Enemy,	GameManager.I.PreMonster);
		PreDict.Add (ECubeType.Trap,	GameManager.I.PreTrap);

	}
	
	void Update () {
	}

	public void DoHit()
	{
		
		//盒子消失特效.
		Instantiate (GameManager.I.PreCubeDis, transform.position, Quaternion.identity);
		GameObject model = PreDict [Info.Type];

		GameObject go = Instantiate (model, transform.position, Quaternion.identity) as GameObject;
		if (go.tag == Tags.Monster)
			GameManager.I.BossList.Add (go);

		Destroy (gameObject);

	}

	void OnDrawGizmos()
	{
		if(IsDoor)
		{
			Vector3 pos = gameObject.transform.position;
			pos.y = 1.5f;
			Gizmos.DrawCube (pos, Vector3.one * 0.2f);
		}

	}

}
