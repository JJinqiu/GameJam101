using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : ObjBase
{
	public GameObject m_door1;
	public GameObject m_door2;

	protected override void ConcreteAction()
	{
		Debug.Log("开门");
	}

	protected override void ResetAction()
	{
		Debug.Log("关门");
	}
}
