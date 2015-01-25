using UnityEngine;
using System.Collections;

public class WinMoveCtrl : MonoBehaviour {
	
	public GameObject Aim;

	CharacterController cc;
	Animator anim;

	Vector3 aimPos;
	public float Speed = 1f;
	public GameObject BackButton;
	public bool IsDone = false;
	void Start () {
		cc = GetComponent<CharacterController> ();
		anim = GetComponent<Animator> ();
		aimPos = Aim.transform.position;
	}
	
	void Update () {
		if(IsDone)
			return;
		float dis = ToolVector.DistanceIgnoreY (transform.position, aimPos) ;
		if(dis < 0.3f){
			IsDone = true;
			DoCarema ();
			anim.SetBool ("WalkDone",true);
			return;
		}

		Vector3 dir = ToolVector.DirectionIgnoreY (transform.position, aimPos);
		transform.forward = dir.normalized;
		cc.SimpleMove (transform.forward * Speed);
	}

	void DoCarema()
	{	
		Camera.main.transform.parent = null;
		transform.forward = -transform.forward;
		BackButton.SetActive (true);
	}

}
