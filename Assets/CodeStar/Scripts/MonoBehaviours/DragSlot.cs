using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
#if PLAYMAKER
using HutongGames.PlayMaker;
#endif

[RequireComponent(typeof(RectTransform))]
public class DragSlot : MonoBehaviour, IDropHandler
{
	#if PLAYMAKER
	public PlayMakerFSM targetFSM;
	#endif

	private DragItem _item;

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
		if(_item == null)
			return;

		#if PLAYMAKER
		if(targetFSM != null)
		{
			FsmEventTarget fsmEventTarget = new FsmEventTarget();
			fsmEventTarget.target = FsmEventTarget.EventTarget.FSMComponent;
			fsmEventTarget.fsmComponent = targetFSM;

			targetFSM.Fsm.Event(fsmEventTarget, _item.eventName);
		}
		#endif

		_item.gameObject.SetActive(false);
		_item = null;
	}

	public void SetDropItem(DragItem item)
	{
		_item = item;
	}
}
