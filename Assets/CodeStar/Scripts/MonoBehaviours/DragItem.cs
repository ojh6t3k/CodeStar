using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
public class DragItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	public int id;
	public Canvas rootCanvas;
	public Transform dragRoot;
	public DragSlot[] targetSlots;
	public bool slotReplacable = false;

	private Transform _startParent;
	private Vector3 _startPosition;
	private DragSlot _parentSlot;
	private Vector3 _slotPosition;
	private bool _dragging = false;
	private Transform _parent;
	private Vector3 _position;

	void Awake()
	{
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

	public void OnBeginDrag (PointerEventData eventData)
	{
		if(_parentSlot != null && !slotReplacable)
			return;

		if(_parentSlot != null)
		{
			_parent = _parentSlot.transform;
			_position = _slotPosition;
		}
		else
		{
			_parent = _startParent;
			_position = _startPosition;
		}

		foreach(DragSlot slot in targetSlots)
			slot.dragItem = this;

		_dragging = true;
		transform.SetParent(dragRoot);
	}

	public void OnDrag (PointerEventData eventData)
	{
		if(!_dragging)
			return;
		
		Vector3 dragPosition = Input.mousePosition;

		if(rootCanvas.renderMode == RenderMode.ScreenSpaceCamera)
		{
			dragPosition.z = rootCanvas.planeDistance;
			dragPosition = Camera.main.ScreenToWorldPoint(dragPosition);
		}

		transform.position = dragPosition;
	}

	public void OnEndDrag (PointerEventData eventData)
	{
		transform.SetParent(_parent);
		transform.position = _position;

		foreach(DragSlot slot in targetSlots)
			slot.dragItem = null;

		_dragging = false;
	}

	public DragSlot dragSlot
	{
		set
		{
			_parentSlot = value;

			if(_parentSlot != null)
			{
				_slotPosition = _parentSlot.transform.position;
				transform.SetParent(_parentSlot.transform);
				transform.position = _slotPosition;
			}
			else
			{
				transform.SetParent(_startParent);
				transform.position = _startPosition;
			}
		}
		get
		{
			return _parentSlot;
		}
	}

	public void DropSlot(DragSlot slot)
	{
		if(slot == null)
		{
			_parent = _startParent;
			_position = _startPosition;
		}
		else
		{
			_parentSlot = slot;

			_parent = _parentSlot.transform;
			_slotPosition = _parentSlot.transform.position;
			_position = _slotPosition;
		}
	}
}
