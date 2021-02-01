using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Station : ObjBase
{
    public bool m_isFirstStation;
    public bool m_isSpecial;
    public GlobalController m_globalController;
    public static int Level = 0;

    protected override void ConcreteAction()
    {
        if (m_isSpecial)
        {
            Debug.Log("特殊补给站，获得技能");
            if (m_isFirstStation)
            {
                m_player.PowerUp1();
                Level = 1;
            }
            else
            {
                m_player.PowerUp2();
                Level = 2;
            }
        }

        m_globalController.Recover();
        m_globalController.SetRevivePosition(gameObject.transform.position);
    }
}