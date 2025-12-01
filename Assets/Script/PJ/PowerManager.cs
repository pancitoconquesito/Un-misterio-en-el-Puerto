using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using NaughtyAttributes;
public class PowerManager : MonoBehaviour
{
    public enum NumPoder
    {
        Poder_DISPARO=1,
        Poder_Bomba=2,
        Poder_teletransportacion=3,
        Poder_inmersion=4,
        Poder_quinto=5,
    }
    public float cadencia;
    
    float curr_cad;

    [ReadOnly] public int currentPowerIndex;
    [ReadOnly] public List<int> l_PowerIndex;
    [ReadOnly] public int totalPowers;
    bool isActiveChangePower;
    bool hasPowers;
    ContextoSingleton contextSingleton;
    Ui_power m_Ui_power;
    List<string> listaPoderes_ANT = new List<string>();
    List<string> listaPoderes_NTX = new List<string>();

    movementPJ m_movementPJ;
    [Header("-Powers scripts-")]
    [SerializeField] private IPOWER m_Poder_DISPARO;
    [SerializeField] private IPOWER m_PODER_Bomba;
    [SerializeField] private IPOWER m_PODER_TeletransportacionPsiquica;
    [SerializeField] private IPOWER m_PODER_Inmersion;
    [SerializeField] private IPOWER m_PODER_Quinto;


    [Header("-Param Powers-")]
    [SerializeField] private changeMirada m_changeMirada;
    [SerializeField] private staminaPsiquica m_staminaPsiquica;
    [SerializeField] private Animator m_Animator;
    public bool IsActiveChangePower { get => isActiveChangePower; set => isActiveChangePower = value; }
    public changeMirada ChangeMirada { get => m_changeMirada; set => m_changeMirada = value; }
    public staminaPsiquica StaminaPsiquica { get => m_staminaPsiquica; set => m_staminaPsiquica = value; }
    public Animator Animator_PJ { get => m_Animator; set => m_Animator = value; }
    public IPOWER Poder_DISPARO { get => m_Poder_DISPARO; set => m_Poder_DISPARO = value; }
    public IPOWER PODER_Bomba { get => m_PODER_Bomba; set => m_PODER_Bomba = value; }
    public IPOWER PODER_TeletransportacionPsiquica { get => m_PODER_TeletransportacionPsiquica; set => m_PODER_TeletransportacionPsiquica = value; }
    public IPOWER PODER_Inmersion{ get => m_PODER_Inmersion; set => m_PODER_Inmersion = value; }
    public IPOWER PODER_Quinto { get => m_PODER_Quinto; set => m_PODER_Quinto = value; }

    // Start is called before the first frame update
    private void Awake()
    {
        l_PowerIndex = new List<int>();
        l_PowerIndex.Add(0);
        l_PowerIndex.Add(0);
        l_PowerIndex.Add(0);
    }
    void Start()
    {
        contextSingleton = DATA.instance.GetContext();
        m_Ui_power = MASTER_REFERENCE.instance.Ui_power;
        curr_cad = cadencia;
        isActiveChangePower = true;
        //listaPoderes
        m_movementPJ = MASTER_REFERENCE.instance.MovementPJ;
    }

    public void UpdatePoderes()//!!!!!!!!!!!!!!!
    {
        contextSingleton = DATA.instance.GetContext();
        m_Ui_power = MASTER_REFERENCE.instance.Ui_power;
        curr_cad = cadencia;
        isActiveChangePower = true;
        //listaPoderes
        m_movementPJ = MASTER_REFERENCE.instance.MovementPJ;

        UpdateData();
    }

