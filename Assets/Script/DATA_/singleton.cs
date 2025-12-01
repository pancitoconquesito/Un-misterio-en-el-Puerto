using System.Collections.Generic;
using UnityEngine;

public class singleton : MonoBehaviour
{
    private static Dictionary<string, singleton> _instances = new Dictionary<string, singleton>();
    public string id; // Identificador único de la instancia
    void Awake()
    {
        transform.parent = null;
        if (!string.IsNullOrEmpty(id))
        {
            if (_instances.ContainsKey(id))
            {
                Destroy(gameObject); // Destruye el objeto si ya hay una instancia con el mismo ID
            }
            else
            {
                _instances[id] = this;
                DontDestroyOnLoad(gameObject); // Mantiene la instancia al cambiar de escena
            }
        }
        else
        {
            Debug.LogError("Error: La instancia debe tener un ID asignado.");
            Destroy(gameObject); // Destruye el objeto si no tiene un ID válido
        }
    }

    public static singleton GetInstanceByID(string id)
    {
        _instances.TryGetValue(id, out singleton instance);
        return instance;
    }
    //private static singleton _instance;
    //void Awake()
    //{
    //    if (_instance == null)
    //    {
    //        _instance = this;
    //        DontDestroyOnLoad(gameObject);
    //    }
    //    else
    //        Destroy(gameObject); 
    //}
}
