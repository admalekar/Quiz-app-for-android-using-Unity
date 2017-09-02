using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class TweenBase : MonoBehaviour {

	//Name of tween (for user interface purposes only)
	public string MyTweenName;

	//Kind of tween
	public enum playStyles {Single, PingPong, Looping, SingleAndSwitch, PingPongOnce};
	public playStyles myTweenType;

	//Tweens animation curve to use
	public AnimationCurve myCurve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));

	//Delay, if any
	public float delay = 0f;

	//Duration of Tween
	public float duration = .4f;

	//Total TweenTime
	internal float totalTime;

	//Should I ignore time scale?
	public bool IgnoreTimeScale;
	
	//Internal timer used for computing start / stop
	
	internal float timer = 0.0f;

	//Am I playing or not?
	//[HideInInspector]
	internal bool isPlaying = false;

	//Is my delay over?
	protected bool delayOver = false;

	//Am I Done?
	//[HideInInspector]
	internal bool isDone = false;

	//Am I playing or not?
	[HideInInspector]
	public float percentComplete;

	//Should I play on Awake?
	public bool playOnStart = false;
	
	internal bool paused = false;

	float pingPongCount;

	//EVENTS
	public delegate void TweenCompletedEvent();
	public event TweenCompletedEvent TweenCompleted;

	public virtual void Awake(){
		//Override me!
		//Make sure it doesn't auto trigger (in case you were using this to test)
		isPlaying = false;
	}

	public virtual void Start(){
		if(playOnStart)
			Begin();
	}

	public virtual void Begin(){
		//Override me!
	}

	//Triggers tween while ignoring any delay. Useful for starting a tween immediately, but wanting to
	//use delay for looping and other automatic calls.
	public void BeginInstantly(){
		if(delay > 0f)
			timer = delay;

		Begin ();
	}

	public void Reset(){
		//Debug.Log ("reset");
		percentComplete = 0f;
		isDone = false;
		timer = 0f;
	}

	public virtual void SwitchTargets(){
		//Override me!
	}

	public virtual void Completed(){

		//Override me!
		if(myTweenType == playStyles.PingPongOnce)
			pingPongCount++;

		//Debug.Log ("Completed");

		if(TweenCompleted != null)
			TweenCompleted();
	}
	
	public virtual void Pause(){
		paused = true;
	}

	public virtual void Stop(){
		isPlaying = false;
		isDone = true;
		timer = 0f;
		percentComplete = 1f;
		Completed ();
	}

	public virtual void Resume(){
		paused = false;
	}

	public virtual void Update(){

		//Total Time
		totalTime = duration + delay;

		//If I am not pause and I am playing
		if(!paused && isPlaying){

			//Advance timer based on ignoring timescale or not
			if(IgnoreTimeScale)
				timer += Time.unscaledDeltaTime;
			else 
				timer += Time.deltaTime;

			//Update Information
			percentComplete = timer / totalTime;

			if(timer > delay){
				delayOver = true;
			}

			//If my timer is greater or equal to my duration
			if(timer >= totalTime){

				//Play Once Tween
				if(myTweenType == playStyles.Single){

					//Set all variables to indicate tween has stopped
					isPlaying = false;
					delayOver = false;
					isDone = true;
					percentComplete = 1f;
					timer = 0f;

					//Call Completed function
					Completed();

				}

				//Play Once Tween and switch start and end targets
				if(myTweenType == playStyles.SingleAndSwitch){
					
					//Set all variables to indicate tween has stopped
					isPlaying = false;
					delayOver = false;
					isDone = true;
					percentComplete = 1f;
					timer = 0f;
					
					//Call Completed function
					Completed();

					//Call Switch
					SwitchTargets();

					//Call Reset
					Reset();
					
				}


				//Go Back and Forth
				else if(myTweenType == playStyles.PingPong || (myTweenType == playStyles.PingPongOnce && pingPongCount < 2)){

					//Reset timer
					timer = 0f;

					//Call Completed function
					Completed();

					//Initiate swap
					SwitchTargets();

					if(delay > 0)
						delayOver = false;

					if(myTweenType == playStyles.PingPongOnce && pingPongCount >= 2){

						//Reset Count
						pingPongCount = 0;

						//Set all variables to indicate tween has stopped
						isPlaying = false;
						delayOver = false;
						isDone = true;
						percentComplete = 1f;
						timer = 0f;
					}


				}

				//Looping Tween
				else if(myTweenType == playStyles.Looping){

					//Reset timer
					timer = 0f;

					//Call Completed function
					Completed();

					if(delay > 0)
						delayOver = false;
				}

			}

		}

	}
	
}
