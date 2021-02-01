using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//CEILING_THORN,  // 天花板刺 -> 钉刺掉落，触碰扣血
public class CeilingThorn : ObjBase
{
	public GameObject thornPrefab;
	public Transform m_thornOffset;

	private GameObject m_thorn;
	private Rigidbody2D m_rigid;

	protected override void Init()
	{
		CreatThorn();
	}

	private void CreatThorn()
	{
		m_thorn = Instantiate(thornPrefab, m_thornOffset.position, m_thornOffset.rotation);
		m_thorn.transform.SetParent(m_thornOffset);
		m_rigid = m_thorn.GetComponent<Rigidbody2D>();
		m_rigid.gravityScale = 0;
	}

	protected override void ConcreteAction()
	{
		Debug.Log("天花板刺掉落");
		m_rigid.gravityScale = 1;
		base.ConcreteAction();
		StartAction();
	}

	protected override void ResetAction()
	{
		Debug.Log("复原");
		// Destroy(m_thorn);
		CreatThorn();
		base.ResetAction();
	}
}
