using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void PlayGame()
    {
        //Important de modifier le nom pour qu'il fonctionne
        SceneManager.LoadScene("Main");
    }
    public void ExitGame()
    {
        Application.Quit();
        //A supprimer pour le final build
        print("Application Quit");
    }
}
