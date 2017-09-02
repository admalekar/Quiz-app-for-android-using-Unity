using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;


public class game_Manager : MonoBehaviour {





	public AudioClip rightsound;
	public AudioClip wrongsound;

	public Transform loadingBar;

	AudioSource mysound;


	public Question[] questions;
	private static List<Question> unansweredQuestions;
	private Question currentQuestion;

	[SerializeField]
	public static int adCalled = 0;



	public Button mybuttontrue;
	public Button mybuttonfalse;
	public GameObject canvaspausemenu;
	public GameObject ExtraLifeMenu;
	public GameObject backblackcolor;
	public GameObject mycolor;
	public GameObject coinsize;

	public GameObject netChecker;

	public static int highscore;


	[SerializeField]
	private Text FactText;

	[SerializeField]
	private Text Timertext;

	[SerializeField]
	private Text joketext;




	[SerializeField]
	private static int scorePoints;



	[SerializeField]
	private static int livesLeft = 5;

	[SerializeField]
	private Text scoreText;

	[SerializeField]
	private Text LifeText;

	[SerializeField]
	private Animator animator;

	[SerializeField]
	private float timeBetQuestions = 1.5f;


	private float timeForQuestion = 15f;

	[SerializeField]
	private Text TrueAnswerText;

	[SerializeField]
	private Text FalseAnswerText;

	string[] joke = new string[]{"Dad: Can I see your report card, son? \n Son: I don't have it. \n Dad: Why? \n Son: I gave it to my friend. He wanted to scare his parents.", 
		"Did you know if you were to stretch your blood vessels out end to end in a straight line, you would die !", 
		"Interviewer: What are your qualifications? \n Student: I have PhD\n Interviewer: Wow, which field? \n Student: No, I just Passed Highschool with Difficulty",
		"There are 10 types of people in this world. \n Those who can understand binary and those who can't",
		"I put my phone in airplane mode, but its not flying!",
		"When a door closes another door should open, but if it doesn't then go in through the window.",
		"Who says nothing is impossible? I've been doing nothing for years."};
	string[] correctstring = new string[]{"Fantastic, Thats Correct!","Its the Correct Answer","Thats Right!","You are Right!","Right, Well done","Right Answer!"};
	string[] wrongstring = new string[]{"Wrong answer","Thats wrong"};



	public void Start()
	{
		
		Init1 ();
	}

	public void Init1()
	{

		mysound = GetComponent<AudioSource> ();
		//gameObject.GetComponent<TweenColor> ();
		mycolor.gameObject.GetComponents<TweenColor> ().Last().enabled = false;
		mycolor.gameObject.GetComponents<TweenColor> ().First ().enabled = false;
		coinsize.gameObject.GetComponent<TweenTransforms> ().enabled = false;
		//for questions
		if (unansweredQuestions == null || unansweredQuestions.Count == 0) {
			unansweredQuestions = questions.ToList<Question> ();


		}

		SetCurrentQuestion ();

		UpdateScore();
		UpdateLife ();
		highscore = PlayerPrefs.GetInt ("highscore", highscore);
		//Debug.Log (highscore);



	}

	public void Update()
	{
		//for timer
		timeForQuestion -= Time.deltaTime;
		Timertext.text = "" + Mathf.Ceil(timeForQuestion).ToString();
		LifeText.text = "x " + livesLeft ; 

		if (timeForQuestion < 1.5f)
		{
			
			StartCoroutine (TransitionToNextQuestionWithoutAnswer ());

		}

		loadingBar.GetComponent<Image> ().fillAmount = timeForQuestion / 15;



		//For pausing the game
		if (Input.GetKeyDown (KeyCode.Escape)) {
			//pause = !pause;
			Pause();

		}
		/*
		if (pause) {
			canvaspausemenu.SetActive (true);
			Time.timeScale = 0;
			setCurrentJoke ();
			mybuttontrue.enabled = false;
			mybuttonfalse.enabled = false;
		}
		if (!pause) {
			canvaspausemenu.SetActive (false);
			Time.timeScale = 1;
			mybuttontrue.enabled = true;
			mybuttonfalse.enabled = true;

		}*/



	}



