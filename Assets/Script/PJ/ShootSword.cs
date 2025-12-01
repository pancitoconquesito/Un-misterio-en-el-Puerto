using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootSword : MonoBehaviour
{
    [SerializeField] private ObjectPooling particleSword_GO;
    [SerializeField]private changeMirada m_changeMirada;
    private GameObject padreParticles;
    private void Awake()
    {
        padreParticles = particleSword_GO.transform.parent.gameObject;
    }
    public void _EmitirParticleSword_FRONTAL()
    {
        if (m_changeMirada.getMirada()==GLOBAL_TYPE.LADO.iz)
        {
            _EmitirParticleSword_LEFT();
        }
        else
        {
            _EmitirParticleSword_RIGHT();
        }

    }
    public void _EmitirParticleSword_UP()
    {
        GameObject particleObj = particleSword_GO.emitirObj(0.8f, padreParticles.transform.position, false, true);
        particleObj.transform.rotation = Quaternion.Euler(new Vector3(0,0,90));
    }
    public void _EmitirParticleSword_DOWN()
    {
        GameObject particleObj = particleSword_GO.emitirObj(0.8f, padreParticles.transform.position, false, true);
        particleObj.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -90));
    }
    void _EmitirParticleSword_LEFT()
    {
        GameObject particleObj = particleSword_GO.emitirObj(0.8f, padreParticles.transform.position, false, true);
        particleObj.transform.rotation = Quaternion.Euler(new Vector3(180, 0, 180));
    }
    void _EmitirParticleSword_RIGHT()
    {
        GameObject particleObj = particleSword_GO.emitirObj(0.8f, padreParticles.transform.position, false, true);
        particleObj.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
    }
    void startSword( float valorY)
    {
       
    }
}
