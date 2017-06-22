using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Makist.IO;

public class CodeStarTest : MonoBehaviour
{
	public CommandData[] commands;
	public Text output;
	public Text input;
	public ScrollRect scrollRect;
	public Button clear;

	private SerialComm _serial;


	void Awake()
	{
		for(int i=0; i<commands.Length; i++)
			commands[i].OnCommandSend.AddListener(OnCommandSend);

		clear.onClick.AddListener(OnClearClick);
	}

	// Use this for initialization
	void Start ()
	{
		_serial = FindObjectOfType<SerialComm>();
		if(_serial == null)
			Debug.LogError("Can not find SerialComm!");
		else
			_serial.OnOpen.AddListener(OnSerialOpened);
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
					string text = "";

					if(input.text.Length > 0)
						text += "\n";

					text += string.Format("{0:d} (0x{1:X}) (b", data[i], data[i]);
					if((data[i] & 128) != 0)
						text += "1";
					else
						text += "0";

					if((data[i] & 64) != 0)
						text += "1";
					else
						text += "0";

					if((data[i] & 32) != 0)
						text += "1";
					else
						text += "0";

					if((data[i] & 16) != 0)
						text += "1";
					else
						text += "0";

					if((data[i] & 8) != 0)
						text += "1";
					else
						text += "0";

					if((data[i] & 4) != 0)
						text += "1";
					else
						text += "0";

					if((data[i] & 2) != 0)
						text += "1";
					else
						text += "0";

					if((data[i] & 1) != 0)
						text += "1";
					else
						text += "0";

					text += ")";
					input.text += text;
				}

				scrollRect.verticalNormalizedPosition = 0;
			}
		}
	}

	private void OnSerialOpened()
	{
		output.text = "";
		input.text = "";
	}

	private void OnClearClick()
	{
		input.text = "";
		scrollRect.verticalNormalizedPosition = 1;
	}

	private void OnCommandSend(CommandData sender, byte value)
	{
		if(_serial.IsOpen)
		{
			_serial.Write(new byte[] { value });
		}
	}
}