	public void Pause()
	{
		canvaspausemenu.SetActive (!canvaspausemenu.activeSelf);
		mybuttontrue.enabled = !mybuttontrue.enabled;
		mybuttonfalse.enabled = !mybuttonfalse.enabled;
		setCurrentJoke ();
		Time.timeScale = Time.timeScale == 0 ? 1 : 0;

	}

	public void AddScore(int newScore){
		scorePoints += newScore;
		coinsize.gameObject.GetComponent<TweenTransforms> ().enabled = true;
		UpdateScore();
		if(highscore >= 100)
		{	
			

			Social.ReportProgress("CgkI7bDngLMIEAIQAQ", 100.0f, (bool success) => {
				//first achieevment
			});
		}
		if(highscore >= 400)
		{	


			Social.ReportProgress("CgkI7bDngLMIEAIQBw", 100.0f, (bool success) => {
				//second acievement
			});
		}
		if(highscore >= 300)
		{
			if(livesLeft == 5)
			{
				Social.ReportProgress("CgkI7bDngLMIEAIQAg", 100.0f, (bool success) => {
					//third acievement
				});
			}
			
		}
		if(highscore >= 1000)
		{	


			Social.ReportProgress("CgkI7bDngLMIEAIQAw", 100.0f, (bool success) => {
				//fourth acievement
			});
		}
		if(highscore >= 1500)
		{	


			Social.ReportProgress("CgkI7bDngLMIEAIQBA", 100.0f, (bool success) => {
				//fifth acievement
			});
		}

		if(highscore >= 2000)
		{	


			Social.ReportProgress("CgkI7bDngLMIEAIQBQ", 100.0f, (bool success) => {
				//sixth acievement
			});
		}

		/*
		Social.ReportScore(highscore,"CgkI7bDngLMIEAIQBg",(bool success) => {
			// handle success or failure
		});
*/
	}




	public void RemoveLife()
	{
		livesLeft --;
		UpdateLife ();
	}

	public void AddLife()
	{
		livesLeft = 1;
	}

	void UpdateLife()
	{
		Debug.Log (netChecker.GetComponent<InternetChecker> ().NetAvailable);
		LifeText.text = "x " + livesLeft;
		// Game over screen
		if (livesLeft <= 0 )
		{
			if (adCalled == 0) {
				
				//for some reason color turns to black at extralives menu
				backblackcolor.SetActive (false);

				if (Application.internetReachability != NetworkReachability.NotReachable)
				{
					//if (netChecker.GetComponent<InternetChecker> ().NetAvailable == 1) {
						Debug.Log ("netconnection");
						ExtraLifeMenu.SetActive (true);
						Time.timeScale = 0;
					//}
				}else{
					SceneManager.LoadScene ("gameOverScreen");
					resetScore ();
					adCalled = 0;
				}

			}
			if (adCalled == 1) 
			{
				SceneManager.LoadScene ("gameOverScreen");
				resetScore ();
				adCalled = 0;

			}
		
		}





	}


	void UpdateScore(){
		
		scoreText.text =  "" + scorePoints;
		PlayerPrefs.SetInt ("score", scorePoints);
		if (scorePoints > highscore) {
			PlayerPrefs.SetInt ("highscore", scorePoints);
			PlayerPrefs.Save ();

		}



	}

	void setCurrentJoke()
	{
		string pauseJoke = joke [Random.Range (0,joke.Length)];
		joketext.text = pauseJoke;

	}

	void SetCurrentQuestion ()
	{
		int randomQuestionIndex = Random.Range (0, unansweredQuestions.Count);
		currentQuestion = unansweredQuestions [randomQuestionIndex];

		FactText.text = currentQuestion.fact;
		if (currentQuestion.isTrue) {
			//TrueAnswerText.text = "Correct";
			//FalseAnswerText.text = "Wrong";
			TrueAnswerText.text = correctstring [Random.Range (0,5)];
			if(scorePoints == 80)
			{
				TrueAnswerText.text = "Correct! You are at 100 points";
			}

			FalseAnswerText.text = wrongstring [Random.Range (0,1)];
			if (livesLeft == 2) 
			{
				FalseAnswerText.text = "Wrong answer. Last life	!!";
			}
					
		} else {
		
			TrueAnswerText.text = wrongstring [Random.Range (0,1)];
			FalseAnswerText.text = correctstring [Random.Range (0,5)];

		
		
		}

	}

