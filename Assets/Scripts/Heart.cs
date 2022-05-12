using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    [SerializeField] private AudioSource sound_heart_collect;
    public void OnTriggerEnter(Collider Col)
    {
        if ((Col.gameObject.tag) == "Player")
        {
            sound_heart_collect.Play();
            Destroy(gameObject);
        }


    }
}
