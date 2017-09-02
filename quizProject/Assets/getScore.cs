using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class getScore : MonoBehaviour {

	[SerializeField]
	public static int score;

	[SerializeField]
	public Text scoretext;

	[SerializeField]
	public Text highscoretext;




	[SerializeField]
	public static int highscore;
	// Use this for initialization
	void Start () {
		score=PlayerPrefs.GetInt ("score");
		highscore = PlayerPrefs.GetInt ("highscore");

		scoretext.text = "  " + score;
		highscoretext.text = "  " + highscore;

	}
	
}
