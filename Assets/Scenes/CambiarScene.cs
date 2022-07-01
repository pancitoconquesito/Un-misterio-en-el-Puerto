using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CambiarScene : MonoBehaviour
{
    [SerializeField] private string nameStage;
    [SerializeField] private float delay;
    [SerializeField] private bool StartChange;
    private void Start()
    {
        if(StartChange) Invoke("changeStageWithOutDATA", delay);
    }
    public void setNameScene(string value)
    {
        nameStage = value;
    }
    public void changeScene()
    {
        Invoke("changeStageNow",delay);
    }
    public void changeScene(string _name)
    {
        nameStage = _name;
        Invoke("changeStageNow", delay);
    }
    public void changeScene(float _delay)
    {
        Invoke("changeStageNow", _delay);
    }
    public void changeScene(string _name, float _delay)
    {
        nameStage = _name;
        Invoke("changeStageNow()", _delay);
    }
    private void changeStageNow()
    {
        if(nameStage=="asdf") nameStage= DATA.instance.save_load_system.m_dataGame.m_DATA_PROGRESS.nameStageSaveRoom;
        SceneManager.LoadScene(nameStage);
    }
    private void changeStageWithOutDATA()
    {
        SceneManager.LoadScene(nameStage);
    }
}
