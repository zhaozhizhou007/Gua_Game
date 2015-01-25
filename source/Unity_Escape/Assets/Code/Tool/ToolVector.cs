using UnityEngine;
using System.Collections;

public class ToolVector
{

	/// <summary>
	/// 忽略Y轴位移.
	/// </summary>
	/// <returns>The y.</returns>
	/// <param name="target">Target.</param>
	public static Vector3 IgnoreY (Vector3 target)
	{
		return new Vector3 (target.x, 0, target.z);  
	}

	/// <summary>
	/// Offset the specified ori, x, y and z.
	/// </summary>
	/// <param name="ori">Ori.</param>
	/// <param name="x">The x coordinate.</param>
	/// <param name="y">The y coordinate.</param>
	/// <param name="z">The z coordinate.</param>
	public static Vector3 offset (Vector3 ori, float x, float y, float z)
	{
		return new Vector3 (ori.x + x, ori.y + y, ori.z + z);
	}

	/// <summary>
	/// 忽略Y 轴的距离.
	/// </summary>
	/// <returns>The no y.</returns>
	/// <param name="ori">Ori.</param>
	/// <param name="pri">Pri.</param>
	public static float DistanceIgnoreY (Vector3 ori, Vector3 pri)
	{
		return Vector3.Distance (IgnoreY (ori), IgnoreY (pri));
	}

	/// <summary>
	/// 获取忽略Y周的标准方向
	/// </summary>
	/// <param name="ori"></param>
	/// <param name="pri"></param>
	/// <returns></returns>
	public static Vector3 DirectionIgnoreY (Vector3 from, Vector3 to)
	{
		return IgnoreY (to - from).normalized;
	}

	/// <summary>
	/// 通过一点位，随即生产半径内的点位.
	/// </summary>
	/// <returns>The random point.</returns>
	/// <param name="oriPoint">原始点位.</param>
	/// <param name="radius">半径.</param>
	/// <param name="y">Y 轴的固定位置.</param>
	public static Vector3 CreateRandomPoint (Vector3 oriPoint, float radius, float y)
	{
		return new Vector3 (oriPoint.x + Random.Range (-radius, radius), y, oriPoint.z + Random.Range (-radius, radius));
	}

	/// <summary>
	/// 使物体转向目标.
	/// </summary>
	/// <param name="fromObj">From object.</param>
	/// <param name="toObj">To object.</param>
	public static void TurnToAim(GameObject fromObj, GameObject toObj)
	{
		fromObj.transform.forward = ToolVector.DirectionIgnoreY (fromObj.transform.position, toObj.transform.position);

	}
}
