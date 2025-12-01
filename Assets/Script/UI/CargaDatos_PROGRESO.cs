using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NaughtyAttributes;
public class CargaDatos_PROGRESO : MonoBehaviour
{
    [Header("---IMG Tarjetas--")]
    [SerializeField] Sprite sp_NoTarjeta;
    [SerializeField] NodeTarjetaDATA  img_NodeTarjeta_Bomba;
    [SerializeField] NodeTarjetaDATA  img_NodeTarjeta_Disparo;
    [SerializeField] NodeTarjetaDATA  img_NodeTarjeta_Desdoblamiento;
    [SerializeField] NodeTarjetaDATA  img_NodeTarjeta_Inmersion;
    [SerializeField] NodeTarjetaDATA  img_NodeTarjeta_Quinto;

    [Header("---Medallas--")]
    [SerializeField] Sprite sp_NoMedalla;
    [SerializeField] NodeTarjetaDATA img_Medalla_verde;
    [SerializeField] NodeTarjetaDATA img_Medalla_azul;
    [SerializeField] NodeTarjetaDATA img_Medalla_rojo;
    [SerializeField] NodeTarjetaDATA img_Medalla_cafe;
    [SerializeField] NodeTarjetaDATA img_Medalla_amarillo;
    [SerializeField] NodeTarjetaDATA img_Medalla_morado;
    
    
    [Header("---Corazon--")]
    [SerializeField] Image img_Corazon_1;
    [SerializeField] Image img_Corazon_2;
    [SerializeField] Image img_Corazon_3;
    [SerializeField] NodeTarjetaDATA dataTexto;

    GLOBAL_TYPE.IDIOMA m_idioma;
    List<NodeTarjetaDATA> l_Tarjetas;
    List<NodeTarjetaDATA> l_Medallas;
    
    void Start()
    {
        UpdateTarjetas();

        UpdateMedallas();

        UpdateCorazon();


    }

    public void UpdateCorazon()
    {
        DATA data = DATA.instance;
        DATA_PROGRESS dataProgress = data.save_load_system.DataGame.DATA_PROGRESS;
        m_idioma = data.getIdioma();
        switch (m_idioma)
        {
            case GLOBAL_TYPE.IDIOMA.espanol:
                {
                    dataTexto.data.Descripcion = dataTexto.descripcion.texto_ESPANOL;
                    dataTexto.data.Titulo = dataTexto.titulo.texto_ESPANOL;
                    break;
                }
            case GLOBAL_TYPE.IDIOMA.ingles:
                {
                    dataTexto.data.Descripcion = dataTexto.descripcion.texto_INGLES;
                    dataTexto.data.Titulo = dataTexto.titulo.texto_INGLES;
                    break;
                }
        }
        //////////
        if (dataProgress == null)
        {
            data = DATA.instance;
            dataProgress = data.save_load_system.DataGame.DATA_PROGRESS;
        }
        if (data == null)
        {
            print("data null");
        }
        if (data.save_load_system == null)
        {
            print("data.save_load_system null");
        }
        if (data.save_load_system.DataGame == null)
        {
            print("data.save_load_system.DataGame null");
        }
        if (data.save_load_system.DataGame.DATA_PROGRESS == null)
        {
            print("data.save_load_system.DataGame.DATA_PROGRESS null");
        }
        if (dataProgress == null)
        {
            print("aaaaaaaaaaaaaaa");
        }
        //////////


        int piezasDeCorazones = dataProgress.CantidadPiezasCorazon;
        img_Corazon_1.enabled = false;
        img_Corazon_2.enabled = false;
        img_Corazon_3.enabled = false;
        switch (piezasDeCorazones)
        {
            case 1:
                {
                    img_Corazon_1.enabled = true;
                    break;
                }
            case 2:
                {
                    img_Corazon_1.enabled = true;
                    img_Corazon_2.enabled = true;
                    break;
                }
            case 3:
                {
                    img_Corazon_1.enabled = true;
                    img_Corazon_2.enabled = true;
                    img_Corazon_3.enabled = true;
                    break;
                }
        }
    }

