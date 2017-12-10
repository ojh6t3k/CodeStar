using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Makist.IO;


public class ConnectionUI : MonoBehaviour
{
	public CommSerial commSerial;
	public Button connect;
	public Button disconnect;
	public Button quit;
	public Canvas popupCanvas;
	public RectTransform settingCommSocket;
	public Dropdown portList;
	public Button okCommSocket;
	public Button cancelCommSocket;
	public Canvas messageCanvas;
	public RectTransform msgConnecting;
	public RectTransform msgConnectionFailed;
	public RectTransform msgLostConnection;
	public Button okConnectionFailed;
	public Button okLostConnection;
	public GameObject appUI;


	void Awake()
	{
		commSerial.OnOpen.AddListener(OnSerialOpen);
		commSerial.OnClose.AddListener(OnSerialClose);
		commSerial.OnOpenFailed.AddListener(OnSerialOpenFailed);
		commSerial.OnErrorClosed.AddListener(OnSerialErrorClosed);
		commSerial.OnStartSearch.AddListener(OnSerialStartSearch);
		commSerial.OnFoundDevice.AddListener(OnSerialFoundDevice);
		commSerial.OnStopSearch.AddListener(OnSerialStopSearch);

		connect.onClick.AddListener(OnConnectClick);
		disconnect.onClick.AddListener(OnDisconnectClick);
		quit.onClick.AddListener(OnQuitClick);
		okConnectionFailed.onClick.AddListener(OnMessageOKClick);
		okLostConnection.onClick.AddListener(OnMessageOKClick);
		okCommSocket.onClick.AddListener(OnCommSocketOKClick);
		cancelCommSocket.onClick.AddListener(OnCommSocketCancelClick);
	}

	// Use this for initialization
	void Start ()
	{
		connect.gameObject.SetActive(true);
		disconnect.gameObject.SetActive(false);
		popupCanvas.gameObject.SetActive(false);
		settingCommSocket.gameObject.SetActive(false);
		messageCanvas.gameObject.SetActive(false);
		if(appUI != null)
			appUI.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	#region EventHandler
	private void OnSerialOpen()
	{
		disconnect.gameObject.SetActive(true);
		connect.gameObject.SetActive(false);
		messageCanvas.gameObject.SetActive(false);
		msgConnecting.gameObject.SetActive(false);
		msgConnectionFailed.gameObject.SetActive(false);
		msgLostConnection.gameObject.SetActive(false);
		if(appUI != null)
			appUI.gameObject.SetActive(true);
	}

	private void OnSerialClose()
	{
		disconnect.gameObject.SetActive(false);
		connect.gameObject.SetActive(true);
		if(appUI != null)
			appUI.gameObject.SetActive(false);
	}

	private void OnSerialOpenFailed()
	{
		messageCanvas.gameObject.SetActive(true);
		msgConnecting.gameObject.SetActive(false);
		msgConnectionFailed.gameObject.SetActive(true);
		msgLostConnection.gameObject.SetActive(false);
	}

	private void OnSerialErrorClosed()
	{
		messageCanvas.gameObject.SetActive(true);
		msgConnecting.gameObject.SetActive(false);
		msgConnectionFailed.gameObject.SetActive(false);
		msgLostConnection.gameObject.SetActive(true);
	}

	private void OnSerialStartSearch()
	{		
	}

	private void OnSerialStopSearch()
	{
		for(int i=0; i<commSerial.foundDevices.Count; i++)
		{
			if(commSerial.device.Equals(commSerial.foundDevices[i]))
			{
				if(portList.value == i)
					portList.captionText.text = commSerial.device.name;
				else
					portList.value = i;

				return;
			}
		}

		if(commSerial.foundDevices.Count > 0)
			portList.captionText.text = portList.options[0].text;
		else
		{
			portList.captionText.text = "";
			okCommSocket.interactable = false;
		}
	}

	private void OnSerialFoundDevice(CommDevice device)
	{
		Dropdown.OptionData item = new Dropdown.OptionData();
		item.text = device.name;
		portList.options.Add(item);
	}

	private void OnConnectClick()
	{
		popupCanvas.gameObject.SetActive(true);
		settingCommSocket.gameObject.SetActive(true);
		okCommSocket.interactable = true;
		portList.options.Clear();

		commSerial.StartSearch();
	}

	private void OnDisconnectClick()
	{
		commSerial.Close();
	}

	private void OnQuitClick()
	{
		commSerial.Close();
		Application.Quit();
	}

	private void OnMessageOKClick()
	{
		messageCanvas.gameObject.SetActive(false);
	}

	private void OnCommSocketOKClick()
	{
		if(portList.options.Count > 0)
			commSerial.device = new CommDevice(commSerial.foundDevices[portList.value]);

		commSerial.Open();

		popupCanvas.gameObject.SetActive(false);
		settingCommSocket.gameObject.SetActive(false);
		messageCanvas.gameObject.SetActive(true);
		msgConnecting.gameObject.SetActive(true);
		msgConnectionFailed.gameObject.SetActive(false);
		msgLostConnection.gameObject.SetActive(false);
	}

	private void OnCommSocketCancelClick()
	{
		popupCanvas.gameObject.SetActive(false);
		settingCommSocket.gameObject.SetActive(false);
	}
	#endregion
}
