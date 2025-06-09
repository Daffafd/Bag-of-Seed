using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GlobalEvents : MonoBehaviour
{
    public static GlobalEvents Instance;

    public Action OnChiliExplode;

    public Action OnGameEnd;

    public Action OnBossDefeated;

    public Action<InputControlScheme> OnControlSchemeChange;

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
    }

    public void BackToMainMenu()
    {
        StartCoroutine(LoadSceneCoroutine());
    }
    
    private IEnumerator LoadSceneCoroutine()
    {
        var operation = SceneManager.LoadSceneAsync(0);

        while (operation is { isDone: false })
        {
            Debug.Log(operation.progress);
            yield return null;
        }
    }
}
