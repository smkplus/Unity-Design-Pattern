using System;
using TMPro;
using UnityEngine;

// Receiver
[ExecuteAlways]
public class FlashLight : MonoBehaviour
{
    private MeshRenderer _meshRenderer;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _meshRenderer.sharedMaterial.color = Color.black;
    }

    public void SetColor(Color color)
    {
        _meshRenderer.sharedMaterial.color = color;

    }
}
