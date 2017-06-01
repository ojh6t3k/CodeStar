using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Makist;


public class NetworkAppManager : NetworkManager
{
	public Toggle isServer;
	public Button start;
	public Button stop;
	public Text log;
	public Text ipAddress;
	public InputField serverIP;
	
	public SerialComm serial;
	public GameObject connectionUI;

	private int _clientNum = 0;


	void Awake()
	{
		isServer.onValueChanged.AddListener(OnIsServerClick);
		start.onClick.AddListener(OnStartClick);
		stop.onClick.AddListener(OnStopClick);
	}

	// Use this for initialization
	void Start ()
	{
		start.gameObject.SetActive(true);
		stop.gameObject.SetActive(false);
		ipAddress.gameObject.SetActive(true);
		serverIP.gameObject.SetActive(false);

		ipAddress.text = string.Format("IP: {0}", Network.player.ipAddress);
		serverIP.text = Network.player.ipAddress;
	}

	// Update is called once per frame
	void Update ()
	{

	}

	public override void OnStartServer()
	{
		base.OnStartServer();

		log.text = "Success to start server.";

		start.interactable = true;
		stop.interactable = true;
		start.gameObject.SetActive(false);
		stop.gameObject.SetActive(true);
	}

	public override void OnStopServer()
	{
		base.OnStopServer();

		log.text = "Success to stop server.";

		isServer.interactable = true;
		serverIP.interactable = true;
		start.interactable = true;
		stop.interactable = true;
		start.gameObject.SetActive(true);
		stop.gameObject.SetActive(false);
	}

	public override void OnStartClient(NetworkClient client)
	{
		base.OnStartClient(client);
	}

	public override void OnStopClient()
	{
		base.OnStopClient();

		log.text = "Success to stop client.";

		isServer.interactable = true;
		serverIP.interactable = true;
		start.interactable = true;
		stop.interactable = true;
		start.gameObject.SetActive(true);
		stop.gameObject.SetActive(false);
	}

	public override void OnClientConnect(NetworkConnection conn)
	{
		base.OnClientConnect(conn);

		log.text = "Success to start client.";

		start.interactable = true;
		stop.interactable = true;
		start.gameObject.SetActive(false);
		stop.gameObject.SetActive(true);
	}

	void OnClientDisconnected(NetworkMessage msg)
	{
		if(isServer.isOn)
		{
			log.text = string.Format("Client disconnected: current client {0:d}", --_clientNum);
		}
		else
		{
			log.text = "Failed to connect to server";

			isServer.interactable = true;
			serverIP.interactable = true;
			start.interactable = true;
			stop.interactable = true;
			start.gameObject.SetActive(true);
			stop.gameObject.SetActive(false);
		}
	}

	public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
	{
		base.OnServerAddPlayer(conn, playerControllerId);

		log.text = string.Format("New client connected: current client {0:d}", ++_clientNum);
		conn.RegisterHandler(MsgType.Disconnect, OnClientDisconnected);
	}

	private void OnIsServerClick(bool state)
	{
		ipAddress.gameObject.SetActive(state);
		serverIP.gameObject.SetActive(!state);

		if(connectionUI != null)
			connectionUI.SetActive(state);
	}

	private void OnStartClick()
	{
		isServer.interactable = false;
		serverIP.interactable = false;
		start.interactable = false;

		if(isServer.isOn)
		{
			log.text = "Starting Server...";
			StartServer();
		}
		else
		{
			log.text = "Starting Client...";
			networkAddress = serverIP.text;
			NetworkClient c = StartClient();
			c.RegisterHandler(MsgType.Disconnect, OnClientDisconnected);
		}
	}

	private void OnStopClick()
	{
		stop.interactable = false;

		if(isServer.isOn)
		{
			log.text = "Stopping Server...";
			StopServer();
		}
		else
		{
			log.text = "Stopping Client...";
			StopClient();
		}
	}

	public void DoAction(int num)
	{
		if(serial.IsOpen)
		{
			Debug.Log("Action " + num);
		}
	}
}