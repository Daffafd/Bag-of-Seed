using System;
using PrimeTween;
using UnityEngine;

public class ImageCutscene : MonoBehaviour
{
    [SerializeField] private Animator _introCutsceneAnimator;

    [SerializeField] private CanvasGroup _canvasGroup;

    [SerializeField] private AudioSource _audioSource;
    
    public static Action OnCutsceneFinish;
    public static Action OnCutsceneStart;

    private int _i;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //StartCutscene();
    }
    
    public void StartCutscene()
    {
        _introCutsceneAnimator.enabled = true;
        Tween.Delay(1f, () => OnCutsceneStart?.Invoke());
    }
    
    public void OnCutsceneEnd()
    {
        Tween.Alpha(_canvasGroup, 0, 1f).OnComplete(() => gameObject.SetActive(false));
        OnCutsceneFinish?.Invoke();
    }

    public void PlaySfx()
    {
        _audioSource.Play();
    }
}
