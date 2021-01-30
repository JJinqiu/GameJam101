using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ACTION_TYPE
{
    TRIGGER_SUDDEN, // 瞬时触发器 -> 触碰即触发（一次）
    TRIGGER_PRESS,  // 按压触发器 -> 按压状态持续触发功能
    TRIGGER_DELAY,  // 延迟触发器

    ACTION_START,     // 物体行为 
    ACTION_RESET,      // 重置物体
}

public enum ACTION_STATE
{
	STATE_ONE,
	STATE_TWO,
	STATE_THREE,
}

public abstract class ObjBase : MonoBehaviour
{
	public List<ACTION_TYPE> actions = new List<ACTION_TYPE>();
    public List<float> delayTime = new List<float>();

    protected int indexOfAction;
	protected int indexOfTime;

	protected PlayerController m_player;

	// 防止多次触发
	protected bool isChangingState;

	protected void Start()
    {
        indexOfAction = 0;
        indexOfTime = 0;
		isChangingState = false;
		Init();
    }

	// 特殊初始化需要
	protected virtual void Init()
	{
	}

	public void EnterAction(PlayerController player)
	{
		if (isChangingState && actions.Count > 1)
			return;

		isChangingState = true;
		m_player = player;
		StartAction();
	}

	public void ExitAction(PlayerController player)
	{
		m_player = player;
		ResetAction();
	}

	public void StartAction()
    {
        if (indexOfAction >= actions.Count)
            return;	

		switch (actions[indexOfAction])
        {
            case ACTION_TYPE.TRIGGER_SUDDEN:
                indexOfAction++;
                StartAction();
                break;
            case ACTION_TYPE.TRIGGER_DELAY:
                if (indexOfTime >= delayTime.Count)
                    return;
                indexOfAction++;
                Invoke("StartAction", delayTime[indexOfTime]);
                indexOfTime++;
                break;
            case ACTION_TYPE.ACTION_START:
                ConcreteAction();
                break; 
            case ACTION_TYPE.ACTION_RESET:
                ResetAction(); 
                break;
            default:
                break;
        }
    }

	protected virtual void ConcreteAction()
    {
        indexOfAction++;
    }

	protected virtual void ResetAction()
    {
        indexOfAction = 0;
        indexOfTime = 0;
		isChangingState = false;

	}
}
