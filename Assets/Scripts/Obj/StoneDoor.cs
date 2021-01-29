using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 石门 -> 触发后打开石门，时间到后自动关闭
public class StoneDoor : ObjBase
{
	public GameObject m_stoneDoor1;
	public GameObject m_stoneDoor2;

	protected override void ConcreteAction()
	{
		Debug.Log("打开石门");
		base.ConcreteAction();
		StartAction();
	}

	protected override void ResetAction()
	{
		Debug.Log("关闭石门");
		base.ResetAction();
	}
}
