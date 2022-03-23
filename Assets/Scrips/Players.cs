using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 

public class Players : MonoBehaviour
{
    public int Keys;
    private Animation My_Animation;
    bool My_IsPlaying_forward = false;
    Transform AnimationPlayer;
    Rigidbody PlayerRigidBody;
    Transform Porte_sortie;

    Vector3 PositionInit =new Vector3(0,-2,0);
    void Start()
    {
        AnimationPlayer = GetComponentInChildren<Transform>();
        PlayerRigidBody = GetComponent<Rigidbody>();
        Porte_sortie = GameObject.Find("Porte_sortie").GetComponent<Transform>();
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
            PlayerRigidBody.transform.position = -PositionInit;
        }
        if((Vector3.Distance(PlayerRigidBody.transform.position, Porte_sortie.transform.position) <0.5) && (Keys >= 3))
        {
            PlayerRigidBody.transform.position = -PositionInit; 
        }
    }

    public void OnTriggerEnter(Collider Col)
    {
        if (Col.gameObject.tag == "Keys") 
        {
            Debug.Log("Cl� collect�");
            Keys++;
            Destroy(Col.gameObject);
        }

        
    }

    //fait crash unity et VS, je dois encore r�gler �a mais jsp comment faire (Damien)
    //Le but final �tant de faire tomber la plaque bleu fonc� quand on passe dans le collider.

   
}
