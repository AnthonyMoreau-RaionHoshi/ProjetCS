using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class menu : MonoBehaviour
{
    private static string SaveSeparator = "%VALUE%";
    // le bouton quitter ne peu etre testé que sur un .exe vu qu il quitte l application (pas de stress si il ne fonctionne pas sur unity).
    public static void Jouer() 
    {
        SceneManager.LoadScene(DataPlayer.SceneActif[0]);
        Players.Lives = 2;
        Players.Keys = 0;
        DataPlayer.LevelEnCours = 0;
    }
    public static void save()
    {
        string[] content = new string[]
        {
            DataPlayer.LevelEnCours.ToString(),
            Players.Lives.ToString(),
        };
        string SaveString = string.Join(SaveSeparator, content);
        File.WriteAllText(Application.dataPath + "/data.txt", SaveString);
    }
    public static void load()
    {
        string SaveString = File.ReadAllText(Application.dataPath + "/data.txt");
        string[] content = SaveString.Split(new[] { SaveSeparator }, System.StringSplitOptions.None);
        DataPlayer.LevelEnCours = int.Parse(content[0]);
        Players.Lives = int.Parse(content[1]);
        Players.Keys = 0;
        SceneManager.LoadScene(DataPlayer.SceneActif[DataPlayer.LevelEnCours]);
    }
    public static void Quitter()
    {
        Application.Quit();
    }
    public static void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
    public static void Continue()
    {
        SceneManager.UnloadSceneAsync("UIMenuIG");
    }


}
