using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CodingSystem : MonoBehaviour
{
	public CodeItem[] items;
	public CodeSlot[] slots;
	public Button uploadButton;
	public Slider timer;
	public int maxTime;
	public GameObject codePanel;
	public float effectSpeed = 1f;
	public AnimationCurve posCurve;
	public AnimationCurve scaleCurve;

	private CodeItem _dragItem;
	private bool _uploading = false;
	private GameObject _effectObject;
	private Vector3 _startPosition;
	private Vector3 _diffPosition;
	private Vector3 _startScale;
	private float _time;

	void Awake()
	{
		foreach(CodeItem item in items)
			item.OnBeginDragItem.AddListener(OnBeginDragItem);

		foreach(CodeSlot slot in slots)
		{
			slot.OnDropItem.AddListener(OnDropItem);
			slot.OnClearItem.AddListener(OnClearItem);
		}

		uploadButton.onClick.AddListener(OnUploadClick);
	}

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(_uploading == true)
		{
			_time += Time.deltaTime * effectSpeed;
			if(_time > 1f)
			{
				GameObject.DestroyImmediate(_effectObject);
				_effectObject = null;

				bool correct = true;
				foreach(CodeSlot s in slots)
				{
					if(!s.IsCorrect)
					{
						correct = false;
						break;
					}
				}
				if(correct)
					Debug.Log("Correct");
				else
					Debug.Log("Incorrect");

				_time = 0f;
				_uploading = false;
			}
			else
			{
				_effectObject.transform.position = _startPosition + _diffPosition * posCurve.Evaluate(_time);
				_effectObject.transform.localScale = _startScale * scaleCurve.Evaluate(_time);
			}
		}
		else if(_time > 0f)
		{
			_time -= Time.deltaTime;
			timer.value = _time / (float)maxTime;
			if(_time <= 0f)
			{
				Debug.Log("Timeout");
			}
		}
	}

	void OnEnable()
	{
		uploadButton.interactable = false;
		_uploading = false;
		codePanel.SetActive(true);
		if(_effectObject != null)
		{
			GameObject.DestroyImmediate(_effectObject);
			_effectObject = null;
		}
		_time = (float)maxTime;
		timer.value = 1f;
	}

	private void OnBeginDragItem(CodeItem item)
	{
		_dragItem = item;
	}

	private void OnDropItem(CodeSlot slot)
	{
		if(slot.InsertItem(_dragItem))
			_dragItem.OkDrop();

		bool complete = true;
		foreach(CodeSlot s in slots)
		{
			if(s.dropItem == null)
			{
				complete = false;
				break;
			}
		}
		uploadButton.interactable = complete;
	}

	private void OnClearItem(CodeSlot slot)
	{
		slot.ClearItem();
		uploadButton.interactable = false;
	}

	private void OnUploadClick()
	{
		if(gameObject.activeInHierarchy)
		{
			uploadButton.interactable = false;

			_effectObject = GameObject.Instantiate(codePanel, codePanel.transform.parent);
			_startPosition = _effectObject.transform.position;
			_startScale = _effectObject.transform.localScale;
			_diffPosition = uploadButton.transform.position - _startPosition;
			_time = 0f;
			codePanel.SetActive(false);
			_uploading = true;
		}
	}
}
