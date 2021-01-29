using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 地刺 -> 触碰扣血
public class GroundThorn : ObjBase
{
	protected override void ConcreteAction()
	{
		Debug.Log("玩家掉血");
	}
}
