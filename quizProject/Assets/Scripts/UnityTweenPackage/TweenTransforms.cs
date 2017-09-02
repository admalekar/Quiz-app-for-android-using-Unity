using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class TweenTransforms : TweenVector
{
	//Setting to prevent rebalance of rotations if not desired.
	[Tooltip("If Tween Transform is set to Rotation or Local Rotation and this setting is disabled, Tween Rotations will attempt to rebalance the starting vector to allow for correct tweening.")]
	public bool ignoreRebalance = false;

    //Transform propterties
    public enum TransformTypes { Position, Rotation, Scale, LocalPosition, LocalRotation };
    public TransformTypes TweenTransformProperty;

	//Copy Data Fields
	public enum CopyTransforms {None, StartingVector, EndingVector, DefaultVector};
	public CopyTransforms CopyTransformTo;

	//Set TransformFields
	public enum SetTransforms {None, StartingVector, EndingVector, DefaultVector};
	public SetTransforms SetTransformTo;

	//Default Vector Variables
	[HideInInspector]
	public bool useDefaultVector = false;

	[HideInInspector]
	public Vector3 defaultVector;

	bool defaultVectorSet = false;

	public override void Awake(){

		if(TweenTransformProperty == TransformTypes.LocalRotation || TweenTransformProperty == TransformTypes.Rotation)
			RebalanceRotations();

		base.Awake();

	}

	// This functions makes sure all rotations are expressed in a way that makes sense to Unity.
	// It will convert angles based on the starting and ending vectors to find the best way to tween between the two angles.
	public void RebalanceRotations(){

		if(ignoreRebalance) return;

		Vector3 vectorToChange;
		
		//Converts rotations angles to always be positive so that lerping can work properly all the time
		for (int i = 0; i <= 2; i++)
		{

			//Debug.Log ("starting : " + startingVector[i] + " ending: " + endVector[i]);

			//Debug.Log ("Difference is: " + (startingVector[i] - endVector[i]));

			float difference = (startingVector[i] - endVector[i]);

			if( difference > 180){

				//Copy the vector
				vectorToChange = startingVector;
				//Change the relevant part
				vectorToChange[i] = 360 - vectorToChange[i];
				//Reflect changes
				startingVector = vectorToChange;
			}

			if( difference < -180){
				
				//Copy the vector
				vectorToChange = startingVector;
				//Change the relevant part
				vectorToChange[i] = 360 + vectorToChange[i];
				//Reflect changes
				startingVector = vectorToChange;
			}


//			if(startingVector[i] < 0){
//				//Copy the vector
//				vectorToChange = startingVector;
//				//Change the relevant part
//				vectorToChange[i] = 360 + vectorToChange[i];
//				//Reflect changes
//				startingVector = vectorToChange;
//			}
//			
//			if(endVector[i] < 0){
//				//Copy the vector
//				vectorToChange = endVector;
//				//Change the relevant part
//				vectorToChange[i] = 360 + vectorToChange[i];
//				//Reflect changes
//				endVector = vectorToChange;
//			}
			
			//If you set something to 360, Unity will try to change it.
			//so instead, set it to something close to 360, because Unity is dumb.
			if(startingVector[i] == 360f){
				vectorToChange = gameObject.transform.localEulerAngles;
				vectorToChange[i] = 359.99f;
				gameObject.transform.localEulerAngles = vectorToChange;
			}

		}
			
	}

    public override void Update()
	{
		//While the game is not playing - Editor / Set up functionality
		if(!Application.isPlaying){

			//Copy Transform data to Vectors
			#region Set Vector Data

			Vector3 retrievedVector3 = Vector3.zero;

			if(TweenTransformProperty == TransformTypes.LocalRotation)
				retrievedVector3 = new Vector3 (gameObject.transform.localEulerAngles.x,
				                                gameObject.transform.localEulerAngles.y,
				                                gameObject.transform.localEulerAngles.z);

			else if(TweenTransformProperty == TransformTypes.Rotation)
				retrievedVector3 = new Vector3 (gameObject.transform.eulerAngles.x,
				                                gameObject.transform.eulerAngles.y,
				                                gameObject.transform.eulerAngles.z);

			else if(TweenTransformProperty == TransformTypes.LocalPosition)
				retrievedVector3 = new Vector3 (gameObject.transform.localPosition.x,
				                                gameObject.transform.localPosition.y,
				                                gameObject.transform.localPosition.z);

			else if(TweenTransformProperty == TransformTypes.Position)
				retrievedVector3 = new Vector3 (gameObject.transform.position.x,
				                                gameObject.transform.position.y,
				                                gameObject.transform.position.z);
		
			else if(TweenTransformProperty == TransformTypes.Scale)
				retrievedVector3 = new Vector3 (gameObject.transform.localScale.x,
				                                gameObject.transform.localScale.y,
				                                gameObject.transform.localScale.z);


			if(CopyTransformTo != CopyTransforms.None){

			if(CopyTransformTo == CopyTransforms.StartingVector)
				startingVector = retrievedVector3;
			else if(CopyTransformTo == CopyTransforms.EndingVector)
				endVector = retrievedVector3;
			else if(CopyTransformTo == CopyTransforms.DefaultVector)
				defaultVector = retrievedVector3;

			CopyTransformTo = CopyTransforms.None;
			}
			#endregion

			#region Set Transform Data
			//Copy Vector Data to Transforms
			if(SetTransformTo != SetTransforms.None){

				if(SetTransformTo == SetTransforms.StartingVector)
				{
					if(TweenTransformProperty == TransformTypes.LocalRotation)
						gameObject.transform.localEulerAngles = startingVector;

					else if(TweenTransformProperty == TransformTypes.Rotation)
						gameObject.transform.eulerAngles = startingVector;

					else if(TweenTransformProperty == TransformTypes.LocalPosition)
						gameObject.transform.localPosition = startingVector;

					else if(TweenTransformProperty == TransformTypes.Position)
						gameObject.transform.position = startingVector;

					else if(TweenTransformProperty == TransformTypes.Scale)
						gameObject.transform.localScale = startingVector;
				}
				else if(SetTransformTo == SetTransforms.EndingVector)
				{
					if(TweenTransformProperty == TransformTypes.LocalRotation)
						gameObject.transform.localEulerAngles = endVector;
					
					else if(TweenTransformProperty == TransformTypes.Rotation)
						gameObject.transform.eulerAngles = endVector;
					
					else if(TweenTransformProperty == TransformTypes.LocalPosition)
						gameObject.transform.localPosition = endVector;
					
					else if(TweenTransformProperty == TransformTypes.Position)
						gameObject.transform.position = endVector;
					
					else if(TweenTransformProperty == TransformTypes.Scale)
						gameObject.transform.localScale = endVector;
				}
				else if(SetTransformTo == SetTransforms.DefaultVector)
				{
					if(TweenTransformProperty == TransformTypes.LocalRotation)
						gameObject.transform.localEulerAngles = defaultVector;
					
					else if(TweenTransformProperty == TransformTypes.Rotation)
						gameObject.transform.eulerAngles = defaultVector;
					
					else if(TweenTransformProperty == TransformTypes.LocalPosition)
						gameObject.transform.localPosition = defaultVector;
					
					else if(TweenTransformProperty == TransformTypes.Position)
						gameObject.transform.position = defaultVector;
					
					else if(TweenTransformProperty == TransformTypes.Scale)
						gameObject.transform.localScale = defaultVector;
				}

				SetTransformTo = SetTransforms.None;

			}
			#endregion

			return;
		}
	
        //Call Base
        base.Update();

        //Action to carry out
        if (!isPlaying || !delayOver) return;

		UpdateTransformProperty();
	}

	public void UpdateTransformProperty(){
		switch (TweenTransformProperty)
		{
		case TransformTypes.Position:
			gameObject.transform.position = getVector3Results();
			break;
		case TransformTypes.Rotation:
			gameObject.transform.eulerAngles = getVector3Results();
			break;
		case TransformTypes.LocalRotation:
			gameObject.transform.localEulerAngles = getVector3Results();
			break;
		case TransformTypes.Scale:
			gameObject.transform.localScale = getVector3Results();
			break;
		case TransformTypes.LocalPosition:
			gameObject.transform.localPosition = getVector3Results();
			break;
		}
	}

	public override void Completed()
    {
		base.Completed();

		UpdateTransformProperty();

		if(useDefaultVector && startingVector != defaultVector && !defaultVectorSet){
			startingVector = defaultVector;
			defaultVectorSet = true;
		}
	}

	public override void Begin(){

		if(TweenTransformProperty == TransformTypes.LocalRotation || TweenTransformProperty == TransformTypes.Rotation)
			RebalanceRotations();

		if(isPlaying && !defaultVectorSet && useDefaultVector){
			startingVector = defaultVector;
			defaultVectorSet = true;
		}

		base.Begin();
	}
}
