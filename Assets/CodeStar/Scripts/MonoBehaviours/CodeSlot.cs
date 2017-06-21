using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;


[Serializable]
public class CodeSlotEvent : UnityEvent<CodeSlot> {}

public class CodeSlot : MonoBehaviour, IDropHandler
{
	public CodeItem[] droptableItems;
	public CodeItem correctItem;
	public Image targetGraphic;
	public Button cancelButton;

	[HideInInspector]
	public CodeSlotEvent OnDropItem;
	[HideInInspector]
	public CodeSlotEvent OnClearItem;

	private CodeItem _item;

	void Awake()
	{
		cancelButton.onClick.AddListener(OnCancelClick);
	}

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	void OnEnable()
	{
		_item = null;
		targetGraphic.enabled = false;
		cancelButton.gameObject.SetActive(false);
	}

	public void OnDrop(PointerEventData eventData)
	{
		OnDropItem.Invoke(this);
	}

	private void OnCancelClick()
	{
		OnClearItem.Invoke(this);
	}

	public bool InsertItem(CodeItem item)
	{
		if(_item != null)
			return false;
		
		bool find = false;
		foreach(CodeItem i in droptableItems)
		{
			if(i.Equals(item))
			{
				find = true;
				break;
			}
		}

		if(!find)
			return false;

		_item = item;
		targetGraphic.sprite = _item.sprite;
		targetGraphic.enabled = true;
		cancelButton.gameObject.SetActive(true);
		return true;
	}

	public void ClearItem()
	{
		_item.CancelDrop();
		_item = null;
		targetGraphic.enabled = false;
		cancelButton.gameObject.SetActive(false);
	}

	public CodeItem dropItem
	{
		get
		{
			return _item;
		}
	}

	public bool IsCorrect
	{
		get
		{
			if(_item == null)
				return false;

			if(_item.Equals(correctItem))
				return true;
			else
				return false;
		}
	}
}
