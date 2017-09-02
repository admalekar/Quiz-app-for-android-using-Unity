using UnityEngine;
using System.Collections;

public class TweenVector : TweenBase {

	// Starting Vector
	public Vector3 startingVector  = new Vector3( 0.0f, 0.0f, 0.0f );

	// Ending Vector
	public Vector3 endVector = new Vector3( 0.0f, 0.0f, 0.0f );

	// Actual Vector
	[HideInInspector]
	public Vector3 vector3Results;

	//If given two vectors begin
	public void Begin(Vector3 pos1, Vector3 pos2){

		//If already playing
		if(isPlaying){
			
			//And I'm a single and switch
			if(myTweenType == playStyles.SingleAndSwitch){
				//Debug.Log ("I was " + percentComplete +"% complete. After switching I will be " + Mathf.Abs(percentComplete - 1f) + " % complete");
				percentComplete = Mathf.Abs(percentComplete - 1f);
				timer = totalTime * percentComplete;
				SwitchTargets();
			}
			else{
				Reset ();
			}
			
		}
		else{
			Reset ();
		}

		//Set new Vector targets
		startingVector = pos1;
		endVector = pos2;

		//I am now playing
		isPlaying = true;

	}

	public override void Begin(){

		//If already playing
		if(isPlaying){

			//And I'm a single and switch
			if(myTweenType == playStyles.SingleAndSwitch){
				Debug.Log ("I was " + percentComplete +"% complete. After switching I will be " + Mathf.Abs(percentComplete - 1f) + " % complete");
				percentComplete = Mathf.Abs(percentComplete - 1f);
				timer = totalTime * percentComplete;
				SwitchTargets();
			}
			else{
				Reset ();
			}
			
		}
		else{
			Reset ();
		}

		//I am now playing
		isPlaying = true;

	}

	public override void Completed(){

		base.Completed();

		//Make sure start and end values are what they should be
		vector3Results = endVector;
		
	}

	public override void Update(){

		//Base Tween handling
		base.Update();

		//If I am playing
		if(isPlaying && delayOver){

			//Evaluate state
			vector3Results = Vector3.Lerp (startingVector, endVector, myCurve.Evaluate((timer-delay)/duration));
		}

	}

	public override void SwitchTargets(){

		base.SwitchTargets();

		//Switch my vector targets
		Vector3 mySwitchVector;
		mySwitchVector = startingVector;
		startingVector = endVector;
		endVector = mySwitchVector;

	}

	public Vector3 getVector3Results(){
		return vector3Results;
	}

}
