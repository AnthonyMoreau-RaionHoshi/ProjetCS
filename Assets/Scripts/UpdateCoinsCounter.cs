using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateCoinsCounter : MonoBehaviour
{
    // Start is called before the first frame update
    Text CoinsCounters;
    void Start()
    {
        CoinsCounters = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        CoinsCounters.text = DataPlayer.Coins.ToString()+" X ";
    }
}
