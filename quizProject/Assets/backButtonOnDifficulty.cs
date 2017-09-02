using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class backButtonOnDifficulty : MonoBehaviour {

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
}
