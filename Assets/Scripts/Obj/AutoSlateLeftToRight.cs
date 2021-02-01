using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoSlateLeftToRight : MonoBehaviour
{
	public GameObject m_slate;
	public float m_offsetX;
	public float m_speed;

	private float m_initX;
	private float m_endX;

	private bool m_moveRight;

	private void Start()
	{
		m_initX = m_slate.transform.position.x;
		m_endX = m_initX + m_offsetX;

		if (m_initX < m_endX)
			m_moveRight = true;
		else
			m_moveRight = false;
	}

	void Update()
    {
		if (m_slate.transform.position.x > Mathf.Max(m_initX, m_endX))
			m_moveRight = false;
		if (m_slate.transform.position.x < Mathf.Min(m_initX, m_endX))
			m_moveRight = true;

		if (m_moveRight)
			m_slate.transform.position = new Vector2(transform.position.x + m_speed * Time.deltaTime, m_slate.transform.position.y);
		else
			m_slate.transform.position = new Vector2(transform.position.x - m_speed * Time.deltaTime, m_slate.transform.position.y);
	}
}
