using System;
using System.Collections;
using System.Collections.Generic;
using PlayerLogic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private InputActionAsset _playerControl;

    private void OnEnable()
    {
        _playerControl.FindActionMap("Pause").FindAction("PauseGame").performed+=OnPauseGame;
    }

    private void OnDisable()
    {
        _playerControl.FindActionMap("Pause").Disable();
        _playerControl.FindActionMap("Pause").FindAction("PauseGame").performed-=OnPauseGame;
    }

    private void OnPauseGame(InputAction.CallbackContext ctx)
    {
        if (!_pausePanel.activeInHierarchy)
        {
            _pausePanel.SetActive(true);
            EventSystem.current.SetSelectedGameObject(_pausePanel.transform.GetChild(1).gameObject);
            _playerControl.FindActionMap("Movement").Disable();
            _playerControl.FindActionMap("Interaction").Disable();
            Time.timeScale = 0;
        }
        else
        {
            _pausePanel.SetActive(false);
            _playerControl.FindActionMap("Movement").Enable();
            _playerControl.FindActionMap("Interaction").Enable();
            Time.timeScale = 1;
        }
    }
    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
#if UNITY_EDITOR
        if(EditorApplication.isPlaying)
        {
            EditorApplication.isPlaying = false;
        }
#endif
        Application.Quit();
    }
    
}
