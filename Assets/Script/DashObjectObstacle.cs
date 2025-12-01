using UnityEngine;
public class DashObjectObstacle : MonoBehaviour
{
    PlatformEffector2D m_Effector2D;
    DashInvencible m_DashInvencibleREF;
    Transform m_transformPlayer;
    movementPJ m_movementPJ;
    private void Awake()
    {
        m_Effector2D = GetComponent<PlatformEffector2D>();
    }

    void Start()
    {
        m_movementPJ = MASTER_REFERENCE.instance.MovementPJ;
        m_transformPlayer = m_movementPJ.gameObject.transform;
        m_DashInvencibleREF = m_movementPJ.DashInvencible;
    }

    void Update()
    {
        if (m_DashInvencibleREF.IsEnableAbility && m_movementPJ.GetState()== GLOBAL_TYPE.ESTADOS.dash)
        {
            m_Effector2D.useColliderMask=true;
            return;
        }
        m_Effector2D.useColliderMask = false;
        if (m_transformPlayer.position.x < transform.position.x)
        {
            m_Effector2D.rotationalOffset = 90;
        }
        else
        {
            m_Effector2D.rotationalOffset = -90;
        }
    }
}
