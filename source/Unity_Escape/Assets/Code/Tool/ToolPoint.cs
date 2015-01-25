using UnityEngine;
using System.Collections;

public class ToolPoint : MonoBehaviour
{
	

	/// <summary>
	/// 判定是否在圆内.
	/// </summary>
	/// <returns><c>true</c>, if point in circle was checked, <c>false</c> otherwise.</returns>
	/// <param name="oriPos">Ori position.</param>
	/// <param name="range">Range.</param>
	public static bool CheckPointInCircle(Vector3 oriPos,float range,Vector3 targetPos)
	{
		return ToolVector.DistanceIgnoreY (oriPos, targetPos) < range;
	}

	/// <summary>
	/// 检查物体是否在扇形内.
	/// </summary>
	/// <returns><c>true</c>, if point in sector was checked, <c>false</c> otherwise.</returns>
	/// <param name="ori">本体</param>
	/// <param name="targetPos">目标物体位置.</param>
	/// <param name="range">半径长度.</param>
	/// <param name="angle">角度.</param>
	public static bool CheckPointInSector (Transform ori, Vector3 targetPos, float range, float angle)
	{

		//必须忽略y轴~~~~
		targetPos = ToolVector.IgnoreY (targetPos);
		Vector3 oriPos = ToolVector.IgnoreY (ori.position);
		//本地的正前方.
		Vector3 oriForward = ToolVector.IgnoreY (ori.forward);
		//目标方向
		Vector3 toTarget = (targetPos - oriPos).normalized;
		//获取物体前方和目标的位置的夹角，判断是否在角度内。
		float toAngle = Vector3.Angle (oriForward, toTarget);

		//距离
		float distance = Vector3.Distance (ori.position, targetPos);
		//Debug.Log("toAngle" + toAngle + "  distance " + distance);
		if (toAngle <= angle / 2 && distance <= range)
			return true;
		return false;

	}

	/// <summary>
	/// 判断目标是否在目标扇形视野中.
	/// </summary>
	/// <param name="source">目标.</param>
	/// <param name="aimPosition">目标点位.</param>
	/// <param name="angle">视野角度.</param>
	/// <param name="range">事业半径.</param>
	/// <param name="ignor_y">是否忽略Y轴的值.</param>
	public static bool CheckInScetor(GameObject source,Vector3 aimPosition, float angle,float range , bool ignor_y = true)
	{
		//出发点位.
		Vector3 sourcePosition = source.transform.position;
		//出发点位视野正前方.
		Vector3 sourceDirection = source.transform.forward;

		if(ignor_y)
		{
			sourcePosition.y = 0;
			aimPosition.y = 0;
		}
		//目标方向.
		Vector3 aimDirection = (aimPosition - sourcePosition).normalized;

		//物体和目标距离.
		float distance = Vector3.Distance (sourcePosition,aimPosition);

		//点集判断
		return Vector3.Dot (sourceDirection, aimDirection) > Mathf.Cos(Mathf.PI * (angle / 360.0f)) && distance <= range;

	}


}
