using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;


[Serializable]
public class CodeItemEvent : UnityEvent<CodeItem> {}


public class CodeItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	public int id;
	public Transform dragRoot;

	[HideInInspector]
	public CodeItemEvent OnBeginDragItem;

	private Image _image;
	private Transform _startParent;
	private Vector3 _startPosition;
	private bool _dragging = false;

	void Awake()
	{
		_image = GetComponent<Image>();
	}

	// Use this for initialization
	void Start ()
	{
		_startParent = transform.parent;
		_startPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update ()
	{		
	}

	void OnEnable()
	{
		_dragging = false;
		_image.enabled = true;
	}

	public void OnBeginDrag (PointerEventData eventData)
	{
		OnBeginDragItem.Invoke(this);

		_dragging = true;
		transform.SetParent(dragRoot);
	}

	public void OnDrag (PointerEventData eventData)
	{
		if(!_dragging)
			return;

		Vector3 dragPosition = Input.mousePosition;

		if(_image.canvas.renderMode == RenderMode.ScreenSpaceCamera)
		{
			dragPosition.z = _image.canvas.planeDistance;
			dragPosition = Camera.main.ScreenToWorldPoint(dragPosition);
		}

		transform.position = dragPosition;
	}

	public void OnEndDrag (PointerEventData eventData)
	{
		transform.SetParent(_startParent);
		transform.position = _startPosition;

		_dragging = false;
	}

	public Sprite sprite
	{
		get
		{
			return _image.sprite;
		}
	}

	public void OkDrop()
	{
		_image.enabled = false;
	}

	public void CancelDrop()
	{
		_image.enabled = true;
	}
}
