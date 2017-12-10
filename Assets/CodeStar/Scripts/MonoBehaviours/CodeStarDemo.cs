using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Makist.IO;
using System;


public enum CMD
{
	Ack = 0x01,
	Go = 0x02,
	Speed = 0x03,
	Home = 0x04,
	Reset = 0x05
}

public enum STATUS
{
	Ack = 0x01,
	Stop = 0x02,
	Pass = 0x03
}

public class CodeStarDemo : MonoBehaviour
{
	public GameObject intro;
	public GameObject tutorial;
	public GameObject rank;
	public GameObject test;
	public SettingUI settingUI;
	public TimeUI timeUI;
	public CommSerial codeStarComm;
	public WebCamManager webCamManager;
	public RawImage video;

	private TestAction _action;

	void Awake()
	{
		settingUI.OnSettingFinished.AddListener(OnSettingFinished);
		codeStarComm.OnErrorClosed.AddListener(OnCodeStarSerialError);
	}

	// Use this for initialization
	void Start ()
	{
		intro.SetActive(true);
		tutorial.SetActive(false);
		rank.SetActive(false);
		test.SetActive(false);
		settingUI.gameObject.SetActive(true);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(codeStarComm.IsOpen)
		{
			byte[] data = codeStarComm.Read();
			if(data != null)
			{
				for(int i = 0; i < data.Length; i++)
				{
					byte cmd = (byte)((data[i] & 0xf0) >> 4);
					byte param = (byte)(data[i] & 0x0f);
					if(cmd == (byte)STATUS.Pass || cmd == (byte)STATUS.Stop)
					{
						EndAction();
						break;
					}
				}
			}
		}
	}

	public void OnSettingFinished()
	{
		intro.SetActive(true);
		test.SetActive(false);
		webCamManager.Play(video);
	}

	public void OnTestFinished()
	{
		test.SetActive(false);
		rank.SetActive(true);
	}

	private void OnCodeStarSerialError()
	{
		settingUI.gameObject.SetActive(true);
	}

	public void ReadyCodeStar()
	{
		tutorial.SetActive(true);
		CommandCodeStar(CMD.Go, 0x02);
	}

	public void StartCodeStar()
	{
		tutorial.SetActive(false);
		test.SetActive(true);
		CommandCodeStar(CMD.Go, 0x02);
	}

	public void StartAction(TestAction action)
	{
		_action = action;
		if(_action != null)
		{
			if(_action.autoSkip)
			{
				if(_action.delay > 0f)
					Invoke("EndAction", _action.delay);
				else
					EndAction();
			}

			if(_action.move)
			{
				if(_action.delay > 0f)
					Invoke("Move", _action.delay);
				else
					Move();
			}
		}
	}

	private void EndAction()
	{
		if(_action != null)
		{
			_action.gameObject.SetActive(false);
			if(_action.next != null)
			{
				_action.next.SetActive(true);
				StartAction(_action.next.GetComponent<TestAction>());
			}
			else
				_action = null;
		}
	}

	public void Move()
	{
		CommandCodeStar(CMD.Go, 0x02);
	}

	private void CommandCodeStar(CMD cmd, int param)
	{
		byte data = (byte)((int)cmd << 4);
		data += (byte)(param & 0x0f);
		codeStarComm.Write(new byte[] { data });
	}
}
