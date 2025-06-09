using System;
using System.Collections;
using System.Collections.Generic;
using PlayerLogic;
using UnityEngine;

public class DangerZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
           player.TeleportToCheckPoint();
        }
    }
}
