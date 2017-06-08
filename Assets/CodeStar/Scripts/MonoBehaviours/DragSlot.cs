using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
#if PLAYMAKER
using HutongGames.PlayMaker;
#endif

[RequireComponent(typeof(RectTransform))]
public class DragSlot : MonoBehaviour, IDropHandler
{
	#if PLAYMAKER
	public PlayMakerFSM targetFSM;
	public string eventName = "DROP ITEM";
	#endif

	public bool itemReplacable = false;
	public UnityEvent OnDropItem;

	private DragItem _dragItem;
	private DragItem _dropItem;

	void Awake()
	{
	}

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	public void OnDrop(PointerEventData eventData)
	{
		if(_dragItem == null)
			return;

		if(_dropItem != null)
		{
			if(_dragItem != null && itemReplacable)
				_dropItem.dragSlot = _dragItem.dragSlot;
			else
				_dropItem.dragSlot = null;
		}

		_dropItem = _dragItem;
		_dropItem.DropSlot(this);

		#if PLAYMAKER
		if(targetFSM != null)
		{
			FsmEventTarget fsmEventTarget = new FsmEventTarget();
			fsmEventTarget.target = FsmEventTarget.EventTarget.FSMComponent;
			fsmEventTarget.fsmComponent = targetFSM;

			targetFSM.Fsm.Event(fsmEventTarget, eventName);
		}
		#endif

		OnDropItem.Invoke();
	}

	public DragItem dragItem
	{
		set
		{
			_dragItem = value;
		}
		get
		{
			return _dragItem;
		}
	}

	public DragItem dropItem
	{
		set
		{
			if(_dropItem != null)
			{
				if(value != null && itemReplacable)
					_dropItem.dragSlot = value.dragSlot;
				else
					_dropItem.dragSlot = null;
			}

			_dropItem = value;
			if(_dropItem != null)
				_dropItem.dragSlot = this;
		}
		get
		{
			return _dropItem;
		}
	}
}
