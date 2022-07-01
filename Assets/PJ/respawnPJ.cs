using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class respawnPJ : MonoBehaviour
{
    [SerializeField]private float delayRespawn;

    public void respawn()
    {
        Invoke("changeScene", delayRespawn);
    }
    private void changeScene()
    {
        DATA_SINGLETON singleton = GameObject.FindGameObjectWithTag("DATA_SINGLETON").GetComponent<DATA_SINGLETON>();
        singleton.m_tipoEntrada = GLOBAL_TYPE.TIPO_ENTRADA.comenzarGameplay;
        singleton.id_entrada_siguienteEtapa = 0;
        string scenName = singleton.stageInitialSaveRoom;
        SceneManager.LoadScene(scenName);
    }
}
