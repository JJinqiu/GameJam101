using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : ObjBase
{
	public GameObject m_door;
	public float m_delayTime;

	private Animator m_anim;
	private bool m_isOpen;
	private float m_timeCount;

	protected override void Init()
	{
		m_anim = GetComponent<Animator>();
		m_anim.speed = 0;
		m_isOpen = false;
	}

	private void Update()
	{
		if (m_timeCount > 0)
		{
			m_timeCount -= Time.deltaTime;
			if (m_timeCount <= 0)
			{
				CloseDoor();
			}
		}
	}

	protected override void ConcreteAction()
	{
		if(m_delayTime > 0)
		{
			if (m_timeCount > 0)
			{
				m_timeCount = m_delayTime;
				return;
			}
			m_timeCount = m_delayTime;
			Debug.Log("开门");
			m_anim.speed = 0.5f;
			m_anim.Play("openDoor");
			m_isOpen = true;
		}
		else
		{
			if (m_player != null)
				return;

			if (m_isOpen)
			{
				if (m_isOpen == false)
					return;
				Debug.Log("关门");
				m_anim.speed = 0.5f;
				m_anim.Play("closeDoor");
				m_isOpen = false;
			}
			else
			{
				if (m_isOpen)
					return;
				Debug.Log("开门");
				m_anim.speed = 0.5f;
				m_anim.Play("openDoor");
				m_isOpen = true;
			}
		}
	}

	protected override void ResetAction()
	{
		Debug.Log("准备关门");
		m_timeCount = m_delayTime;
	}

	private void CloseDoor()
	{
		if (m_isOpen == false)
			return;
		Debug.Log("关门");
		m_anim.speed = 0.5f;
		m_anim.Play("closeDoor");
		m_isOpen = false;
	}

	public void SetDoorOpen()
	{
		m_isOpen = true;
	}

	public void SetDoorClose()
	{
		m_isOpen = false;
	}


	//public void AttackChangeDoorState()
	//{
	//	if (m_isOpen)
	//	{
	//		if (m_isOpen == false)
	//			return;
	//		Debug.Log("关门");
	//		m_anim.speed = 0.5f;
	//		m_anim.Play("closeDoor");
	//		m_isOpen = false;
	//	}
	//	else
	//	{
	//		if (m_isOpen)
	//			return;
	//		Debug.Log("开门");
	//		m_anim.speed = 0.5f;
	//		m_anim.Play("openDoor");
	//		m_isOpen = true;
	//	}
	//}
}
