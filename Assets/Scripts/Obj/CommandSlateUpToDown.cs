using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandSlateUpToDown : ObjBase
{
	public GameObject m_slate;
	public float m_offsetY;
	public float m_speed;

	private float m_initY;
	private float m_endY;

	private bool m_initMoveDir;
	private bool m_moveUp;

	private bool m_isReturn;
	private bool m_isMove;

	protected override void Init()
	{
		m_initY = m_slate.transform.position.y;
		m_endY = m_initY + m_offsetY;

		if (m_initY < m_endY)
			m_initMoveDir = true;
		else
			m_initMoveDir = false;
		m_isReturn = false;
	}

	protected override void ConcreteAction()
	{
		m_isMove = true;
		m_isReturn = false;
		m_moveUp = m_initMoveDir;
		base.ConcreteAction();
	}

	protected override void ResetAction()
	{
		m_isMove = false;
		m_isReturn = true;
		m_moveUp = !m_initMoveDir;
		base.ResetAction();
	}


	void Update()
    {
		if(m_isMove)
		{
			if (m_slate.transform.position.y > Mathf.Max(m_initY, m_endY))
				m_moveUp = false;
			if (m_slate.transform.position.y < Mathf.Min(m_initY, m_endY))
				m_moveUp = true;

			if (m_moveUp)
				m_slate.transform.position = new Vector2(transform.position.x, m_slate.transform.position.y + m_speed * Time.deltaTime);
			else
				m_slate.transform.position = new Vector2(transform.position.x, m_slate.transform.position.y - m_speed * Time.deltaTime);
		}

		if(m_isReturn)
		{
			if (m_moveUp)
			{
				m_slate.transform.position = new Vector2(transform.position.x, m_slate.transform.position.y + m_speed * Time.deltaTime);
				if (m_slate.transform.position.y >= m_initY)
					m_isReturn = false;
			}
				
			else
			{
				m_slate.transform.position = new Vector2(transform.position.x, m_slate.transform.position.y - m_speed * Time.deltaTime);
				if (m_slate.transform.position.y <= m_initY)
					m_isReturn = false;
			}
				
		}
	}
}
