using UnityEngine;
using System.Collections;


public enum ECubeType
{
	Empty,		//空的.
	Bomb,		//炸弹.
	Spirit,		//精灵.
	Trap,		//陷阱.
	Enemy,		//敌人.
	Door,		//门.

}


[System.Serializable]
public class CubeInfo {

	public ECubeType[] cubetypes = {
		ECubeType.Bomb,
		ECubeType.Spirit,
		ECubeType.Trap
		};

	public ECubeType Type = ECubeType.Empty;


	/// <summary>
	/// 随即初始化
	/// </summary>
	/// <param name="isRandom">If set to <c>true</c> is random.</param>
	public CubeInfo(bool isRandom)
	{
		if(isRandom)
		{
			//Random.seed = seed++;
			float v = Random.value;
			if(v > 0.75f)
			{
				Type = ECubeType.Empty;
			}
			else if(v > 0.65f)
			{
				Type = ECubeType.Enemy;
			}
			else
			{
				int vv = Random.Range (0, 3);
				Type = cubetypes [vv];
			}

		}
	}

}
