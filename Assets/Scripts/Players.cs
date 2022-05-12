using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;


public class Players : MonoBehaviour
{
    [SerializeField] private AudioSource musiclevel, musicKey, musicSpike, musicLaser, musicTeleport, musicFall, musicGameOver, musicUnlock;
    [SerializeField] private Transform groundCheckTransform = null;
    [SerializeField] public static int Keys=0;
    [SerializeField] public static int Lives = 2;
    [SerializeField] private static int Coins;
    string[,] CurrentScene = new string[3, 4] { { "UI_L1K0", "UI_L1K1", "UI_L1K2", "UI_L1K3"},
        { "UI_L2K0", "UI_L2K1", "UI_L2K2", "UI_L2K3"}, { "UI_L3K0", "UI_L3K1", "UI_L3K2","UI_L3K3"} };
    private Animation My_Animation;
    private Vector3[] directions3 = { new Vector3(0, 0, (float)1.25), new Vector3((float)1.25, 0, 0), new Vector3((float)-1.25, 0, 0), new Vector3(0, 0, (float)-1.25) };
    private KeyCode[] keyscode = new[] { KeyCode.LeftArrow, KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.RightArrow };
    private bool My_IsPlaying_forward = false;
    private Transform AnimationPlayer;
    private Rigidbody PlayerRigidBody;
    private Transform Porte_sortie;
    private Vector3 PositionInit =new Vector3(0,-2,0);
    private bool boTpMusic;

    void Start()
    {
        if (boTpMusic)
        {
            musicTeleport.Play();
        }
        GOReset();
        AnimationPlayer = GetComponentInChildren<Transform>();
        PlayerRigidBody = GetComponent<Rigidbody>();
        Porte_sortie = GameObject.Find("Porte sortie").GetComponent<Transform>();
        //PositionInit = PlayerRigidBody.position;
        musiclevel.Play();
    }

    void Update()
    {
        UIUpDate();

        if (Physics.OverlapSphere(groundCheckTransform.position, 0.1f).Length > 1)
        {
            Move();
        }

    }

    private void FixedUpdate()
    {

        Fall();
        CheckTp();

    }

    public void OnTriggerEnter(Collider Col)
    {
        switch (Col.gameObject.tag)
        {
            case "Heart":
                Lives++;
                Debug.Log("Life+1");
                break;
            case "Coin":
                Coins++;
                Debug.Log("Coin +1");
                break;
            case "Spike":
                Lives--;
                if (Lives > -1)
                {
                    musicSpike.Play();
                }
                PlayerRigidBody.transform.position = -PositionInit;
                break;
            case "Lazer":
                Lives--;
                if (Lives > -1)
                {
                    musicLaser.Play();
                }
                PlayerRigidBody.transform.position = -PositionInit;
                break;
            case "Keys":
                Debug.Log("Cl� collect�");
                Keys++;
                if ((Keys % 3) == 0)
                {
                    musicUnlock.Play();
                    Debug.Log("Toutes les clées");
                    Destroy(Col.gameObject);
                }
                else
                {
                    musicKey.Play();
                    Debug.Log(Keys);
                    Destroy(Col.gameObject);
                }
                break;
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
        if (Input.GetKeyDown(KeyCode.Escape) == true && SceneManager.GetSceneByName("UIMenuIG").isLoaded == false)
        {
            SceneManager.LoadSceneAsync("UIMenuIG", LoadSceneMode.Additive);
        }
        else if (transform.position.y < 1.20)
        {
            musicFall.Play();
            Lives--;
            PlayerRigidBody.transform.position = -PositionInit;
        }
        else if (Lives == -1)
        {
            Lives = 2;
            Keys = 0;
            musicGameOver.Play();
            SceneManager.LoadSceneAsync("UI_GAMEOVER", LoadSceneMode.Additive);
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


    public void Move()
    {
        for (int i = 0; i < keyscode.Length; i++)
        {
            if (Input.GetKeyDown(keyscode[i]))
            {
                AnimationPlayer.Translate(directions3[i]);
            }
        }
    }


    public void Fall()
    {
        if (transform.position.y < 1)
        {
            Lives--;
            if (Lives > -1)
            {
                musicFall.Play();
            }
            PlayerRigidBody.transform.position = -PositionInit;
        }
    }

    public void CheckTp()
    {
        if ((Vector3.Distance(transform.position, Porte_sortie.transform.position) < 0.5) && (Keys >= 3))
        {
            boTpMusic = true;
            Debug.Log("Porte passée");
            DataPlayer.LevelEnCours++;
            SceneManager.LoadScene(DataPlayer.SceneActif[DataPlayer.LevelEnCours]);
            Keys = 0;
        }
    }

}
