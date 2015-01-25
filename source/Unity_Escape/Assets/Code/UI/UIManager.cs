using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class UIManager : MonoBehaviour {
	

	public static UIManager I;
	public Image[] HPS;

	public GameObject OverPanel;
	public GameObject Success;
	public GameObject Fail;
	public GameObject CtrlPanel;

	public GameObject HomeBtn;

	public int Blood = PlayerAttr.BloodMax ;

	void Awake()
	{
		I = this;
		
	}

	void Start ()
	{
		
		
	}
	
	void Update ()
	{
		
		
	}

	public void Over(bool isSuccess)
	{
		OverPanel.SetActive (true);
		Success.SetActive (isSuccess);
		Fail.SetActive (!isSuccess);
		HomeBtn.SetActive (!isSuccess);
	}

	public void BloodAdd()
	{
		Blood++;
		SetBlood ();
	}

	public void BloodMinus()
	{
		Blood--;
		SetBlood ();
	}

	void SetBlood()
	{
		for(int i = 0 ;i < HPS.Length;i++)
		{		
			HPS [i].gameObject.SetActive (i < Blood);
			
		}

	}

	public void CtrlShow()
	{
		CtrlPanel.SetActive (true);
		GameManager.I.TouchEnable = false;
	}

	public void CtrlDisPlay()
	{
		CtrlPanel.SetActive (false);
		GameManager.I.TouchEnable = true;
	}

	public void DoSuccess()
	{
		//再来一次
		Application.LoadLevel (2);
	}


	public void DoFail()
	{
		//再来一次
		Application.LoadLevel (1);
	}

	public void GoHome()
	{
		Application.LoadLevel (0);
	}

}
