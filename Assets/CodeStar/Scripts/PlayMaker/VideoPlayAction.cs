#if PLAYMAKER
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("CodeStar")]
	public class VideoPlayAction : FsmStateAction
	{
		[RequiredField]
		public VideoTest videoPlayer;
		public FsmFloat startTime;
		public FsmFloat endTime;

		public override void Reset()
		{
			videoPlayer = null;
			// default axis to variable dropdown with None selected.
			startTime = new FsmFloat { UseVariable = false };
			endTime = new FsmFloat { UseVariable = false };
		}

		public override void OnEnter()
		{
			base.OnEnter();

			if(videoPlayer != null)
			{
				if(!startTime.IsNone && !endTime.IsNone)
				{
					videoPlayer.Play(startTime.Value, endTime.Value);
				}
				else if(!endTime.IsNone)
					videoPlayer.Play(endTime.Value);
			}
		}
	}
}
#endif