using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public enum EPlayerState
{
	Idle,
	Move,
	Attack,
	Death

}

public class Tags
{
	public static string None 		= "None";
	public static string Plane 		= "Plane";
	public static string Player 	= "Player";
	public static string Cube 		= "Cube";
	public static string Monster 	= "Monster";

}

public class PlayerCtrl : MonoBehaviour
{
		
	public static string PIsMove = "IsMove";
	public static string PIsIdle = "IsIdle";
	public static string PIsAttack = "IsAttack";
	public static string PIsDeath = "IsDeath";

	public float Speed = 3f;
	private float ARange = 1.49f;
	private float MRange = 1.09f;

	private Animator PlayerAnim;
	private CharacterController PlayerCh;

	//攻击间隙.
	public bool IsAttackInterval = false;
	public EPlayerState CurState = EPlayerState.Idle;
	public GameObject Target;
	public Vector3 TargetPositioin;
	public bool CanCtrl = true;

	//锁定目标.
	public GameObject LockAim;

	void Awake ()
	{	
		PlayerAnim = GetComponent<Animator> ();
		PlayerCh = GetComponent<CharacterController> ();

		TouchManager.I.On_TapOne += On_TapOne;

	}

	void OnDestory ()
	{
		TouchManager.I.On_TapOne -= On_TapOne;
	}

	void Start ()
	{
		
	}

	void Update ()
	{
		
		if (GameManager.I.IsOver)
			return;

		if(CurState == EPlayerState.Death)
		{
			return;
		}
		if (!CanCtrl){
			DoStop ();
			return;
		}

		//判断攻击.
		if (CurState == EPlayerState.Idle && !IsAttackInterval) {
			
			//如果有锁定的怪, 并且锁定的怪在攻击范围,攻击.
			if(LockAim != null)
			{
				float dis = ToolVector.DistanceIgnoreY (transform.position, LockAim.transform.position) ;
				if(dis < ARange)
				{
					DoAttack ();
				}
			}else{
				//没有就查找有无可以锁定的怪.
				FindLockAim();

			}
			
		}

		//判断有没有到达目的地
		if (CurState == EPlayerState.Move && Target != null) {
			
			float distance = ToolVector.DistanceIgnoreY (TargetPositioin, transform.position);
			if (Target.tag == Tags.Plane) {
				if (distance < 0.3f) {
					DoStop ();
				}
			} else if (Target.tag == Tags.Monster || Target.tag == Tags.Cube) {
				if (distance < MRange) {
					DoStop ();
				}
			}
		}

		if (Target != null) {
			DoMove ();
			Vector3 dir = ToolVector.DirectionIgnoreY (transform.position, TargetPositioin);
			transform.forward = dir.normalized;
			PlayerCh.SimpleMove (transform.forward * Speed);

		}

	}

	void On_TapOne (GameObject clickObject, Vector3 position)
	{
		if (!GameManager.I.TouchEnable)
			return;
		if (clickObject.tag == Tags.Plane || clickObject.tag == Tags.Monster || clickObject.tag == Tags.Cube) {
			Target = clickObject;
			TargetPositioin = position;

			//锁定目标.
			if(clickObject.tag != Tags.Plane)
			{
				
				DoLock (Target);

			}

		}

	}

	/// <summary>
	/// 锁定目标.
	/// </summary>
	/// <param name="aim">Aim.</param>
	void DoLock(GameObject aim)
	{
		LockAim = aim;
	}

	#region 角色控制

	//找到目标
	private void FindLockAim ()
	{

		//判断怪物.
		foreach (GameObject m in GameManager.I.BossList) {
			if (m != null && ToolVector.DistanceIgnoreY (transform.position, m.transform.position) < MRange) {
				DoLock (m);
				return;
			}
		}

		foreach (GameObject m in GameManager.I.CubeList) {
			if (m != null && ToolVector.DistanceIgnoreY (transform.position, m.transform.position) < MRange) {
				DoLock (m);
				return;
			}
		}

	}

	public void DoAttack ()
	{
		CleanAnim ();
		PlayerAnim.SetBool (PIsAttack, true);

		//0.733
		//boss:0.933
		CurState = EPlayerState.Attack;

		StartCoroutine (SoAttack ());

	}

	private IEnumerator SoAttack ()
	{	

		Vector3 dict = ToolVector.DirectionIgnoreY (transform.position, LockAim.transform.position);
		transform.forward = dict.normalized;
		yield return new WaitForSeconds (0.755f);
		DoStop ();
		IsAttackInterval = true;
		//攻击间隔
		yield return new WaitForSeconds (1f);
		IsAttackInterval = false;

	}

	private void _AttackCheck(GameObject go)
	{
		bool isDeath = true;
		if(go.tag == Tags.Cube)
		{
			CubeController cc = go.GetComponent<CubeController> ();
			cc.DoHit ();

		}
		else if(go.tag == Tags.Monster)
		{
			MonsterCtrl mc = go.GetComponent<MonsterCtrl> ();
			isDeath = mc.DoHit ();
		}
		SoundManager.I.Play (SoundManager.I.Attack);
		if(isDeath)
		{
			LockAim = null;
		}
	}

	public void DoMove ()
	{
		CurState = EPlayerState.Move;
		CleanAnim ();
		PlayerAnim.SetBool (PIsMove, true);
	}

	public void DoStop ()
	{
		CurState = EPlayerState.Idle;
		CleanAnim ();
		PlayerAnim.SetBool (PIsIdle, true);
		Target = null;
	}

	public void DoDeath ()
	{	
		CurState = EPlayerState.Death;
		CleanAnim ();
		PlayerAnim.SetBool (PIsDeath, true);

		SoundManager.I.Play (SoundManager.I.Death);
	}

	public void CleanAnim ()
	{
		PlayerAnim.SetBool (PIsMove, false);
		PlayerAnim.SetBool (PIsAttack, false);
		PlayerAnim.SetBool (PIsIdle, false);
	}

	#endregion

	public void AnAttack()
	{
		
		_AttackCheck (LockAim);
		
	}

}
