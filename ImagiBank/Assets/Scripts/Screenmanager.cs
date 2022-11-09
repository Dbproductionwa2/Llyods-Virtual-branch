using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Screenmanager : MonoBehaviour
{
    [SerializeField] private GameObject share, stop;
  public void screenShare()
    {
        //SceneManager.UnloadSceneAsync("Desktoplauncher");
        SceneManager.LoadSceneAsync("DeskApp",LoadSceneMode.Additive);
       
    }
    public void StopShare()
    {
       //SceneManager.LoadSceneAsync("Desktoplauncher");
       SceneManager.UnloadSceneAsync("DeskApp");
        
    }
    public void QuitApp()
    {
        Application.Quit();
    }
    public void minimise()
    {

    }
}
