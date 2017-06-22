using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Makist.IO;


public class NetworkApp : NetworkBehaviour
{
	public GameObject ui;
	public Button buttonA;
	public Button buttonB;
	public Button buttonC;

	private NetworkAppManager _manager;

	void Awake()
	{
		buttonA.onClick.AddListener(OnButtonAClick);
		buttonB.onClick.AddListener(OnButtonBClick);
		buttonC.onClick.AddListener(OnButtonCClick);
	}


	// Use this for initialization
	void Start ()
	{
		_manager = FindObjectOfType<NetworkAppManager>();

		if(isServer)
			ui.gameObject.SetActive(true);
		else
		{
			if(isLocalPlayer)
				ui.gameObject.SetActive(true);
			else
				ui.gameObject.SetActive(false);
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	[Server]
	private void DoLocalAction(int num)
	{
		_manager.DoAction(num);
	}

	[Command]
	private void CmdDoRemoteAction(int num)
	{
		_manager.DoAction(num);
	}

	private void OnButtonAClick()
	{
		if(isServer)
			DoLocalAction(1);
		else
			CmdDoRemoteAction(1);
	}

	private void OnButtonBClick()
	{
		if(isServer)
			DoLocalAction(2);
		else
			CmdDoRemoteAction(2);
	}

	private void OnButtonCClick()
	{
		if(isServer)
			DoLocalAction(3);
		else
			CmdDoRemoteAction(3);
	}
}
