using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

[Serializable]
public class CommandEvent : UnityEvent<CommandData, byte> {}

public class CommandData : MonoBehaviour
{
	public bool fixedCommand = true;
	public bool fixedData = true;
	public int command;
	public int data;
	public Dropdown dropDown;
	public InputField inputField;
	public Text output;
	public Button button;

	public CommandEvent OnCommandSend;


	void Awake()
	{
		button.onClick.AddListener(OnButtonClick);
	}

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	public byte value
	{
		get
		{
			byte result = 0;

			if(fixedCommand)
				result = (byte)(command << 4);
			else
			{
				if(inputField != null)
				{
					if(inputField.text.Length == 8)
					{
						for(int i=0; i<4; i++)
						{
							if(inputField.text[i] == '1')
								result += (byte)Mathf.Pow(2, 7 - i);
						}
					}
				}
			}

			if(fixedData)
				result += (byte)(data & 0xFF);
			else
			{
				if(dropDown != null)
					result += (byte)(dropDown.value & 0xFF);
				else if(inputField != null)
				{
					if(inputField.text.Length == 8)
					{
						for(int i=4; i<8; i++)
						{
							if(inputField.text[i] == '1')
								result += (byte)Mathf.Pow(2, 7 - i);
						}
					}
				}
			}

			return result;
		}
	}

	private void OnButtonClick()
	{
		byte result = value;

		if(output != null)
		{
			string text = "";
			if((result & 128) != 0)
				text += "1";
			else
				text += "0";
			
			if((result & 64) != 0)
				text += "1";
			else
				text += "0";
			
			if((result & 32) != 0)
				text += "1";
			else
				text += "0";
			
			if((result & 16) != 0)
				text += "1";
			else
				text += "0";

			if((result & 8) != 0)
				text += "1";
			else
				text += "0";

			if((result & 4) != 0)
				text += "1";
			else
				text += "0";

			if((result & 2) != 0)
				text += "1";
			else
				text += "0";

			if((result & 1) != 0)
				text += "1";
			else
				text += "0";

			output.text = text;
		}

		if(OnCommandSend != null)
			OnCommandSend.Invoke(this, result);
	}
}
