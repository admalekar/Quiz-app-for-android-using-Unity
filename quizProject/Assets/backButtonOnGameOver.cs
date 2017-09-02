using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class backButtonOnGameOver : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			SceneManager.LoadScene ("mainmenu");
		}
	}

	public void backButton()
	{
		SceneManager.LoadScene ("mainmenu");
	}
}
