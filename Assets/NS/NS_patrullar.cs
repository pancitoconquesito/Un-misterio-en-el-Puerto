using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NS_patrullar : MonoBehaviour
{
    [SerializeField] private Rigidbody2D m_rigidbody;
    [SerializeField] private GameObject stepGO_folder;
    private GameObject[] stepGO;
    private int cantidadSteps;
    [Range(0f,100f)][SerializeField] private float speed;

    //[SerializeField] private bool ignorarY;
    //[SerializeField] private bool ignorarX;

    private Vector3[] STEPS;
    private int currentStep;
    private float diferencia;

    //private bool avanzar;
    //[SerializeField] private float delay;
    //private float currentDelay;
    //private float valorComponente_X, valorComponente_Y;
    //private float xInicial, yInicial;
    private void Start()
    {
        //xInicial = transform.position.x;
        //yInicial = transform.position.y;
        cantidadSteps = stepGO_folder.transform.childCount;
        stepGO = new GameObject[cantidadSteps];
        for (int i = 0; i < cantidadSteps; i++)
        {
            stepGO[i] = stepGO_folder.transform.GetChild(i).gameObject;
        }

        STEPS = new Vector3[cantidadSteps];
        for (int i = 0; i < cantidadSteps; i++)
        {
            STEPS[i] = stepGO[i].transform.position;
        }
        currentStep = 0;
        dir = (STEPS[currentStep] - transform.position).normalized;
        //avanzar = true;
    }
    private int idMove;
    private void Update()
    {
        verificarDistancia();
        dir = (STEPS[currentStep] - transform.position).normalized;

        //moverObjeto();
    }
    private void FixedUpdate()
    {
        m_rigidbody.velocity = dir * speed;
    }
    private Vector3 dir;
    
    /*
    private void moverObjeto()
    {
        //LeanTween.cancel(idMove);
        //if (ignorarX) vectActual.x = xInicial;

        //if (ignorarY) vectActual.y = yInicial;

        dir = (STEPS[currentStep] - transform.position).normalized;

        //idMove = LeanTween.move(gameObject, dir + transform.position, 1f-speed).id;
        //if (STEPS[currentStep].x > transform.position.x) dir = Vector3.right;
        //else dir = Vector3.left;

       
    }
    */
    private void verificarDistancia()
    {
        diferencia = Vector3.Distance(transform.position, STEPS[currentStep]);
        if (diferencia < 0.1f)
        {
            //TODO delay
            cambiarStep();
        }
    }

    private void cambiarStep()
    {
        print("aaaa");
        currentStep++;
        if (currentStep >= cantidadSteps) currentStep = 0;
        dir = (STEPS[currentStep] - transform.position).normalized;
    }
}
