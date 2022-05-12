using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MixesController : MonoBehaviour
{
    [SerializeField] private AudioMixer MyaudioMixer;

    public void SetVolume (float sliderValue)
    {
        MyaudioMixer.SetFloat("Master Vol", Mathf.Log10(sliderValue) * 28);
    }


}
