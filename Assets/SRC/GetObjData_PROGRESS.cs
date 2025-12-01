using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using System;
using System.Linq;

public class GetObjData_PROGRESS : MonoBehaviour
{
    public enum MEDALLA
    {
        med_verde = 1,
        med_amarillo = 2,
        med_azul = 3,
        med_rojo = 4,
        med_morado = 5,
        med_cafe = 6,
    }
    public enum TARJETA
    {
        tar_disparo = 1,
        tar_bomba = 2,
        tar_desdoblamiento = 3,
        tar_inmersion = 4,
        tar_quinto = 5,
    }
    [SerializeField] MEDALLA m_medalla;
    [SerializeField] TARJETA m_tarjeta;
    [SerializeField] DATA_PROGRESS m_type;
    GLOBAL_TYPE.IDIOMA m_idioma;
    DATA data;
    movementPJ m_movementPJ;

    void Start()
    {
        GetValues();
    }

    void GetValues()
    {
        data = DATA.instance;
        DATA_PROGRESS m_type = data.save_load_system.DataGame.DATA_PROGRESS;
        m_idioma = data.getIdioma();
        m_movementPJ = MASTER_REFERENCE.instance.MovementPJ;
    }

    [Button]
    public void CanJumpOnWall()
    {
        //Has_JumpWall
        DATA_GAME dt_game = data.save_load_system.DataGame;
        dt_game.DATA_PROGRESS.Has_JumpWall = true;
        data.save_load_system.save_(dt_game);
        m_movementPJ.Has_JumpWall = true;
    }

    [Button]
    public void ShowListaPoderes()
    {
        GetValues();
        DATA_PJ m_datPJ = data.save_load_system.DataGame.DATA_PJ;
        m_datPJ.ShowListaPoderes();
    }


    [Button]
    public void addNewPiezaCorazon()
    {
        GetValues();
        DATA_PROGRESS m_type = data.save_load_system.DataGame.DATA_PROGRESS;
        if (m_type == null)
        {
            print("aaaaaaaaaaaaa");
            return;
        }
        int piezaCorazon = m_type.CantidadPiezasCorazon + 1;
        if (piezaCorazon == 4)
        {
            piezaCorazon = 0;
            AumentarCorazon();
        }
        m_type.CantidadPiezasCorazon = piezaCorazon;
        Save();
        //actualziar en pausa?
        MASTER_REFERENCE.instance.UI_Context.CargaDatos_PROGRESO.UpdateCorazon();
        GameObject.FindGameObjectWithTag("DATA_SINGLETON").GetComponent<DATA_SINGLETON>().VidaPj = m_type.CantidadDeCorazonesTotales;
        GameObject.FindGameObjectWithTag("DATA_SINGLETON").GetComponent<DATA_SINGLETON>().VidaMAXIMA_pj = m_type.CantidadDeCorazonesTotales;
        MASTER_REFERENCE.instance.UI_Context.UI_corazon.UpdateFromDATA();
        MASTER_REFERENCE.instance.VidaPJ.ActualziarValoresVida(m_type.CantidadDeCorazonesTotales, m_type.CantidadDeCorazonesTotales);
    }

    [Button]
    public void AddTarjeta()
    {
        GetValues();
        DATA_GAME dt_game = data.save_load_system.DataGame;
        m_type = dt_game.DATA_PROGRESS;

        switch (m_tarjeta)
        {
            case TARJETA.tar_disparo:
            {
                m_type.HasTarjeta_Disparo = true;
                break;
            }
            case TARJETA.tar_bomba:
            {
                m_type.HasTarjeta_Bomba = true;
                break;
            }
            case TARJETA.tar_desdoblamiento:
            {
                m_type.HasTarjeta_Desdoblamiento = true;
                break;
            }
            case TARJETA.tar_inmersion:
            {
                m_type.HasTarjeta_Inmersion = true;
                break;
            }
            case TARJETA.tar_quinto:
            {
                m_type.HasTarjeta_Quinto = true;
                break;
            }
        }

        Save();
        MASTER_REFERENCE.instance.UI_Context.CargaDatos_PROGRESO.UpdateTarjetas();
    }

