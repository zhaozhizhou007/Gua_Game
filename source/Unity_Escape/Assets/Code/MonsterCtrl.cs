using UnityEngine;
using System.Collections;


public enum EState
{
	Idle,Attack,Move,Death
	
}

public class MonsterCtrl : MonoBehaviour {

	public static string PIsMove = "IsMove";
	public static string PIsIdle = "IsIdle";
	public static string PIsAttack = "IsAttack";
	public static string PIsDeath = "IsDeath";

	private float Speed = 1.8f;
	public EState CurState = EState.Idle;
	private Animator Anim;
	private CharacterController Cc;

	public int Blood = 3;
	private float Range = 1.05f;
	private float ARange = 2f;

	public bool InAttackInterval = false;

	public bool IsDeath 		= false;

	public float IdleTime 		= 0.9f;
	public float _CurIdleTime 	= 0f;

	public bool CanCtrl = true;

	void Start () {
		
		Anim = GetComponent<Animator> ();
		Cc = GetComponent<CharacterController> ();
		SoundManager.I.Play (SoundManager.I.Boss_Come);
	}
	
	void Update () {
		
		if (!CanCtrl){
			DoStop ();
			return;
		}
			

		_CurIdleTime += Time.deltaTime;
		if (_CurIdleTime < IdleTime)
			return;
		if (GameManager.I.IsOver){
			DoStop ();
			return;
		}

		if (IsDeath)
			return;
		//如果角色在攻击范围内,攻击.
		float dis = ToolVector.DistanceIgnoreY (transform.position, GameManager.I.Hero.transform.position);
		if( !InAttackInterval && (CurState == EState.Idle || CurState == EState.Move ) && dis < Range)
		{
			DoAttack ();
			return;
		}


		//移动
		if(CurState == EState.Idle || CurState == EState.Move)
		{

			if(dis > Range)
			{
				DoMove ();
				Vector3 dict = ToolVector.DirectionIgnoreY (transform.position, GameManager.I.Hero.transform.position);
				transform.forward = dict.normalized;
				Cc.SimpleMove (dict * Speed);

			}else{
				DoStop ();
			}

		}

	}


	public void DoAttack()
	{
		CurState = EState.Attack;
		Anim.SetBool (PIsAttack, true);
		StartCoroutine (SoAttack());
	}


	private IEnumerator SoAttack()
	{

		Vector3 dict = ToolVector.DirectionIgnoreY (transform.position, GameManager.I.Hero.transform.position);

		transform.forward = dict.normalized;
		yield return new WaitForSeconds (0.333f);
		AttackCheck ();

		yield return new WaitForSeconds (0.633f);
		DoStop ();
		InAttackInterval = true;
		yield return new WaitForSeconds (2.6f);
		InAttackInterval = false;

	}

	void AttackCheck()
	{	
		if (CurState == EState.Death)
			return;

		SoundManager.I.Play (SoundManager.I.Boss_Attack);
		bool flag = ToolPoint.CheckInScetor (gameObject,GameManager.I.Hero.transform.position,75,ARange);
		if(flag)
		{
			GameManager.I.Hero.GetComponent<PlayerAttr> ().BloodMinus ();
		}

	}

	public bool DoHit()
	{
		Blood--;
		if(Blood <= 0 )
		{
			DoDeath ();

			return true;
		}
		return false;
	}


	public void DoMove ()
	{
		CurState = EState.Move;
		CleanAnim ();
		Anim.SetBool (PIsMove, true);
	}

	public void DoStop ()
	{
		CurState = EState.Idle;
		CleanAnim ();
		Anim.SetBool (PIsIdle, true);
	}

	public void DoDeath ()
	{
		IsDeath = true;
		this.gameObject.AddComponent<AutoDestory> ().Time = 2.5f;
		this.gameObject.tag = Tags.None;
		Cc.enabled = false;
		CurState = EState.Death;
		CleanAnim ();
		Anim.SetBool (PIsDeath, true);
	
	}

	public void CleanAnim ()
	{
		Anim.SetBool (PIsMove, false);
		Anim.SetBool (PIsAttack, false);
		Anim.SetBool (PIsIdle, false);
	}

}
