using UnityEngine;
using System.Collections;

public class creditsScript : MonoBehaviour {

	public GameObject credits;

	void Start()
	{
		credits.SetActive (false);
	}
	public void creditsPanelOpen()
	{
		credits.SetActive (true);
	}
	public void creditsPanelClose()
	{
		credits.SetActive (false);
	}
}
