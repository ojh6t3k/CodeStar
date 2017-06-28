using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class WebCamManager : MonoBehaviour
{
	public CanvasScaler canvasScaler;
	private WebCamTexture _webCam;

	void Awake()
	{
		_webCam = new WebCamTexture();
		_webCam.requestedWidth = (int)canvasScaler.referenceResolution.x;
		_webCam.requestedHeight = (int)canvasScaler.referenceResolution.y;
		_webCam.requestedFPS = 30f;
	}

	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{
	}

	public void Play(string deviceName, RawImage rawImage)
	{
		rawImage.texture = _webCam;
		_webCam.deviceName = deviceName;
		_webCam.Play();
	}

	public void Play(RawImage rawImage)
	{
		try
		{
			rawImage.texture = _webCam;
			_webCam.Play();
		}
		catch(Exception)
		{
		}
	}

	public void Stop()
	{
		_webCam.Stop();
	}
}
