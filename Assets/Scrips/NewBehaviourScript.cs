using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Rigidbody Falling_Platform_R;
    void Start()
    {
        Falling_Platform_R = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        if (transform.position.y <= -100)
        {
            Falling_Platform_R.isKinematic = true;
            Falling_Platform_R.transform.position = new Vector3(transform.position.x, 1, transform.position.z);
            Debug.Log("Pos Platform= " + transform.position);
        }
    } 
    void OnTriggerEnter(Collider Col)
    {
        
        if (Col.gameObject.name == "Player")
        {
            Invoke("PlatformTombe",(float) 0.2);
        }
    }
    public void PlatformTombe()
    {
        Debug.Log("Platform tombée");
        Falling_Platform_R.isKinematic = false;
    }

}
