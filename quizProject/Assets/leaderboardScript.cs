using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using Com.Google.Android.Gms.Common.Api;
using GooglePlayGames.BasicApi;


public class leaderboardScript : MonoBehaviour
{
#region PUBLIC_VAR
    public string leaderboard;
	private int leaderboardHighscore;
#endregion
    #region DEFAULT_UNITY_CALLBACKS
    void Start ()
    {
		//leaderboardHighscore = PlayerPrefs.GetInt ("highscore");
        
		// recommended for debugging:
        PlayGamesPlatform.DebugLogEnabled = true;

		// Activate the Google Play Games platform
        PlayGamesPlatform.Activate ();
		Social.localUser.Authenticate ((bool success) =>
			{
				if (success) {
					Debug.Log ("Login Sucess");
				} else {
					Debug.Log ("Login failed");
				}
			});
		//PlayGamesPlatform.Instance.LoadScores(

	
    }
    #endregion
#region BUTTON_CALLBACKS
    /// <summary>
    /// Login In Into Your Google+ Account
    /// </summary>
   
    /// <summary>
    /// Shows All Available Leaderborad
    /// </summary>
    public void OnShowLeaderBoard ()
    {
//        Social.ShowLeaderboardUI (); // Show all leaderboard
        ((PlayGamesPlatform)Social.Active).ShowLeaderboardUI (leaderboard); // Show current (Active) leaderboard
    }


	public void OnShowAchievements()
	{
		((PlayGamesPlatform)Social.Active).ShowAchievementsUI ();
	}
    /// <summary>
    /// Adds Score To leader board
    /// </summary>
    public void OnAddScoreToLeaderBorad ()
    {
        if (Social.localUser.authenticated) {
			Social.ReportScore (PlayerPrefs.GetInt ("highscore"), leaderboard, (bool success) =>
            {
                if (success) {
                    Debug.Log ("Update Score Success");
                    
                } else {
                    Debug.Log ("Update Score Fail");
                }
            });
        }
    }
    /// <summary>
    /// On Logout of your Google+ Account
    /// </summary>
    public void OnLogOut ()
    {
        ((PlayGamesPlatform)Social.Active).SignOut ();
    }
#endregion
}
