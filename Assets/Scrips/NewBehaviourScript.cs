using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Rigidbody Falling_Platform;

    void Start()
    {
        Falling_Platform = GetComponent<Rigidbody>();
    }

    void OnTriggerEnter(Collider Col)
    {
        
        if (Col.gameObject.name == "Player")
        {
            Debug.Log("Platform tombée");

            Falling_Platform.isKinematic = false;

        }
    }

}
