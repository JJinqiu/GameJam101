using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 命令石板 -> 初始静止，触发移动，离开回到初始位置
public class CommandSlate : ObjBase
{
	public GameObject m_slate;
	public float speed;

	private bool m_isMoving;
	private bool m_isReset;

	protected override void Init()
	{
		m_isMoving = false;
		m_isReset = false;
	}

	private void Update()
	{
		if (m_isMoving == false && m_isReset == false)
			return;
	}

	private void Move()
	{
		
	}


	protected override void ConcreteAction()
	{
		Debug.Log("开始移动");
		m_isMoving = true;
	}

	protected override void ResetAction()
	{
		Debug.Log("停止移动");
		m_isMoving = false;
	}
}
