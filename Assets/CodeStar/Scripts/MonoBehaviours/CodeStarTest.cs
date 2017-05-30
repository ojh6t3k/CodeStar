using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Makist;

public class CodeStarTest : MonoBehaviour
{
	public Toggle tourMode;
	public Toggle correctMode;
	public Toggle incorrectMode;
	public Dropdown velocityList;
	public GameObject completeMessage;

	private SerialComm _serial;

	void Awake()
	{
		completeMessage.SetActive(false);
	}

	// Use this for initialization
	void Start ()
	{
		_serial = FindObjectOfType<SerialComm>();
		if(_serial == null)
			Debug.LogError("Can not find SerialComm!");
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(_serial.IsOpen)
		{
			byte[] data = _serial.Read();
			if(data != null)
			{
				for(int i = 0; i < data.Length; i++)
				{
					if(data[i] == 33)
					{
						completeMessage.SetActive(true);
						break;
					}
				}
			}
		}
	}

	public void Cmd_Go()
	{
		if(_serial.IsOpen)
		{
			byte data = 32;
			if(tourMode.isOn)
				data += 1;
			else if(correctMode.isOn)
				data += 2;
			else if(incorrectMode.isOn)
				data += 3;
			_serial.Write(new byte[] { data });
		}
	}

	public void Cmd_Velocity()
	{
		if(_serial.IsOpen)
		{
			byte data = 48;
			data += (byte)velocityList.value;
			_serial.Write(new byte[] { data });
		}
	}

	public void Cmd_Home()
	{
		if(_serial.IsOpen)
		{
			byte data = 64;
			_serial.Write(new byte[] { data });
		}
	}
}
