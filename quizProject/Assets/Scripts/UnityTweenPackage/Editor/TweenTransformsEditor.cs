using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(TweenTransforms),true)]
public class TweenTransformsEditor : Editor {

	public override void OnInspectorGUI(){

		DrawDefaultInspector();

		TweenTransforms myScript = (TweenTransforms)target;

		if(GUILayout.Button("Save Data")){
			Debug.Log ("Saving...");
			EditorUtility.SetDirty(target);
		}

		if(!myScript.useDefaultVector){

			if(GUILayout.Button("Show Default Vector")){
				myScript.useDefaultVector = true;
			}

		}else{

			if(GUILayout.Button("Hide Default Vector")){
				myScript.useDefaultVector = false;
			}

		}

		if(myScript.useDefaultVector)
			myScript.defaultVector = EditorGUILayout.Vector3Field("Default Vector:",myScript.defaultVector);
	
	}
}
