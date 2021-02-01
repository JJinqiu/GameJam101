using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 碎裂的石板 -> 等待一段时间后，破坏自身，而后复原
public class CrackedSlate : ObjBase
{
	public GameObject slatePrefab;
	public BoxCollider2D m_trigger;
	private GameObject m_slate;
	private ACTION_STATE m_state;

	protected override void Init()
	{
		m_state = ACTION_STATE.STATE_ONE;
		CreatSlate();
	}

	private void CreatSlate()
	{
		m_slate = Instantiate(slatePrefab, this.gameObject.transform.position, this.gameObject.transform.rotation);
		m_slate.transform.SetParent(this.gameObject.transform);
	}

	protected override void ConcreteAction()
	{
		switch(m_state)
		{
			case ACTION_STATE.STATE_ONE:
				StateOneAction();
				break;
			case ACTION_STATE.STATE_TWO:
				StateTwoAction();
				break;
			case ACTION_STATE.STATE_THREE:
				StateThreeAction();
				break;
			default:
				break;
		}
	}

	private void StateOneAction()
	{
		m_state++;
		Debug.Log("播放动画");
		base.ConcreteAction();
		StartAction();
	}

	private void StateTwoAction()
	{
		m_state++;
		Debug.Log("破坏自身");
		Destroy(m_slate);
		m_trigger.enabled = false;
		base.ConcreteAction();
		StartAction();
	}

	private void StateThreeAction()
	{
		ResetAction();
	}

	protected override void ResetAction()
	{
		Debug.Log("复原");
		m_state = ACTION_STATE.STATE_ONE;
		CreatSlate();
		m_trigger.enabled = true;
		base.ResetAction();
	}
}