    public void UpdateMedallas()
    {
        l_Medallas = new List<NodeTarjetaDATA>();
        l_Medallas.Add(img_Medalla_verde);
        l_Medallas.Add(img_Medalla_azul);
        l_Medallas.Add(img_Medalla_rojo);
        l_Medallas.Add(img_Medalla_cafe);
        l_Medallas.Add(img_Medalla_amarillo);
        l_Medallas.Add(img_Medalla_morado);

        DATA data = DATA.instance;
        m_idioma = data.getIdioma();
        foreach (NodeTarjetaDATA item in l_Medallas)
        {
            SetTextoIdioma(item);
        }

        DATA_PROGRESS dataProgress = data.save_load_system.DataGame.DATA_PROGRESS;
        img_Medalla_verde.has = dataProgress.Has_Medalla_verde;
        img_Medalla_azul.has = dataProgress.Has_Medalla_azul;
        img_Medalla_rojo.has = dataProgress.Has_Medalla_rojo;
        img_Medalla_cafe.has = dataProgress.Has_Medalla_cafe;
        img_Medalla_amarillo.has = dataProgress.Has_Medalla_amarillo;
        img_Medalla_morado.has = dataProgress.Has_Medalla_morado;

        foreach (NodeTarjetaDATA item in l_Medallas)
        {
            UpdateImgAndText(item, sp_NoMedalla);
        }
    }
    public void UpdateTarjetas()
    {
        l_Tarjetas = new List<NodeTarjetaDATA>();
        l_Tarjetas.Add(img_NodeTarjeta_Bomba);
        l_Tarjetas.Add(img_NodeTarjeta_Disparo);
        l_Tarjetas.Add(img_NodeTarjeta_Desdoblamiento);
        l_Tarjetas.Add(img_NodeTarjeta_Inmersion);
        l_Tarjetas.Add(img_NodeTarjeta_Quinto);
        DATA data = DATA.instance;
        //idioma textos
        m_idioma = data.getIdioma();
        foreach (NodeTarjetaDATA item in l_Tarjetas)
        {
            SetTextoIdioma(item);
        }

        //Has
        DATA_PROGRESS dataProgress = data.save_load_system.DataGame.DATA_PROGRESS;
        img_NodeTarjeta_Bomba.has = dataProgress.HasTarjeta_Bomba;
        img_NodeTarjeta_Disparo.has = dataProgress.HasTarjeta_Disparo;
        img_NodeTarjeta_Desdoblamiento.has = dataProgress.HasTarjeta_Desdoblamiento;
        img_NodeTarjeta_Inmersion.has = dataProgress.HasTarjeta_Inmersion;
        img_NodeTarjeta_Quinto.has = dataProgress.HasTarjeta_Quinto;

        foreach (NodeTarjetaDATA item in l_Tarjetas)
        {
            UpdateImgAndText(item, sp_NoTarjeta);
        }

        //
        DATA_PJ dataPJ = DATA.instance.save_load_system.DataGame.DATA_PJ;
        if (img_NodeTarjeta_Disparo.has)
        {
            dataPJ.AddPower("1-disparo");
        }
        if (img_NodeTarjeta_Desdoblamiento.has)
        {
            dataPJ.AddPower("2-desdoblar");
        }
        if (img_NodeTarjeta_Bomba.has)
        {
            dataPJ.AddPower("3-bomba");
        }
        if (img_NodeTarjeta_Inmersion.has)
        {
            dataPJ.AddPower("4-inmersion");
        }
        if (img_NodeTarjeta_Quinto.has)
        {
            dataPJ.AddPower("5-quinto");
        }


        //visual//!TODO
        MASTER_REFERENCE.instance.PowerManager.UpdatePoderes();
        //logica

    }

    private void SetTextoIdioma(NodeTarjetaDATA nodoTarjeta)
    {
        switch (m_idioma)
        {
            case GLOBAL_TYPE.IDIOMA.espanol:
                {
                    nodoTarjeta.data.Titulo = nodoTarjeta.titulo.texto_ESPANOL;
                    nodoTarjeta.data.Descripcion = nodoTarjeta.descripcion.texto_ESPANOL;
                    break;
                }
            case GLOBAL_TYPE.IDIOMA.ingles:
                {
                    nodoTarjeta.data.Titulo = nodoTarjeta.titulo.texto_INGLES;
                    nodoTarjeta.data.Descripcion = nodoTarjeta.descripcion.texto_INGLES;
                    break;
                }
        }
    }

    private void UpdateImgAndText(NodeTarjetaDATA nodeTarjeta, Sprite sp)
    {
        if (!nodeTarjeta.has)
        {
            //Debug.Log("poder no existente");
            nodeTarjeta.img.sprite = sp;
            nodeTarjeta.data.Titulo = "???";
            nodeTarjeta.data.Descripcion = "";
        }
        else
        {
            nodeTarjeta.img.sprite = nodeTarjeta.sp_ok;
        }
    }



}
