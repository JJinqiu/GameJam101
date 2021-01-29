using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//CEILING_THORN,  // 天花板刺 -> 钉刺掉落，触碰扣血
public class CeilingThorn : ObjBase
{
	public GameObject m_thorn;

	protected override void ConcreteAction()
	{
		Debug.Log("天花板刺掉落");
		base.ConcreteAction();
		StartAction();
	}

	protected override void ResetAction()
	{
		Debug.Log("复原");
		base.ResetAction();
	}
}
