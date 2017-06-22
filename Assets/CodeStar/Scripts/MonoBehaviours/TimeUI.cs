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
			bestTime.text = ToTimeString(bestTimeValue);
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		lapTimeValue += (int)(Time.deltaTime * 100f);
		if(lapTime != null)
		{
			lapTime.text = ToTimeString(lapTimeValue);
		}
	}

	static public string ToTimeString(int timeValue)
	{
		int minute = (int)(timeValue / 6000f);
		int second = (int)(timeValue % 6000f);
		second = (int)(second / 100f);
		int millis = (int)(timeValue % 100f);

		return string.Format("{0:d2}:{1:d2}:{2:d2}", minute, second, millis);
	}
}
