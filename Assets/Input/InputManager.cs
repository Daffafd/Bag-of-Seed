using System;
using PlayerLogic;
using PrimeTween;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    [SerializeField] private InputActionAsset _asset;

    public Action<string> OnNotifyControlChange;
    
    public string CurrentControlScheme { get; private set; }
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        
        InputUser.onChange += (inputUser, changeEnum, inputDevice) =>
        {
            if (changeEnum == InputUserChange.ControlSchemeChanged)
            {
                if (inputUser.controlScheme != null)
                {
                    Debug.Log((InputControlScheme)inputUser.controlScheme);
                    GlobalEvents.Instance?.OnControlSchemeChange?.Invoke((InputControlScheme)inputUser.controlScheme);
                    CurrentControlScheme = inputUser.controlScheme?.bindingGroup;
                }
            }
        };
    }

    private void Start()
    {
        CurrentControlScheme = Player.Instance.PlayerInput.currentControlScheme;
    }

    public void EnableInputAction(string id)
    {
        _asset.FindActionMap(id).Enable();
    }
    
    public void DisableInputAction(string id)
    {
        _asset.FindActionMap(id).Disable();
    }
    
    public void Rumble()
    {
        if (Gamepad.current!=null)
        {
            Gamepad pad = Gamepad.current;
            pad.SetMotorSpeeds(0.25f,0.5f);
            Tween.Delay(0.5f,()=>pad.SetMotorSpeeds(0f,0f));
        }
    }
}
