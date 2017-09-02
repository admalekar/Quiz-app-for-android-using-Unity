using UnityEngine;
using System.Collections;

public class TweenSequence : MonoBehaviour {

	public string sequenceName;
	public bool testSequenceOnStart;
	public TweenBase[] tweenSequence;

	public TweenBase GetLongestTween(){

		TweenBase tempTween = new TweenBase();

		for (int i = 0; i < tweenSequence.Length; i++) {
			if(tweenSequence[i].duration > tempTween.duration)
				tempTween = tweenSequence[i];
		}

		return tempTween;
	}

	//Call when starting from the begining and triggered for the first time
	public void BeginSequence(){
		for (int i = 0; i < tweenSequence.Length; i++)
		{
			tweenSequence[i].Begin();
		}
	}

	//Pauses the sequence
	public void StopSequence(){
		for (int i = 0; i < tweenSequence.Length; i++)
		{
			tweenSequence[i].Stop();
		}
	}

	//Reset sequence from the begining
	public void ResetSequence(){
		for (int i = 0; i < tweenSequence.Length; i++)
		{
			tweenSequence[i].Reset();
		}
	}

	//Resume the sequence
	public void ResumeSequence(){
		for (int i = 0; i < tweenSequence.Length; i++)
		{
			tweenSequence[i].Resume();
		}
	}

	//Resume the sequence
	public void ResetAndPlaySequence(){
		ResetSequence();
		ResumeSequence();
	}

	public void Start(){
	
		if(testSequenceOnStart){
			BeginSequence();
		}
	
	}
}
