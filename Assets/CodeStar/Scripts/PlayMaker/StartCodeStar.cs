#if PLAYMAKER
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("CodeStar")]
	public class StartCodeStar : FsmStateAction
	{
		[RequiredField]
		public CodeStarDemo demo;

		public override void Reset()
		{
			demo = null;
		}

		public override void OnEnter()
		{
			base.OnEnter();

			if(demo != null)
			{
				demo.StartCodeStar();
			}
		}
	}
}
#endif