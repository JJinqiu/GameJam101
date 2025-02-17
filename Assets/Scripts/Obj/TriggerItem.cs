﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 触发器
public class TriggerItem : MonoBehaviour
{
	public ObjBase enterTarget;
	public ObjBase exitTarget;


	public void ColEnter(PlayerController player = null)
	{
		if(player != null && enterTarget != null)
		{
			enterTarget.EnterAction(player);
		}

		if(player == null && enterTarget != null)
		{
			enterTarget.SpecialEnter();
		}
	}

	public void ColExit(PlayerController player)
	{
		if (player != null && exitTarget != null)
		{
			exitTarget.ExitAction(player);
		}
	}
}
