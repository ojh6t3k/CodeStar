#if PLAYMAKER
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("CodeStar")]
	public class DragSlotClear : FsmStateAction
	{
		[RequiredField]
		public DragSlot slot;

		public override void Reset()
		{
			slot = null;
		}

		public override void OnEnter()
		{
			base.OnEnter();

			if(slot != null)
			{
				slot.dropItem = null;
			}
		}
	}
}
#endif