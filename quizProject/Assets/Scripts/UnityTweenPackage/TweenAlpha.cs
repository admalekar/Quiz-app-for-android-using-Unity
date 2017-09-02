using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class TweenAlpha : TweenValue
{
    [HideInInspector]
    public Image imageAlpha;

	[HideInInspector]
	public RawImage rawImageAlpha;

    [HideInInspector]
    public CanvasGroup canvasAlpha;

    [HideInInspector]
    public Text textAlpha;

	[HideInInspector]
	public Renderer renderAlpha;

	//In the case of a material in Unity 5.0, enable this to tween the emissiveness of a material.
	//public bool tweenEmissionValue;

    public override void Awake()
    {
		base.Awake();

		//Retrieve relevant componenets to tween, checking UI elements first.
        imageAlpha = gameObject.GetComponent<Image>();
		rawImageAlpha = gameObject.GetComponent<RawImage>();
        canvasAlpha = gameObject.GetComponent<CanvasGroup>();
        textAlpha = gameObject.GetComponent<Text>();

		//If no UI elements are found, check for a renderer
		if(!imageAlpha && !canvasAlpha && !textAlpha)
			renderAlpha = gameObject.GetComponent<Renderer>();

    }


    public override void Update()
    {
        //Call Base
        base.Update();

        if (!isPlaying || !delayOver)
            return;

		if (canvasAlpha)
			canvasAlpha.alpha = getValueResults();

        if (imageAlpha && !canvasAlpha)
            imageAlpha.color = new Color(imageAlpha.color.r, imageAlpha.color.g, imageAlpha.color.b, getValueResults());

		if (rawImageAlpha && !canvasAlpha)
			rawImageAlpha.color = new Color(rawImageAlpha.color.r, rawImageAlpha.color.g, rawImageAlpha.color.b, getValueResults());

        if (textAlpha)
            textAlpha.color = new Color(textAlpha.color.r, textAlpha.color.g, textAlpha.color.b, getValueResults());

		if(renderAlpha){
	
			renderAlpha.material.color = new Vector4(renderAlpha.material.color.r,renderAlpha.material.color.g,renderAlpha.material.color.b,getValueResults());
		
			//Example of using alpha to change emission values in Unity 5
//			if(tweenEmissionValue)
//				DynamicGI.SetEmissive(renderAlpha, renderAlpha.material.color * value);
		}
    }

    public override void Completed()
    {
        base.Completed();

		if (imageAlpha && !canvasAlpha)
            imageAlpha.color = new Color(imageAlpha.color.r, imageAlpha.color.g, imageAlpha.color.b, getValueResults());

		if (rawImageAlpha && !canvasAlpha)
			rawImageAlpha.color = new Color(rawImageAlpha.color.r, rawImageAlpha.color.g, rawImageAlpha.color.b, getValueResults());

        if (canvasAlpha)
            canvasAlpha.alpha = getValueResults();

        if (textAlpha)
            textAlpha.color = new Color(textAlpha.color.r, textAlpha.color.g, textAlpha.color.b, getValueResults());

		if(renderAlpha)
			renderAlpha.material.color = new Vector4(renderAlpha.material.color.r,renderAlpha.material.color.g,renderAlpha.material.color.b,getValueResults());
    }
}
