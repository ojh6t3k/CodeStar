using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using System.IO;
#if PLAYMAKER
using HutongGames.PlayMaker;
#endif

[RequireComponent(typeof(VideoPlayer))]
public class VideoTest : MonoBehaviour
{
	public string fileName = "";

	private VideoPlayer _player;
	private double _time;

	#if PLAYMAKER
	public string playMakerEventName = "VIDEO COMPLETED";
	private PlayMakerFSM _fsm;
	#endif

	void Awake()
	{
		_player = GetComponent<VideoPlayer>();

		if(Application.isEditor)
			_player.url = Path.Combine(Directory.GetParent(Application.dataPath).FullName, Path.Combine("Build/macOS/UITest", fileName));
		else
		{
			if(Application.platform == RuntimePlatform.OSXPlayer)
			{
				string path = Directory.GetParent(Application.dataPath).FullName;
				path = Directory.GetParent(path).FullName;
				_player.url = Path.Combine(path, fileName);
			}
			else if(Application.platform == RuntimePlatform.WindowsPlayer)
				_player.url = Path.Combine(Directory.GetParent(Application.dataPath).FullName, fileName);
		}

		_player.prepareCompleted += PreparedCompleted;
		_player.Prepare();

		#if PLAYMAKER
		_fsm = FindObjectOfType<PlayMakerFSM>();
		#endif
	}

	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{		
		if(_player.isPlaying)
		{
			if(_player.time >= _time)
			{
				_player.Pause();
				#if PLAYMAKER
				if(_fsm != null)
				{
					FsmEventTarget fsmEventTarget = new FsmEventTarget();
					fsmEventTarget.target = FsmEventTarget.EventTarget.BroadcastAll;
					fsmEventTarget.excludeSelf = false;

					_fsm.Fsm.Event(fsmEventTarget, playMakerEventName);
				}
				#endif
			}
		}
	}

	private void PreparedCompleted(VideoPlayer source)
	{
		_player.Pause();
	}

	public void Play(float start, float end)
	{
		_player.time = start;
		_time = end;
		_player.Play();
	}

	public void Play(float end)
	{
		_time = end;
		_player.Play();
	}
}
