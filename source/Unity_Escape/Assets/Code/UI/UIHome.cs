using UnityEngine;
using System.Collections;

public class UIHome : MonoBehaviour {
	

	public GameObject AboutPanel;

	void Start () {
	
	}
	
	void Update () {
	
	}

	public void GameStart()
	{
		Application.LoadLevel (1);

	}

	public void About()
	{
		AboutPanel.SetActive (true);
	}

	public void AboutClose()
	{
		AboutPanel.SetActive (false);
	}
}
