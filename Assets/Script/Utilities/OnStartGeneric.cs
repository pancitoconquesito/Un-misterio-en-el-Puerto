using UnityEngine;
using UnityEngine.Events;
public class OnStartGeneric : MonoBehaviour
{
    public UnityEvent OnStart;
    void Start()
    {
        OnStart.Invoke();
    }
}
