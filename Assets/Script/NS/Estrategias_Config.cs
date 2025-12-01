using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Estrategias_Config : MonoBehaviour
{
    [SerializeField] NS_States_v2026_1 ns_states;
    [SerializeField] List<NodeEstrategia> l_estrategias;
    [SerializeField] List<AreaAlertConfig> m_l_areas;
    [SerializeField] Animator anim;

    public List<NodeEstrategia> L_estrategias { get => l_estrategias; set => l_estrategias = value; }
    public bool OnEstrategias { get => onEstrategias; set => onEstrategias = value; }


    private void Update()
    {
        
    }


    //public int SeleccionarEstrategia(int curr_indiceArea)
    //{
    //    //enlistar por distancia (indexArea)
    //    int indexReturn = -1;
    //    foreach (var item in l_estrategias)//no seria l_estrategias, si no la lista por indexArea
    //    {
    //        if (item.indexArea != curr_indiceArea)
    //        {
    //            continue;
    //        }
    //        if (indexReturn == -1)
    //        {
    //            indexReturn = item.indexEstrategia;
    //        }
    //        int nRandom = Random.Range(0,100);
    //        if (item.probabilidad > nRandom)
    //        {
    //            indexReturn = item.indexEstrategia;
    //            break;
    //        }
    //    }
    //    return indexReturn;
    //}

    bool onEstrategias=false;
    internal void ExitEstrategia() => onEstrategias = false;
    internal void OnEstrategia()
    {
        int indexArea = 1;
        //buscar desde adentro hacia afuera
        for (int i = 0; i < m_l_areas.Count; i++)
        {
            if (m_l_areas[i].IsDentro)
            {
                indexArea = 1 + i;
                break;
            }
        }

        onEstrategias = true;
        int indexANim_E = 1;
        List<NodeEstrategia> l_E_index = l_estrategias.FindAll(x => x.indexArea == indexArea);
        int nRandom_estrategia = Random.Range(0, 100);
        Vector2 rangoEspera = Vector2.zero;
        foreach (var item in l_E_index)
        {
            if (nRandom_estrategia < item.probabilidad)
            {
                indexANim_E = item.indexEstrategia;
                rangoEspera = item.rangoEspera;
                nodeEstrategiaSelected = item;
                break;
            }
        }
        ////llamar
        //float nRandom_espera = Random.Range(rangoEspera.x, rangoEspera.y);
        //ns_states.AlertEstrategia(nRandom_espera, this, nodeEstrategiaSelected.seDetiene);
    }
    NodeEstrategia nodeEstrategiaSelected = null;
    internal void StartEstrategia()
    {
        //anim
        anim.SetFloat("BT_Estrategia", nodeEstrategiaSelected.indexEstrategia);
        anim.ResetTrigger("tr_estrategia");
        anim.SetTrigger("tr_estrategia");
    }


}



[System.Serializable]
public class NodeEstrategia
{
    public int indexArea;
    public int indexEstrategia;
    public int probabilidad;
    public Vector2 rangoEspera;
    public bool seDetiene;
}
