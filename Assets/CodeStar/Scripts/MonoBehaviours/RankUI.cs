using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Makist.Input;



public class RankUI : MonoBehaviour
{
	public GameObject register;
	public GameObject rankingBoard;
	public RectTransform itemRoot;
	public PlayerRankUI rankItem;
	public ScoreUI scoreUI;
	public TimeUI timeUI;
	public Text resultTime;
	public Text resultExp;
	public InputField playerName;
	public VirtualKeyboard keyboard;


	private List<PlayerRankUI> _players;

	void Awake()
	{
		_players = new List<PlayerRankUI>();
		keyboard.OnSubmit.AddListener(OnSubmitPlayerName);
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
		register.SetActive(true);
		rankingBoard.SetActive(false);

		resultTime.text = TimeUI.ToTimeString(timeUI.lapTimeValue);
		resultExp.text = ScoreUI.ToScoreString(scoreUI.expValue);
	}

	private void OnSubmitPlayerName()
	{
		PlayerRankUI newPlayer = GameObject.Instantiate(rankItem, itemRoot);
		newPlayer.gameObject.SetActive(true);
		newPlayer.nameValue = playerName.text;
		newPlayer.lapTimeValue = timeUI.lapTimeValue;
		newPlayer.expValue = scoreUI.expValue;

		bool insert = false;
		for(int i = 0; i < _players.Count; i++)
		{
			if(newPlayer.expValue > _players[i].expValue)
			{
				_players.Insert(i, newPlayer);
				insert = true;
				break;
			}
		}

		if(!insert)
			_players.Add(newPlayer);

		for(int i = 0; i < _players.Count; i++)
		{
			_players[i].orderValue = i + 1;
			_players[i].transform.SetSiblingIndex(i);
			_players[i].Refresh();
		}

		timeUI.bestTimeValue = _players[0].lapTimeValue;

		register.SetActive(false);
		rankingBoard.SetActive(true);
	}
}
