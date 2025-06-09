using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(menuName = "Database/InputDatabase", fileName = "NewInputDatabase")]
public class InputDatabase : ScriptableObject
{
    public SerializedDictionary<string, RuntimeAnimatorController> InputDictionary = new();


    public RuntimeAnimatorController GetAnimatorController(string path)
    {
        return InputDictionary[path];
    }
}
