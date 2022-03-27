using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Players : MonoBehaviour
{

    [SerializeField] private Transform groundCheckTransform = null;
    [SerializeField] private int KeysObject;
    TextMesh status;
    private Animation My_Animation;
    private bool[] boolKeys = new bool[] { false /*Left*/, false/*Arrow*/, false/*Down*/, false /*Right*/};
    private KeyCode[] keyscode = new []{ KeyCode.LeftArrow,KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.RightArrow};
    Transform AnimationPlayer;
    Rigidbody PlayerRigidBody;
    private Vector3 PositionInit = new Vector3((float)-0.126000002, (float)1.60000002, 0);

    void Start()
    {

        status = GameObject.Find("Status").GetComponent<TextMesh>();
        AnimationPlayer = GetComponentInChildren<Transform>();
        My_Animation = GetComponentInChildren<Animation>();
        PlayerRigidBody = GetComponent<Rigidbody>();

    }

    void Update()
    {



        status.text = "Clés : " + KeysObject;

        for (int i = 0; i < keyscode.Length; i++) // Check touts les inputs. 
        {
            if (Input.GetKeyDown(keyscode[i]))
            {
                boolKeys[i] = true;
            }
        }


        
    }
    private void FixedUpdate()
    {
        Debug.Log(Physics.OverlapSphere(groundCheckTransform.position, 0.1f).Length); //Permetde vérifier les points de contacts avec le player.
        if (Physics.OverlapSphere(groundCheckTransform.position, 0.1f).Length == 0)
        {
            iFall();
            return;
        }

        WhereIGo();

    }


    public void WhereIGo() // Fonction pour connaitre la direction du joueur.
    {



        if (boolKeys[0])
        {
            AnimationPlayer.Translate(0, 0, (float)1.25);
            boolKeys[0] = false;
            
        }
        else if (boolKeys[1]) {

            AnimationPlayer.Translate((float)1.25, 0, 0);
            
            boolKeys[1] = false ;
        }
        else if (boolKeys[2]) {
            AnimationPlayer.Translate((float)-1.25, 0, 0);
            boolKeys[2] = false ;
        }
        else if (boolKeys[3]) {
            AnimationPlayer.Translate(0, 0, (float)-1.25);
            
            
            boolKeys[3] = false ;
        }
    }

    public void iFall() // Vérifie si il tombe sous un point y.
    {
        if (transform.position.y < 0)
        {
            PlayerRigidBody.transform.position = PositionInit;
        }
    }



    public void OnTriggerEnter(Collider Col) // Check la colisition avec un objet key.
    {
        if (Col.gameObject.tag == "Keys") 
        {
            Debug.Log("Clé collecté");
            KeysObject++;
            Destroy(Col.gameObject);
        }
    }

}
