using UnityEngine;
using UnityEngine.Advertisements;
using System.Collections;

public class adManager : MonoBehaviour {

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
			Debug.Log("The ad was successfully shown.");
			//
			// YOUR CODE TO REWARD THE GAMER
			// Give coins etc.
			break;
		case ShowResult.Skipped:
			Debug.Log("The ad was skipped before reaching the end.");
			break;
		case ShowResult.Failed:
			Debug.LogError("The ad failed to be shown.");
			break;
		}
	}

}
