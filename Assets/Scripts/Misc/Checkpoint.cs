using System;
using System.Collections;
using System.Collections.Generic;
using PlayerLogic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player.Instance.LastCheckPoint = transform.position;
        }
    }
}
