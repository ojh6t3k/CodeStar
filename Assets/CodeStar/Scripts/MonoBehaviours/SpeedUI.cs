using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SpeedUI : MonoBehaviour
{
	public RectTransform needle;
	public Text speed;
	public AnimationCurve startCurve;
	public AnimationCurve loopCurve;

	public float minNeedleAngle = -140f;
	public float maxNeedleAngle = 140f;
	public int maxSpeedValue = 100;
	public int speedValue = 0;

	private int _speedValue = 0;
	private int _diff;
	private int _state = 2;
	private float _time;

	// Use this for initialization
	void Start ()
	{
		
	}

	// Update is called once per frame
	void Update ()
	{
		_time += Time.deltaTime;

		speedValue = Mathf.Clamp(speedValue, 0, maxSpeedValue);
		if(speedValue != _speedValue)
		{
			_diff = speedValue - _speedValue;
			_speedValue = speedValue;
			_time = 0f;
			_state = 0;
		}

		int value = _speedValue;
		if(_state == 0)
		{
			int diff = (int)(_diff * (1f - startCurve.Evaluate(_time)));
			value -= diff;
			
			if(_time > 1f)
			{
				if(_speedValue > 0)
					_state = 1;
				else
					_state = 2;
				_time = 0f;
			}
		}
		else if(_state == 1)
		{
			value += (int)(10f * (1f - loopCurve.Evaluate(_time)));
			if(_time > 1f)
			{
				_time = 0f;
			}
		}

		value = Mathf.Clamp(value, 0, maxSpeedValue);
		float ratio = Mathf.Clamp((float)value / (float)maxSpeedValue, 0f, 1f);
		float angle = (maxNeedleAngle - minNeedleAngle) * ratio + minNeedleAngle;
		if(needle != null)
		{
			Vector3 eulerAngle = needle.localEulerAngles;
			eulerAngle.z = -angle;
			needle.localEulerAngles = eulerAngle;
		}
		if(speed != null)
		{
			speed.text = string.Format("{0:d}", value);
		}
	}
}
