using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour
{
    // le bouton quitter ne peu etre testé que sur un .exe vu qu il quitte l application (pas de stress si il ne fonctionne pas sur unity).
    public static void Jouer() 
    {
        SceneManager.LoadScene("Level_1");
    }
    public static void Charger()
    {
        SceneManager.LoadScene(Players.Get_SceneActif());
    }
    public static void Quitter()
    {

        Application.Quit();
    }
}