	IEnumerator TransitionToNextQuestionWithoutAnswer()
	{

		mybuttontrue.enabled = false;
		mybuttonfalse.enabled = false;

		unansweredQuestions.Remove (currentQuestion);

		yield return new WaitForSeconds (timeBetQuestions);

		AddScore (-10);

		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);


	}

	IEnumerator TransitionToNextQuestion()
	{

		mybuttontrue.enabled = false;
		mybuttonfalse.enabled = false;

		unansweredQuestions.Remove (currentQuestion);

		yield return new WaitForSeconds (timeBetQuestions);
	
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);


	}

	public void skip()

	{
		ExtraLifeMenu.SetActive (false);
		Time.timeScale = 1;
		//Application.LoadLevel ("gameOverScreen");
		SceneManager.LoadScene ("gameOverScreen");
		resetScore ();

	}

	public void resetScore()
	{
		scorePoints = 0;
		livesLeft = 5;
	}

	public void watchedAd()
	{
		ExtraLifeMenu.SetActive (false);
		Time.timeScale = 1;
	}


	public void exit()
	{
		Time.timeScale = 1;
		SceneManager.LoadScene ("mainmenu");
		resetScore ();

	}


	//advertisements code
	public void showAd()
	{
		if (Advertisement.IsReady ()) 
		{
			Advertisement.Show ("video",new ShowOptions(){resultCallback = HandleShowResult});


		}
	}
	private void HandleShowResult(ShowResult result)
	{
		switch (result)
		{
		case ShowResult.Finished:
			Debug.Log ("The ad was successfully shown.");
			//
			// YOUR CODE TO REWARD THE GAMER
			// Give coins etc.
			adCalled = 1;
			AddLife ();
			Time.timeScale = 1;
			SceneManager.LoadScene ("ThirdVid with ad try");
			Debug.Log (adCalled);


			break;
		case ShowResult.Skipped:
			Debug.Log ("The ad was skipped before reaching the end.");
			//skipped ads buts i m still rewarding
			adCalled = 1;
			AddLife ();
			Time.timeScale = 1;
			SceneManager.LoadScene ("ThirdVid with ad try");

			break;
		case ShowResult.Failed:
			Debug.LogError ("The ad failed to be shown.");
			Time.timeScale = 1;
			SceneManager.LoadScene ("gameOverScreen");

			break;
		}
	}





	public void UserSelectTrue()
	{
		
			animator.SetTrigger ("True");



			if (currentQuestion.isTrue) 
			{
			    mysound.PlayOneShot (rightsound);
				mycolor.gameObject.GetComponents<TweenColor> ().First ().enabled = true;
			    AddScore (20);
				//Debug.Log ("Correct");

			} 
		    else 
		    {
			    mysound.PlayOneShot (wrongsound);
				mycolor.gameObject.GetComponents<TweenColor> ().Last().enabled = true;
				RemoveLife ();	
				//	AddScore (-15);
				//Debug.Log ("Wrong");
			
			}


		StartCoroutine (TransitionToNextQuestion());

	}
	public void UserSelectFalse()
	{
		
			animator.SetTrigger ("False");
			if (!currentQuestion.isTrue) 
		    {
			    mysound.PlayOneShot (rightsound);
				mycolor.gameObject.GetComponents<TweenColor> ().First ().enabled = true;
				AddScore (20);
				//Debug.Log ("Correct");

			} 
		    else 
			{
			    mysound.PlayOneShot (wrongsound);
				mycolor.gameObject.GetComponents<TweenColor> ().Last().enabled = true;
				RemoveLife ();
				//	AddScore (-15);
				//Debug.Log ("Wrong");

			}


		StartCoroutine (TransitionToNextQuestion());

	}
}


