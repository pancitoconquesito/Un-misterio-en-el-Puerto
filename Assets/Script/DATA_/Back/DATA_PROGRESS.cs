using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class DATA_PROGRESS
{
    int cantidadDeCorazonesTotales;
    int cantidadPiezasCorazon;


    string nameStageSaveRoom;
    string nameBackground;
    private int indiceNEKO;

    private bool hasTarjeta_Bomba;
    private bool hasTarjeta_Disparo;
    private bool hasTarjeta_Desdoblamiento;
    private bool hasTarjeta_Inmersion;
    private bool hasTarjeta_Quinto;

    private bool has_Medalla_verde;
    private bool has_Medalla_azul;
    private bool has_Medalla_rojo;
    private bool has_Medalla_cafe;
    private bool has_Medalla_amarillo;
    private bool has_Medalla_morado;

    private bool has_Dash;
    private bool has_telekinesis;
    private bool has_JumpWall;
    public DATA_PROGRESS()
    {
        CantidadDeCorazonesTotales = 3;
        CantidadPiezasCorazon = 0;


        NameStageSaveRoom = "S0_A";
        nameBackground = "None";
        IndiceNEKO = 0;

        Has_Dash = false;
        Has_telekinesis = false;

        HasTarjeta_Bomba = false;
        HasTarjeta_Disparo= false;
        HasTarjeta_Desdoblamiento= false;
        HasTarjeta_Inmersion = false;
        HasTarjeta_Quinto=false;

        Has_Medalla_verde= false;
        Has_Medalla_azul= false;
        Has_Medalla_rojo= false;
        Has_Medalla_cafe= false;
        Has_Medalla_amarillo = false;
        Has_Medalla_morado = false;

        Has_JumpWall = false;
    }

    public bool HasTarjeta_Bomba { get => hasTarjeta_Bomba; set => hasTarjeta_Bomba = value; }
    public bool HasTarjeta_Disparo { get => hasTarjeta_Disparo; set => hasTarjeta_Disparo = value; }
    public bool HasTarjeta_Desdoblamiento { get => hasTarjeta_Desdoblamiento; set => hasTarjeta_Desdoblamiento = value; }
    public bool HasTarjeta_Inmersion { get => hasTarjeta_Inmersion; set => hasTarjeta_Inmersion = value; }
    public bool HasTarjeta_Quinto { get => hasTarjeta_Quinto; set => hasTarjeta_Quinto = value; }
    public int CantidadDeCorazonesTotales { get => cantidadDeCorazonesTotales; set => cantidadDeCorazonesTotales = value; }
    public string NameStageSaveRoom { get => nameStageSaveRoom; set => nameStageSaveRoom = value; }
    public bool Has_Medalla_verde { get => has_Medalla_verde; set => has_Medalla_verde = value; }
    public bool Has_Medalla_azul { get => has_Medalla_azul; set => has_Medalla_azul = value; }
    public bool Has_Medalla_rojo { get => has_Medalla_rojo; set => has_Medalla_rojo = value; }
    public bool Has_Medalla_cafe { get => has_Medalla_cafe; set => has_Medalla_cafe = value; }
    public bool Has_Medalla_amarillo { get => has_Medalla_amarillo; set => has_Medalla_amarillo = value; }
    public bool Has_Medalla_morado { get => has_Medalla_morado; set => has_Medalla_morado = value; }
    public int CantidadPiezasCorazon { get => cantidadPiezasCorazon; set => cantidadPiezasCorazon = value; }
    public bool Has_Dash { get => has_Dash; set => has_Dash = value; }
    public bool Has_telekinesis { get => has_telekinesis; set => has_telekinesis = value; }
    public string NameBackground { get => nameBackground; set => nameBackground = value; }
    public int IndiceNEKO { get => indiceNEKO; set => indiceNEKO = value; }
    public bool Has_JumpWall { get => has_JumpWall; set => has_JumpWall = value; }
}
