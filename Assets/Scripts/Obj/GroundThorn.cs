using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// 地刺 -> 触碰扣血
public class GroundThorn : ObjBase
{
	public int m_damage;

	protected override void ConcreteAction()
	{
		Debug.Log("玩家掉血");
		m_player.Hurt(m_damage);
	}
}
