using System;
using Unity.VisualScripting;
using UnityEngine;

public class Wither:MonoBehaviour
{
    [SerializeField] private Material _mat;

    public void ChangeMaterial()
    {
        GetComponent<MeshRenderer>().material = _mat;
    }
}