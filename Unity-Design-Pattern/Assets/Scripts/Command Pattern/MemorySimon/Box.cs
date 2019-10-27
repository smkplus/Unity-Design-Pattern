using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Box : MonoBehaviour
{
    [SerializeField]
    private AudioClip audioClip;

    private Material _material;

    private bool _isTurnOn;

    private void Awake()
    {
        _material = GetComponent<MeshRenderer>().material;
    }


    private void OnMouseDown()
    {
        if(_isTurnOn) return;
        TurnOn();
        MemorySimon.Instance.actions.Add(TurnOn);

    }

    public void PlaySound()
    {
        AudioSource.PlayClipAtPoint(audioClip,transform.position);

    }

    public void TurnOn()
    {
        _isTurnOn = true;
        _material.color *= 2;
        PlaySound();

        Invoke(nameof(TurnOff),0.5f);
    }
    
    public void TurnOff()
    {
        _isTurnOn = false;
        _material.color /= 2;
    }


}
