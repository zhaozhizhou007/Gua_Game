using UnityEngine;
using System;
using System.Collections;

/// <summary>
/// 工具管理器
/// </summary>
public class ToolManager : MonoBehaviour
{
	
	private static ToolManager _instance;

	/*---------------------------------------------
        音效管理器
     ---------------------------------------------*/
	public static ToolManager I {
		get { 
			if (_instance == null) 
			{
				_instance = GameObject.FindObjectOfType<ToolManager> () ;
				if (_instance == null)
					_instance = new GameObject (typeof(ToolManager).Name).AddComponent<ToolManager> ();
			}
			return _instance;
		}
	}


	void Awake ()
	{	
		_instance = this;
		DontDestroyOnLoad (this.gameObject);
	}


	/// <summary>
	/// 开始执行延迟任务
	/// </summary>
	/// <param name="delay">延迟时间</param>
	/// <param name="action">执行任务</param>
	public void StartAction (float delay, Action action)
	{
		StartCoroutine (_SoStartAction (delay, action));
	}

	/// <summary>
	/// 开始携程.
	/// </summary>
	/// <param name="soAction">So action.</param>
	public void StartAction (IEnumerator soAction)
	{
		StartCoroutine (soAction);
	}

	private IEnumerator _SoStartAction (float delay, Action action)
	{
		yield return new WaitForSeconds (delay);
		action ();

	}


}
