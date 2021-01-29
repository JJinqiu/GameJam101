using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 命令石板 -> 初始静止，触发移动，离开回到初始位置
public class CommandSlate : ObjBase
{
	public GameObject m_slate;

	protected override void ConcreteAction()
	{
		Debug.Log("开始移动");
	}

	protected override void ResetAction()
	{
		Debug.Log("停止移动");
	}
}
