using UnityEngine;
using System.Collections;

public class TweenValue : TweenBase {
	
	//Starting value
	public float startValue = 1f;
	
	//Ending Value
	public float endValue = 0f;
	
	//Actual Value
	[HideInInspector]
	public float value;
	
	public void Begin(float start, float end){
		
		//Set new Value targets
		startValue = start;
		endValue = end;
		
		//I am now playing
		isPlaying = true;

	}
	
	public override void Begin(){

		if(isPlaying){
			value = endValue;
			timer = 0f;

			//And I'm a single and switch
			if(myTweenType == playStyles.SingleAndSwitch){
				//Debug.Log ("I was " + percentComplete +"% complete. After switching I will be " + Mathf.Abs(percentComplete - 1f) + " % complete");
				percentComplete = Mathf.Abs(percentComplete - 1f);
				timer = totalTime * percentComplete;
				SwitchTargets();
			}
		}


		
		//I am now playing
		isPlaying = true;
		
	}
	
	public override void Completed(){

		base.Completed();
		
		//Make sure start and end values are what they should be
		value = endValue;
		
	}
	
	public override void Update(){
		
		//Base Tween handling
		base.Update();
		
		//If I am playing
		if(isPlaying && delayOver){
			
			//Evaluate state
			value = Mathf.Lerp (startValue, endValue, myCurve.Evaluate((timer-delay)/duration));
		}
		
	}
	
	public override void SwitchTargets(){
		
		base.SwitchTargets();
		
		//Switch my vector targets
		float mySwitchValue;
		mySwitchValue = startValue;
		startValue = endValue;
		endValue = mySwitchValue;
		
	}
	
	public float getValueResults(){
		return value;
	}
	
}