    [Button]
    public void AddMedalla()
    {
        GetValues();
        DATA_GAME dt_game = data.save_load_system.DataGame;
        m_type = dt_game.DATA_PROGRESS;

        switch (m_medalla)
        {
            case MEDALLA.med_amarillo:
            {
                m_type.Has_Medalla_amarillo = true;
                break;
            }
            case MEDALLA.med_azul:
            {
                m_type.Has_Medalla_azul = true;
                break;
            }
            case MEDALLA.med_rojo:
            {
                m_type.Has_Medalla_rojo = true;
                break;
            }
            case MEDALLA.med_morado:
            {
                m_type.Has_Medalla_morado = true;
                break;
            }
            case MEDALLA.med_verde:
            {
                m_type.Has_Medalla_verde = true;
                break;
            }
            case MEDALLA.med_cafe:
                {
                    m_type.Has_Medalla_cafe = true;
                    break;
                }
        }

        Save();
        MASTER_REFERENCE.instance.UI_Context.CargaDatos_PROGRESO.UpdateMedallas();
    }

    [Button]
    public void Add_Telekinesis()
    {
        GetValues();
        DATA_GAME dt_game = data.save_load_system.DataGame;
        dt_game.DATA_PROGRESS.Has_telekinesis=true;
        data.save_load_system.save_(dt_game);
        m_movementPJ.Poder_telekinesis = true;
    }

    [Button]
    public void Add_Dash()
    {
        GetValues();
        DATA_GAME dt_game = data.save_load_system.DataGame;
        dt_game.DATA_PROGRESS.Has_Dash = true;
        data.save_load_system.save_(dt_game);
        m_movementPJ.Poder_CanDash = true;
    }
    void Save()
    {
        DATA_GAME dt_game = data.save_load_system.DataGame;
        dt_game.DATA_PROGRESS = m_type;
        data.save_load_system.save_(dt_game);
    }
    void AumentarCorazon()
    {
        GetValues();
        int cantidadCorazones = m_type.CantidadDeCorazonesTotales + 1;
        m_type.CantidadDeCorazonesTotales = cantidadCorazones;
        //
    }

    [Button("Get_ALL")]
    public void Get_ALL()
    {
        List<MEDALLA> listaMedallas = Enum.GetValues(typeof(MEDALLA)).Cast<MEDALLA>().ToList();
        foreach (var item in listaMedallas)
        {
            m_medalla = item;
            AddMedalla();
        }
        List<TARJETA> listaTarjetas = Enum.GetValues(typeof(TARJETA)).Cast<TARJETA>().ToList();
        foreach (var item in listaTarjetas)
        {
            m_tarjeta = item;
            AddTarjeta();
        }
        Add_Telekinesis();
        Add_Dash();
        CanJumpOnWall();
    }

    [Button]
    public void VisualizacionSOLIDOS_activar()
    {
        GameObject[] objsSolidos = GameObject.FindGameObjectsWithTag("Plataform");
        foreach (var item in objsSolidos)
        {
            SpriteRenderer spRenderer = item.GetComponent<SpriteRenderer>();
            if (spRenderer != null)
            {
                spRenderer.enabled = true;
                Color color = spRenderer.color;
                color.a = 0.5f;
                spRenderer.color= color;
            }
        }
    }

    [Button]
    public void VisualizacionSOLIDOS_desactivar()
    {
        GameObject[] objsSolidos = GameObject.FindGameObjectsWithTag("Plataform");
        foreach (var item in objsSolidos)
        {
            SpriteRenderer spRenderer = item.GetComponent<SpriteRenderer>();
            if (spRenderer != null)
            {
                spRenderer.enabled = false;
            }
        }
    }
}
