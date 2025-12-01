using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
public class StaminaOrbe : MonoBehaviour
{
    [SerializeField][Tag] string m_tagPJ;
    [SerializeField] float staminaValue;
    [SerializeField] float delayDestruir;
    [SerializeField] Animator anim;
    bool realizoSuAccion = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!realizoSuAccion && collision.CompareTag(m_tagPJ))
        {
            realizoSuAccion = true;
            staminaPsiquica stamina = collision.gameObject.GetComponent<movementPJ>().Stamina;
            stamina.addStamina(staminaValue);
            anim.SetTrigger("finish");
            Destroy(gameObject, delayDestruir);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
