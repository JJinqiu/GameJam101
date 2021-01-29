using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 摇摆锤 -> 触碰触发，位移，有击退效果，有伤害
public class SwingHammer : ObjBase
{
	public GameObject m_hammer;

	protected override void ConcreteAction()
	{
		Debug.Log("开始摇摆");
	}

	protected override void ResetAction()
	{
		Debug.Log("停止摇摆");
	}
}
