using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Players : MonoBehaviour
{
    public int KeysObject;
    private Animation My_Animation;
    private bool My_IsPlaying_forward = false; // A mettre dans un tableau ?
    private bool key_pressed_left = false;     // A mettre dans un tableau ?
    private bool key_pressed_right = false;    // A mettre dans un tableau ?
    private bool key_pressed_down = false;     // A mettre dans un tableau ?
     // private KeyCode[] keyscode = new []{KeyCode.UpArrow, KeyCode.RightArrow, KeyCode.DownArrow, KeyCode.RightArrow, KeyCode.LeftArrow }; Idée pour simplifer le code ?
    Transform AnimationPlayer;
    Rigidbody PlayerRigidBody;

    Vector3 PositionInit =new Vector3(0,-2,0);
    void Start()
    {
        AnimationPlayer = GetComponentInChildren<Transform>();
        My_Animation = GetComponentInChildren<Animation>();
        PlayerRigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {   
        if (Input.GetKeyDown(KeyCode.UpArrow) == true)
        {   
            My_IsPlaying_forward = true;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) == true)
        {
            key_pressed_right = true;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) == true)
        {
            key_pressed_down = true;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) == true)
        {
            key_pressed_left=true;
        }
    }
    private void FixedUpdate()
    {
        WhereIGo();
        iFall();
       
    }


    public void WhereIGo() // Fonction pour connaitre la direction du joueur.
    {
        if (My_IsPlaying_forward == true)
        {
            AnimationPlayer.Translate((float)1.25, 0, 0);
            My_IsPlaying_forward = false;
        }
        else if (key_pressed_down == true) {
            AnimationPlayer.Translate((float)-1.25, 0, 0);
            key_pressed_down = false ;
        }
        else if (key_pressed_right == true) {
            AnimationPlayer.Translate(0, 0, (float)-1.25);
            key_pressed_right = false ;
        }
        else if (key_pressed_left == true) {
            AnimationPlayer.Translate(0,0, (float)1.25);
            key_pressed_left = false ;
        }
    }

    public void iFall() // Vérifie si il tombe 
    {
        if (transform.position.y < 1.20)
        {
            PlayerRigidBody.transform.position = -PositionInit;

        }
    }


    public void OnTriggerEnter(Collider Col)
    {
        if (Col.gameObject.tag == "Keys") 
        {
            Debug.Log("Clé collecté");
            KeysObject++;
            Destroy(Col.gameObject);
        }
    }

}
