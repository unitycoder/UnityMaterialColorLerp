using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LerpColorWithUpdate : MonoBehaviour 
{
	public float lerpDuration = 5; // in seconds
	public Color targetColor = Color.clear; // target color to lerp

	// these are only for debugging (to display time and lerp values)
	public Slider slider;
	public Text textTime;

	Material material; // reference to the object material
	Color originalColor; // take original color (so can later reset to it)

	float lerpTimer = 0;
	bool isLerping = false;
	bool lerpFinished = false;

	float timeCounter=0;


	void Start () 
	{
		material = GetComponent<Renderer>().material;
		originalColor = material.color; // take original color
	}

	// main loop
	void Update () 
	{
		// press space to run/pause lerp
		if (Input.GetKeyDown (KeyCode.Space)) ToggleLerp();

		if (isLerping) UpdateLerp();
	}


	// actual lerp is happening here
	void UpdateLerp()
	{
		lerpTimer += Time.deltaTime/lerpDuration;

		// the actual color lerp and setting material color
		material.color = Color.Lerp (originalColor, targetColor, lerpTimer);

		// for debugging only, display info
		timeCounter += Time.deltaTime;
		slider.value = lerpTimer;
		textTime.text = "Lerp: "+lerpTimer+" ( "+Mathf.Round(lerpTimer*100)+"% ) "+" | Time: "+timeCounter;

		// has lerp reached 1? then its finished
		if (lerpTimer >= 1) 
		{
			isLerping = false;
			lerpFinished = true;
		}
	}

	// call this to toggle lerp running/paused
	void ToggleLerp()
	{
		isLerping = !isLerping;

		// if lerp was finished, reset lerp & material
		if (lerpFinished) 
		{
			ResetLerp();
		}
	}

	// reset lerp values and set original material
	void ResetLerp()
	{
		lerpFinished = false;
		lerpTimer = 0;
		timeCounter = 0;
		ResetMaterial();
	}

	void ResetMaterial()
	{
		material.color = originalColor; // restore original color
	}

}
