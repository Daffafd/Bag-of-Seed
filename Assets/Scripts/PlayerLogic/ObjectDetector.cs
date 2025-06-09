using System;
using System.Collections;
using System.Collections.Generic;
using PlayerLogic;
using Sirenix.OdinInspector;
using UnityEngine;

public class ObjectDetector : MonoBehaviour
{
    private Rigidbody _currRigidbody;
    
    private void OnDisable()
    {
        ResetRigidbody();
        Player.Instance.PlayerMovement.SetIsPushing(false);
    }

    private void ResetRigidbody()
    {
        if (!_currRigidbody) return;
        _currRigidbody.isKinematic = true;
        _currRigidbody = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Rigidbody otherRigidbody) &&
            Player.Instance.PlayerMovement.IsGrounded)
        {
            _currRigidbody = otherRigidbody;
            otherRigidbody.isKinematic = false;
            Player.Instance.PlayerMovement.SetIsPushing(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Rigidbody rb) && rb==_currRigidbody)
        {
            ResetRigidbody();
            Player.Instance.PlayerMovement.SetIsPushing(false);
        }
    }
}
