using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Makist.IO;


public class SettingUI : MonoBehaviour
{
	public WebCamManager webCamManager;
	public Dropdown webCamList;
	public Button webCamConnect;
	public RawImage webCamPreview;
	public CommSerial codeStarComm;
	public Dropdown codeStarPort;
	public Button codeStarConnect;
	public Button codeStarDisconnect;
	public Button finish;

	public UnityEvent OnSettingFinished;

	void Awake()
	{
		webCamConnect.onClick.AddListener(OnWebCamConnect);

		codeStarComm.OnOpen.AddListener(OnCodeStarSerialOpen);
		codeStarComm.OnClose.AddListener(OnCodeStarSerialClose);
		codeStarComm.OnOpenFailed.AddListener(OnCodeStarSerialOpenFailed);
		codeStarComm.OnErrorClosed.AddListener(OnCodeStarSerialErrorClosed);
		codeStarComm.OnStartSearch.AddListener(OnCodeStarSerialStartSearch);
		codeStarComm.OnFoundDevice.AddListener(OnCodeStarSerialFoundDevice);
		codeStarComm.OnStopSearch.AddListener(OnCodeStarSerialStopSearch);

		codeStarConnect.onClick.AddListener(OnCodeStarConnect);
		codeStarDisconnect.onClick.AddListener(OnCodeStarDisconnect);

		finish.onClick.AddListener(OnSettingFinish);
	}

	// Use this for initialization
	void Start()
	{
		
	}

	void OnEnable()
	{
		webCamList.ClearOptions();
		WebCamDevice[] devices = WebCamTexture.devices;
		List<string> nameList = new List<string>();
		for(int i = 0; i < devices.Length; i++)
		{
			nameList.Add(devices[i].name);
		}
		webCamList.AddOptions(nameList);
		webCamList.value = 0;
		webCamManager.Play(webCamPreview);

		if(codeStarComm.IsOpen)
		{
			codeStarPort.interactable = false;
			codeStarConnect.gameObject.SetActive(false);
			codeStarDisconnect.gameObject.SetActive(true);
		}
		else
		{
			codeStarPort.interactable = true;
			codeStarConnect.gameObject.SetActive(true);
			codeStarDisconnect.gameObject.SetActive(false);
		}
		codeStarPort.options.Clear();
		codeStarComm.StartSearch();
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	private void OnCodeStarSerialOpen()
	{
		codeStarPort.interactable = false;
		codeStarConnect.gameObject.SetActive(false);
		codeStarDisconnect.gameObject.SetActive(true);
	}

	private void OnCodeStarSerialClose()
	{
		codeStarPort.interactable = true;
		codeStarConnect.gameObject.SetActive(true);
		codeStarDisconnect.gameObject.SetActive(false);
	}

	private void OnCodeStarSerialOpenFailed()
	{
	}

	private void OnCodeStarSerialErrorClosed()
	{
	}

	private void OnCodeStarSerialStartSearch()
	{
		
	}

	private void OnCodeStarSerialFoundDevice(CommDevice device)
	{
		Dropdown.OptionData item = new Dropdown.OptionData();
		item.text = device.name;
		codeStarPort.options.Add(item);
	}

	private void OnCodeStarSerialStopSearch()
	{
		for(int i=0; i<codeStarComm.foundDevices.Count; i++)
		{
			if(codeStarComm.device.Equals(codeStarComm.foundDevices[i]))
			{
				if(codeStarPort.value == i)
					codeStarPort.captionText.text = codeStarComm.device.name;
				else
					codeStarPort.value = i;

				return;
			}
		}

		if(codeStarComm.foundDevices.Count > 0)
			codeStarPort.captionText.text = codeStarPort.options[0].text;
		else
		{
			codeStarPort.captionText.text = "";
		}
	}

	private void OnCodeStarConnect()
	{
		if(codeStarPort.options.Count > 0)
			codeStarComm.device = new CommDevice(codeStarComm.foundDevices[codeStarPort.value]);
		
		codeStarComm.Open();
	}

	private void OnCodeStarDisconnect()
	{
		codeStarComm.Close();
	}

	private void OnWebCamConnect()
	{
		webCamManager.Play(webCamList.options[webCamList.value].text, webCamPreview);
	}

	private void OnSettingFinish()
	{
		gameObject.SetActive(false);
		webCamManager.Stop();
		OnSettingFinished.Invoke();
	}
}
