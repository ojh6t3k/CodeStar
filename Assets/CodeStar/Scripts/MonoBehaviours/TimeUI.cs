using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TimeUI : MonoBehaviour
{
	public Text lapTime;
	public Text bestTime;

	public int bestTimeValue;
	public int lapTimeValue;


	// Use this for initialization
	void Start ()
	{
		
	}

	void OnEnable()
	{
		if(bestTime != null)
		{
			int minute = (int)(bestTimeValue / 6000f);
			int second = (int)(bestTimeValue % 6000f);
			second = (int)(second / 100f);
			int millis = (int)(bestTimeValue % 100f);
			bestTime.text = string.Format("{0:d2}:{1:d2}:{2:d2}", minute, second, millis);
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		lapTimeValue += (int)(Time.deltaTime * 100f);
		if(lapTime != null)
		{
			int minute = (int)(lapTimeValue / 6000f);
			int second = (int)(lapTimeValue % 6000f);
			second = (int)(second / 100f);
			int millis = (int)(lapTimeValue % 100f);
			lapTime.text = string.Format("{0:d2}:{1:d2}:{2:d2}", minute, second, millis);
		}
	}
}
