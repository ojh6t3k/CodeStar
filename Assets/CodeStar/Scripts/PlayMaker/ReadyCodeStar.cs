#if PLAYMAKER
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("CodeStar")]
	public class ReadyCodeStar : FsmStateAction
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
				demo.ReadyCodeStar();
			}
		}
	}
}
#endif