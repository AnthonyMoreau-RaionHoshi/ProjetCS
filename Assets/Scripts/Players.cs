using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Players : MonoBehaviour
{
    public static int Keys=0;
    public static int Lives = 2;
    string[,] CurrentScene = new string[3, 4] { { "UI_L1K0", "UI_L1K1", "UI_L1K2", "UI_L1K3"},
        { "UI_L2K0", "UI_L2K1", "UI_L2K2", "UI_L2K3"}, { "UI_L3K0", "UI_L3K1", "UI_L3K2","UI_L3K3"} };
    private Animation My_Animation;
    private bool My_IsPlaying_forward = false;
    private Transform AnimationPlayer;
    private Rigidbody PlayerRigidBody;
    private Transform Porte_sortie;
    private Vector3 PositionInit =new Vector3(0,-2,0);
    
    void Start()
    {
        GOReset();
        AnimationPlayer = GetComponentInChildren<Transform>();
        PlayerRigidBody = GetComponent<Rigidbody>();
        Porte_sortie = GameObject.Find("Porte sortie").GetComponent<Transform>();
        //PositionInit = PlayerRigidBody.position;
    }

    void Update()
    {
        UIUpDate();
        if (Input.GetKeyDown(KeyCode.UpArrow) == true)
        {   
            My_IsPlaying_forward = true;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) == true)
        {
            AnimationPlayer.Translate(0, 0, (float)-1.25);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) == true)
        {
            AnimationPlayer.Translate((float)-1.25, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) == true)
        {
            AnimationPlayer.Translate(0, 0, (float)1.25);
        }
    }
    private void FixedUpdate()
    {
        if (My_IsPlaying_forward == true )
        {
            AnimationPlayer.Translate((float)1.25, 0, 0);
            My_IsPlaying_forward = false ;
        }
        if (transform.position.y < 1.20)
        {
            Lives--;
            PlayerRigidBody.transform.position = -PositionInit;
        }
        if((Vector3.Distance(transform.position, Porte_sortie.transform.position) <0.5) && (Keys >= 3))
        {   
            Debug.Log("Porte passée");
            DataPlayer.LevelEnCours++;
            SceneManager.LoadScene(DataPlayer.SceneActif[DataPlayer.LevelEnCours]);
            Keys = 0;
        }
        
    }

    public void OnTriggerEnter(Collider Col)
    {
        if (Col.gameObject.tag == "Keys") 
        {
            Debug.Log("Cl� collect�");
            Keys++;
            Debug.Log(Keys);
            Destroy(Col.gameObject);
        }

        if ((Col.gameObject.tag == "Spike" )|| (Col.gameObject.tag == "Lazer" ))
        {
            Lives--;
            PlayerRigidBody.transform.position = -PositionInit;
        }
    }
    public void GOReset()
    {
        if (SceneManager.GetSceneByName("UI_GAMEOVER").isLoaded == true)
        {
            SceneManager.UnloadSceneAsync("UI_GAMEOVER");
        }
    }
    public void UIUpDate()
    {
        if (Lives == -1)
        {
            SceneManager.LoadSceneAsync("UI_GAMEOVER", LoadSceneMode.Additive);
            Keys = 0;
            Lives = 2;
            DataPlayer.LevelEnCours = 0;
        }
        else if(SceneManager.GetSceneByName(CurrentScene[Lives, Keys]).isLoaded == false)
        {
            for (int L = 0; L < CurrentScene.GetLength(0); L++)
            {
                for (int K = 0; K < CurrentScene.GetLength(1); K++)
                {
                    if (CurrentScene[Lives, Keys] != CurrentScene[L, K] && SceneManager.GetSceneByName(CurrentScene[L, K]).isLoaded == true)
                    {
                        SceneManager.UnloadSceneAsync(CurrentScene[L, K]);
                    }
                }
            }
            SceneManager.LoadSceneAsync(CurrentScene[Lives, Keys], LoadSceneMode.Additive);
        }   
    }

}
