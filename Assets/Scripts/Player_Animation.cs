using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Animation : MonoBehaviour
{
    // Start is called before the first frame update

    private Animation My_Animation;
    private Rigidbody My_Rigidbody;

    void Start()
    {
        My_Animation = GetComponent<Animation>();
        My_Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) == true)
        {
            My_Animation.Play("Players");
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) == true)
        {
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) == true)
        {
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) == true)
        {
        }
    }
}
