using UnityEngine;

public class singleton : MonoBehaviour
{
    private static singleton _instance;
    /*
    public static singleton Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<singleton>();
            }

            return _instance;
        }
    }
    */
    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject); 
    }
}
