using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 报警陷阱 -> 仅触发一次
public class AlarmTrap : ObjBase
{
	public GameObject m_trap;

	private bool m_isTrigger;

	protected override void Init()
	{
		m_isTrigger = false;
	}

	protected override void ConcreteAction()
	{
		if(!m_isTrigger)
		{
			Debug.Log("报警");
			m_isTrigger = true;
		}
	}
}
