using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Players : MonoBehaviour
{
    private Animation My_Animation;
    bool My_IsPlaying_forward = false;
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
    }
}
