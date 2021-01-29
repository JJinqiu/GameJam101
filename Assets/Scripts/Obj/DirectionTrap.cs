using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//方向陷阱 -> 角色控制方向反向，5s后停止
public class DirectionTrap : ObjBase
{
	protected override void ConcreteAction()
	{
		Debug.Log("控制角色反向");
		base.ConcreteAction();
		StartAction();
	}

	protected override void ResetAction()
	{
		Debug.Log("恢复角色方向");
		base.ResetAction();
	}
}
