using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GridController : MonoBehaviour {
	
	public int X = 11,Y = 7;

	public List<GameObject> CubeList;

	//预制
	public GameObject[] Cubes;

	private GameObject[,] _CubesArray;

	public void Init ()
	{	
		CubeList = new List<GameObject> ();
		//生成11 * 7 的方块面;
		GameObject col = new GameObject ("col");
		_CubesArray = new GameObject[X, Y];
		Vector3 pos = Vector3.one;
		pos.y = 0f;
		for (int i = 0; i < X; i++) {
			for (int j = 0; j < Y; j++) {
				if ((j >= Y / 2 - 1 && j <= Y / 2 + 1) && (i >= X / 2 - 1 && i <= X / 2 + 1)) {
					
				} else {
					
					if (Random.value < 0.3f)
						continue;

					Vector3 newPos = new Vector3 (pos.x * j, pos.y, pos.z * i);
					GameObject go = Instantiate (Cubes [GetRandom ()], newPos, Quaternion.identity) as GameObject;
					CubeController cc = go.GetComponent<CubeController> ();
					cc.Init ();
					go.transform.parent = col.transform;
					go.transform.localScale = new Vector3 (0.9f, 0.9f, 0.9f);
					Debug.Log (cc.Info.Type);
					CubeList.Add (go);
					_CubesArray [i, j] = go;
				}

			}

		}

		//-----------生成门-------------------------------------------
		List<int[]> doorList = new List<int[]> ();

		for (int i = 0; i < X; i++) {
			for (int j = 0; j < Y; j++) {
				if ((i == 0 || i == X - 1) || (j == 0 || j == Y - 1)) {
					if(_CubesArray[i,j] != null)
						doorList.Add (new int[]{i,j});
				}
			}
		}

		int[] indexDoor = doorList [Random.Range (0, doorList.Count)];

		GameObject gog = _CubesArray [indexDoor [0], indexDoor [1]];
		gog.GetComponent<CubeController> ().SetIsDoor ();

	}

	public static int GetRandom ()
	{
		int r = Random.Range (0,4);
		return r;

	}

	/// <summary>
	/// 获取中心点.
	/// </summary>
	/// <returns>The center.</returns>
	public Vector3 GetCenter()
	{	
		int x = X / 2;
		int y = Y / 2 ;
		Vector3 pos = Vector3.one;
		pos.y = 0.5f;
		Vector3 newPos = new Vector3 (pos.x * y,pos.y,pos.z * x);

		return newPos;

	}

}
