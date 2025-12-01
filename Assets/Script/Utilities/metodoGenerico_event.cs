using UnityEngine;
using UnityEngine.Events;
public class metodoGenerico_event : MonoBehaviour
{
    public UnityEvent genericEvent;
    [SerializeField]public float m_delay;
    public void startEvent(float delay)
    {
        Invoke("ejecutarEvento", delay);
    }
    public void startEvent()
    {
        if (!Application.isEditor)
            Invoke("ejecutarEvento", m_delay);
        else
            ejecutarEvento();
    }
    public void ejecutarEvento()
    {
        print("Metodo ejecutado!");
        genericEvent.Invoke();
    }

}
