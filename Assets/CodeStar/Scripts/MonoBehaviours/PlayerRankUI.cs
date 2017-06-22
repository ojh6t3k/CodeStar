using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerRankUI : MonoBehaviour
{
	public Text order;
	public Image icon;
	public Text lapTime;
	public Text exp;
	public Text name;

	public int orderValue;
	public Sprite iconImage;
	public int lapTimeValue;
	public int expValue;
	public string nameValue;

	// Use this for initialization
	void Start ()
	{
		Refresh();
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	public void Refresh()
	{
		order.text = orderValue.ToString();
		if(iconImage != null)
			icon.sprite = iconImage;
		lapTime.text = TimeUI.ToTimeString(lapTimeValue);
		exp.text = ScoreUI.ToScoreString(expValue);
		name.text = nameValue;
	}
}
