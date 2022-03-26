using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Players : MonoBehaviour
{
    public int Keys;
    private int Lives = 3;
    private Animation My_Animation;
    private bool My_IsPlaying_forward = false;
    private Transform AnimationPlayer;
    private Rigidbody PlayerRigidBody;
    private Transform Porte_sortie;
    private static int LevelsEnCours = 0;
    private static string [] SceneActif = {"", "Level_1"};
    private Vector3 PositionInit =new Vector3(0,-2,0);
    
    void Start()
    {
        AnimationPlayer = GetComponentInChildren<Transform>();
        PlayerRigidBody = GetComponent<Rigidbody>();
        Porte_sortie = GameObject.Find("Porte sortie").GetComponent<Transform>();
        
        //PositionInit = PlayerRigidBody.position;
    }

    void Update()
    {   
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
            LevelsEnCours++;
            SceneManager.LoadScene(SceneActif[LevelsEnCours]);
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
    }
    public static string Get_SceneActif()
    {
        return (SceneActif[DataPlayer.LevelEnCours]);
    }
}