    // Update is called once per frame
    void Update()
    {
        if (curr_cad > 0 && IsActiveChangePower) curr_cad -= Time.deltaTime;
    }
    private void getValues()
    {
        //currentPowerIndex = contextSingleton.currentPjPower;
        //totalPowers = contextSingleton.totalPowers;
        currentPowerIndex =GameObject.FindGameObjectWithTag("DATA_SINGLETON").GetComponent<DATA_SINGLETON>().Curr_indexPoder;
        l_PowerIndex[1] = currentPowerIndex;
        totalPowers = DATA.instance.save_load_system.DataGame.DATA_PJ.TotalPowers;
        if (currentPowerIndex == 0 && totalPowers>0)
        {
            currentPowerIndex = 1;
        }
        //Debug.Log("Load poder: "+ currentPowerIndex);
    }
    public void MovePower_toLeft()
    {
        //addCadencia
        //currentPowerIndex = (currentPowerIndex - 1) % (totalPowers+1);
        //if (currentPowerIndex == 0) currentPowerIndex = totalPowers;
        if (curr_cad > 0 || !IsActiveChangePower) return;
        curr_cad = cadencia;

            currentPowerIndex = (currentPowerIndex + 1) % (totalPowers + 1);
        if (currentPowerIndex == 0) currentPowerIndex = 1;
        UpdateUI(ShiftPower.Left);

    }
    public void MovePower_toRight()
    {
        //addCadencia
        //currentPowerIndex = (currentPowerIndex + 1) % (totalPowers+1);
        //if (currentPowerIndex == 0) currentPowerIndex = 1;
        if (curr_cad > 0 || !IsActiveChangePower) return;
        curr_cad = cadencia;

        currentPowerIndex = (currentPowerIndex - 1) % (totalPowers + 1);
        if (currentPowerIndex == 0) currentPowerIndex = totalPowers;
        UpdateUI(ShiftPower.Right);

    }
    public void ExecutePower()
    {
        //Debug.Log("Execute currentPowerIndex: "+ currentPowerIndex);
        //aqui recibo el nombre del poder, el primero, el segundo.etc, 
        //asi puedo saber cual es el priemr poder porejemplo.
        string _currentPoderName=DATA.instance.save_load_system.DataGame.DATA_PJ.GetPoder_Name(currentPowerIndex);
        //Debug.Log("ExecutePower: "+ _currentPoderName);
        switch (_currentPoderName)
        {
            case "null":
                {
                    //Debug.Log("ejecutar null!");
                    break;
                }
            case "1-disparo":
                {
                    
                    if (m_movementPJ.TryPoder_Disparo())
                    {
                        m_Poder_DISPARO.TryExecute(m_movementPJ);
                    }
                    break;
                }
            case "2-desdoblar":
                {
                    if (m_movementPJ.TryPoder_TeletransportacionPsiquica())
                    {
                        PODER_TeletransportacionPsiquica.TryExecute(m_movementPJ);
                    }
                    break;
                }
            case "3-bomba":
                {
                    if (m_movementPJ.TryPoder_Bomba())
                    {
                        m_PODER_Bomba.TryExecute(m_movementPJ);
                    }
                    break;
                }
            //!
            case "4-inmersion":
                {
                    //Debug.Log("4-inmersion");
                    if (m_movementPJ.TryPoder_Bomba())
                    {
                        PODER_Inmersion.TryExecute(m_movementPJ);
                    }
                    break;
                }
            //!
            case "5-quinto":
                {
                    //Debug.Log("5-quinto");
                    if (m_movementPJ.TryPoder_Bomba())
                    {
                        PODER_Quinto.TryExecute(m_movementPJ);
                    }
                    break;
                }
        }
            
    }
    internal void UpdateData()//!
    {
        getValues();
        UpdateUI(ShiftPower.None);
    }
    public enum ShiftPower
    {
        None=0,Left=1,Right=2,
    }
    void UpdateUI(ShiftPower shift)
    {
        //carga a la mala desde DATA
        //DATA_PJ _dataPJ=DATA.instance.save_load_system.m_dataGame.m_DATA_PJ;
        UpdateListasPoderesString(shift);
        m_Ui_power.UpdateUi(listaPoderes_ANT, listaPoderes_NTX, shift);
        //update contextData
        DATA.instance.save_load_system.DataGame.DATA_PJ.CurrentPower = currentPowerIndex;//?
        DATA.instance.GetContext().currentPjPower= currentPowerIndex;
        GameObject.FindGameObjectWithTag("DATA_SINGLETON").GetComponent<DATA_SINGLETON>().Curr_indexPoder = currentPowerIndex;
        //Debug.Log("Guardo poder: "+ currentPowerIndex);
    }
    void UpdateListasPoderesString(ShiftPower shift)
    {
        //Debug.Log("totalPowers: "+ totalPowers);
        listaPoderes_ANT.Clear();
        if (totalPowers < 2)
        {
            listaPoderes_ANT.Add(DATA.instance.save_load_system.DataGame.DATA_PJ.GetPowerLString_DINAMIC()[totalPowers]);
            listaPoderes_ANT.Add(DATA.instance.save_load_system.DataGame.DATA_PJ.GetPowerLString_DINAMIC()[totalPowers]);
            listaPoderes_ANT.Add(DATA.instance.save_load_system.DataGame.DATA_PJ.GetPowerLString_DINAMIC()[totalPowers]);

            listaPoderes_NTX.Clear();
            listaPoderes_NTX.AddRange(listaPoderes_ANT);
            return;
        }
        else
        {
            //Debug.Log($"");
            listaPoderes_ANT.Add(DATA.instance.save_load_system.DataGame.DATA_PJ.GetPowerLString_DINAMIC()[NxtValue(-1)]);
            listaPoderes_ANT.Add(DATA.instance.save_load_system.DataGame.DATA_PJ.GetPowerLString_DINAMIC()[currentPowerIndex]);
            listaPoderes_ANT.Add(DATA.instance.save_load_system.DataGame.DATA_PJ.GetPowerLString_DINAMIC()[NxtValue(1)]);
        }

        listaPoderes_NTX.Clear();

        if (shift == ShiftPower.None)
        {

            listaPoderes_NTX.Add(DATA.instance.save_load_system.DataGame.DATA_PJ.GetPowerLString_DINAMIC()[NxtValue(-1)]);
            listaPoderes_NTX.Add(DATA.instance.save_load_system.DataGame.DATA_PJ.GetPowerLString_DINAMIC()[currentPowerIndex]);
            listaPoderes_NTX.Add(DATA.instance.save_load_system.DataGame.DATA_PJ.GetPowerLString_DINAMIC()[NxtValue(1)]);
        }
        else
        {
            listaPoderes_NTX.Clear();
            listaPoderes_NTX.AddRange(listaPoderes_ANT);
        }
    }

    private int NxtValue(int mod)
    {
        int nxtValue = currentPowerIndex + mod;
        if (nxtValue <= 0 || nxtValue==totalPowers) nxtValue = totalPowers; else nxtValue %= totalPowers;
        return nxtValue;
    }

    
}

