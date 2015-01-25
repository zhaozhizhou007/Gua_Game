using UnityEngine;
using System.Collections;
using System;

public class TouchManager : MonoBehaviour
{

	/// <summary>
	/// 双击的间隔时间.
	/// </summary>
	//public static float DoubleInterval = 0.2f;

	//单例.
	private static TouchManager _instance;

	/*---------------------------------------------
        音效管理器
     ---------------------------------------------*/
	public static TouchManager I {
		get { 
			if (_instance == null) 
			{
				_instance = GameObject.FindObjectOfType<TouchManager> () ;
				if (_instance == null)
					_instance = new GameObject (typeof(TouchManager).Name).AddComponent<TouchManager> ();
			}
			return _instance;
		}
	}
	/*-----------------*/
	/// <summary>
	/// 敲击事件
	/// </summary>
	/// <param name="clickObject"></param>
	/// <param name="position"></param>
	public delegate void DelegateTap (GameObject clickObject,Vector3 position);

	/// <summary>
	/// 单击事件委托
	/// </summary>
	public DelegateTap On_TapOne;

	/// <summary>
	/// 双击事件委托
	/// </summary>
	public DelegateTap On_TapDouble;

	void OnDisable ()
	{
		//移除过滤单击,双击.
		FingerGestures.OnGestureEvent -= OnTapFilter; 
	}


	void Awake ()
	{
		_instance = this;  //懒人 单例.
		//敲击事件钩子,过滤单击,双击.
		FingerGestures.OnGestureEvent += OnTapFilter; 
	}

	void Start ()
	{
	}
	
	//敲击事件
	void OnTapFilter (Gesture gesture)
	{
		if (gesture is TapGesture) {
			TapGesture e = gesture as TapGesture;
			if (e.Selection == null) {
				Debug.Log ("Tap selection is null , so return.");
				return;
			}

			Vector3 hitPos3d = e.Raycast.Hit3D.point;
			if (e.Taps == 0) {
				//设置单击事件
				if (On_TapOne != null) {
					On_TapOne (e.Selection, hitPos3d);

				}

			} else if (e.Taps == 2) {
				//设置双击事件
				if (On_TapDouble != null) {
					On_TapDouble (e.Selection, hitPos3d);
				}
			}
		}

	}

}
