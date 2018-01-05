using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SliderBar : MonoBehaviour,IPointerDownHandler,IPointerUpHandler {

	private bool isMouseDown=false;
	private Slider sliderBar;
	public float sliderValue;

	// Use this for initialization
	void Start () {

		sliderBar = GetComponent <Slider> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		//print (isMouseDown);
		if(!isMouseDown)
		{
			sliderBar.value = Mathf.Lerp (sliderBar.value, 0, Time.deltaTime*3.0f);//Vector3.Lerp ();
		}
		sliderValue = sliderBar.value;
	}

	#region IPointerDownHandler implementation

	public void OnPointerDown (PointerEventData eventData)
	{
		isMouseDown = true;
	}

	#endregion

	#region IPointerUpHandler implementation

	public void OnPointerUp (PointerEventData eventData)
	{
		isMouseDown = false;
	}

	#endregion
}
