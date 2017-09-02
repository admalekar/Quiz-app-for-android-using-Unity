using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TweenColor : TweenBase {
	
	// Starting Color // 
	public Color startingColor = new Color(0f,0f,0f,255f);
	
	// Ending Color
	public Color endColor = new Color(0f,0f,0f,255f);
	
	// Actual Color
	[HideInInspector]
	public Color colorResults = new Color(0f,0f,0f,255f);
	
	[HideInInspector]
	public Image imageColor;

	[HideInInspector]
	public Text textColor;
	
	[HideInInspector]
	public Material materialColor;
	
	public override void Awake(){

		base.Awake ();

		//Retrieve relevant componenets to tween
		imageColor = gameObject.GetComponent<Image>();
		textColor = gameObject.GetComponent<Text>();
		
		if(GetComponent<Renderer>())
			materialColor = GetComponent<Renderer>().material;
		
	}
	
	//If given two vectors begin
	public void Begin(Color c1, Color c2){
		
		//Set new Vector targets
		c1 = startingColor;
		c2 = endColor;
		
		//I am now playing
		isPlaying = true;
		
	}
	
	public override void Begin(){

		if(isPlaying && myTweenType == playStyles.SingleAndSwitch){
		
			percentComplete = Mathf.Abs(percentComplete - 1f);
			timer = totalTime * percentComplete;
			SwitchTargets();
		
		}
		
		//I am now playing
		isPlaying = true;
		
	}
	
	public override void Completed(){
		
		//Make sure start and end values are what they should be
		colorResults = endColor;
		
	}
	
	public override void Update(){
		
		//Base Tween handling
		base.Update();
		
		//If I am playing
		if(isPlaying && delayOver){
			
			//Evaluate state
			colorResults = Color.Lerp (startingColor,endColor, myCurve.Evaluate((timer-delay)/duration));
		}

		if(imageColor && isPlaying && (delayOver == true || delay == 0f))
			imageColor.color = colorResults;

		if(materialColor && isPlaying && (delayOver == true || delay == 0f))
			materialColor.color = colorResults;

		if(textColor && isPlaying && (delayOver == true || delay == 0f))
			textColor.color = colorResults;
		
	}
	
	public override void SwitchTargets(){
		
		base.SwitchTargets();
		
		//Switch my color targets
		Color mySwitchColor;
		mySwitchColor = startingColor;
		startingColor = endColor;
		endColor = mySwitchColor;
		
	}
	
	public Color getColorResults(){
		return colorResults;
	}
}
