using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checker_Areas : MonoBehaviour
{
    public enum TipoArea
    {
        Rectangular, Circular
    }
    [SerializeField] TipoArea tipoArea = TipoArea.Rectangular;
    [SerializeField] List<Vector2> areas;
    [SerializeField, Range(0.01f, 1f)] float alphaColorAreaDistancia = 0.05f;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnDrawGizmos()
    {
        if (areas != null && areas.Count > 0)
        {
            Gizmos.color = Color.white;
            int indice = 0;

            if (tipoArea == TipoArea.Circular)
            {
                for (int i = 0; i < areas.Count; i++)
                {
                    areas[i] = new Vector2(areas[i].x, areas[i].x);
                }
            }

            foreach (var vector in areas)
            {
                int currIndice = indice % GLOBAL_TYPE.lista_Colores_A.Count;
                indice++;
                Gizmos.color = new Color(GLOBAL_TYPE.lista_Colores_A[currIndice].r, GLOBAL_TYPE.lista_Colores_A[currIndice].g, GLOBAL_TYPE.lista_Colores_A[currIndice].b, alphaColorAreaDistancia);

                if (tipoArea == TipoArea.Rectangular)
                {
                    Gizmos.DrawWireCube(transform.position, new Vector3(vector.x, vector.y, 0) * 2f);
                    Gizmos.DrawCube(transform.position, new Vector3(vector.x, vector.y, 0) * 2f);
                }
                else
                {
                    Gizmos.DrawSphere(transform.position, vector.magnitude);
                    Gizmos.color = Color.white;
                    Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y + vector.magnitude));
                    Gizmos.DrawLine(transform.position, new Vector2(transform.position.x + vector.magnitude, transform.position.y));
                }
            }
        }


//        if (distanciaMinima_limitePerseguir != null)
//        {
//            Gizmos.color = Color.white;
//            int indice = 0;

//            if (tipoArea_limitePerseguir == TipoArea.Circular)
//            {
//                distanciaMinima_limitePerseguir = new Vector2(distanciaMinima_limitePerseguir.x, distanciaMinima_limitePerseguir.x);
//            }

//            indice = GLOBAL_TYPE.lista_Colores.Count - 1;
//            Gizmos.color = new Color(GLOBAL_TYPE.lista_Colores[indice].r, GLOBAL_TYPE.lista_Colores[indice].g, GLOBAL_TYPE.lista_Colores[indice].b, alphaColorAreaDistancia);

//            Vector2 originVector = originalPosition;
//#if UNITY_EDITOR
//            if (!Application.isPlaying)
//            {
//                originVector = transform.position;
//            }
//            else
//            {
//                //Debug.Log("En el Editor y el juego está corriendo");
//            }
//#endif
//            if (tipoArea == TipoArea.Rectangular)
//            {
//                Gizmos.DrawWireCube(originVector, new Vector3(distanciaMinima_limitePerseguir.x, distanciaMinima_limitePerseguir.y, 0) * 2f);
//                Gizmos.DrawCube(originVector, new Vector3(distanciaMinima_limitePerseguir.x, distanciaMinima_limitePerseguir.y, 0) * 2f);
//            }
//            else
//            {
//                Gizmos.DrawSphere(originVector, distanciaMinima_limitePerseguir.magnitude);
//                Gizmos.color = Color.white;
//                Gizmos.DrawLine(originVector, new Vector2(originVector.x, originVector.y + distanciaMinima_limitePerseguir.magnitude));
//                Gizmos.DrawLine(originVector, new Vector2(originVector.x + distanciaMinima_limitePerseguir.magnitude, originVector.y));
//            }
//        }
    }

}
