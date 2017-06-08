#if PLAYMAKER
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("CodeStar")]
	public class GetDropItem : FsmStateAction
	{
		[RequiredField]
		public DragSlot slot;
		[UIHint(UIHint.Variable)]
		public FsmInt id;

		public override void Reset()
		{
			slot = null;
			id = new FsmInt { UseVariable = true };
		}

		public override void OnEnter()
		{
			base.OnEnter();

			if(slot != null)
			{
				DragItem item = slot.dropItem;
				if(item != null)
				{
					if(id != null)
						id.Value = item.id;
				}
			}
		}
	}
}
#endif