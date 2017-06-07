using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
public class DragItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	public Canvas rootCanvas;
	public Transform dragRoot;
	public DragSlot[] targetSlots;
	#if PLAYMAKER
	public string eventName = "DROP ITEM";
	#endif

	private RectTransform _rectTransform;
	private Transform _parent;
	private Vector2 _startPosition;
	private Vector3 _dragPosition;

	void Awake()
	{
		_rectTransform = GetComponent<RectTransform>();
		_startPosition = _rectTransform.anchoredPosition;
	}

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	public void OnBeginDrag (PointerEventData eventData)
	{
		_parent = transform.parent;
		_rectTransform.SetParent(dragRoot);

		foreach(DragSlot slot in targetSlots)
			slot.SetDropItem(this);
	}

	public void OnDrag (PointerEventData eventData)
	{
		_dragPosition = Input.mousePosition;

		if(rootCanvas.renderMode == RenderMode.ScreenSpaceCamera)
		{
			_dragPosition.z = rootCanvas.planeDistance;
			_dragPosition = Camera.main.ScreenToWorldPoint(_dragPosition);
		}
		
		transform.position = _dragPosition;
	}

	public void OnEndDrag (PointerEventData eventData)
	{
		_rectTransform.SetParent(_parent);
		_rectTransform.anchoredPosition = _startPosition;
	}
}
