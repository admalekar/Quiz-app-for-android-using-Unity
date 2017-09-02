using UnityEngine;
using System.Collections;

public class exitScript : MonoBehaviour {

	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			
			Application.Quit ();
		}
	}

	public void exitButton()
	{
		
		Application.Quit ();
	}

}
