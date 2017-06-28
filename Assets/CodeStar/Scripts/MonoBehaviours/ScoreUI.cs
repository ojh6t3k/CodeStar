using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScoreUI : MonoBehaviour
{
	public Toggle[] stars;
	public Text exp;
	public int starValue = 0;
	public int expValue = 0;
	public float testStarValue = 0f;
	public float testExpValue = 0f;

	private int _starValue = 0;
	private int _expValue = 0;
	private float _testStarValue = 0;
	private float _testExpValue = 0;


	// Use this for initialization
	void Start ()
	{
		
	}

	void OnEnable()
	{
		_testStarValue = testStarValue;
		_testExpValue = testExpValue;

		_starValue = starValue;
		for(int i = 0; i < stars.Length; i++)
		{
			if(_starValue > i)
				stars[i].isOn = true;
			else
				stars[i].isOn = false;
		}

		_expValue = expValue;
		if(exp != null)
			exp.text = ToScoreString(_expValue);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(testStarValue != _testStarValue)
		{
			_testStarValue = testStarValue;
			starValue = (int)_testStarValue;
		}

		if(testExpValue != _testExpValue)
		{
			_testExpValue = testExpValue;
			expValue = (int)_testExpValue;
		}

		if(expValue != _expValue)
		{
			int diff = (int)Mathf.Clamp(Mathf.Abs(expValue - _expValue), 0, (int)(Time.deltaTime * 1000f));
			if(expValue > _expValue)
			{
				_expValue += diff;
				if(_expValue > expValue)
					_expValue = expValue;
			}
			else
			{
				_expValue -= diff;
				if(_expValue < expValue)
					_expValue = expValue;
			}
			
			if(exp != null)
				exp.text = ToScoreString(_expValue);
		}

		if(starValue != _starValue)
		{
			_starValue = starValue;
			for(int i = 0; i < stars.Length; i++)
			{
				if(_starValue > i)
					stars[i].isOn = true;
				else
					stars[i].isOn = false;
			}
		}
	}

	static public string ToScoreString(int value)
	{
		return string.Format("{0:n0}", value);
	}
}
