using System.Collections;
using System.Collections.Generic;
using Interactables;
using Interfaces;
using PlayerLogic;
using PrimeTween;
using Sounds;
using Unity.Mathematics;
using UnityEngine;

public class LogTrigger : PlayerInteractable
{
    [SerializeField] private GameObject _logUI;
    [SerializeField] private GameObject _endGameUI;
    [SerializeField] private bool _isLastLog;
    public override void Interact()
    {
        if (!_logUI.activeInHierarchy)
        {
            SoundManager.Instance.PlaySfx("Paper", transform.position);
            _logUI.SetActive(true);
            DisableMovementAndPause();
        }
        else
        {
            _logUI.SetActive(false);
            switch (_isLastLog)
            {
                case true:
                    StartEndingSequence();
                    break;
                case false:
                    EnableMovementAndPause();
                    break;
            }
        }
    }

    protected override void OnTriggerExit(Collider other)
    {
        if (_logUI.activeInHierarchy)
        {
            _logUI.SetActive(false);
            Player.Instance.PlayerMovement.enabled = true;
            Player.Instance.PlayerInput.actions.FindActionMap("Pause").Enable(); 
            Player.Instance.PlayerInput.actions.FindActionMap("Movement").Enable();
        }
        base.OnTriggerExit(other);
    }

    private void DisableMovementAndPause()
    {
        Player.Instance.GetComponent<Animator>().SetBool("isWalking",false);
        Player.Instance.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        Player.Instance.PlayerMovement.enabled = false;
        InputManager.Instance.DisableInputAction("Movement");
        InputManager.Instance.DisableInputAction("Pause");   
    }

    private void EnableMovementAndPause()
    {
        Player.Instance.PlayerMovement.enabled = true;
        InputManager.Instance.EnableInputAction("Pause"); 
        InputManager.Instance.EnableInputAction("Movement"); ;
    }

    private void StartEndingSequence()
    {
        Instantiate(_endGameUI, transform.position, quaternion.identity);
        Player.Instance.PlayerInput.actions["Interact"].performed-=TriggerInteraction;
        GlobalEvents.Instance.OnGameEnd?.Invoke();
        CutsceneManager.Instance.PlayCutscene("Ending");
        Tween.Delay(6f,()=>
        {
            GlobalEvents.Instance.BackToMainMenu();
        });
    }
}
