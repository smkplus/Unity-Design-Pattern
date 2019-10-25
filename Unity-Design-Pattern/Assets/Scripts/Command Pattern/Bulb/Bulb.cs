using System;
using TMPro;
using UnityEngine;

// Receiver
[ExecuteAlways]
public class Bulb : MonoBehaviour
{
    private MeshRenderer _meshRenderer;
    public bool toggle;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _meshRenderer.sharedMaterial.color = Color.black;
    }

    public void TurnOn()
    {
        _meshRenderer.sharedMaterial.color = Color.yellow;

    }

    public void TurnOff()
    {
        _meshRenderer.sharedMaterial.color = Color.black;
    }
}
