using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Rendering;

public class CutsceneManager : MonoBehaviour
{
    [SerializeField] private SerializedDictionary<String, PlayableAsset> _cutsceneDictionary = new();

    public static CutsceneManager Instance;

    [SerializeField] private PlayableDirector _director;
    private void Awake()
    {
        Instance = this;
    }

    public void PlayCutscene(string id)
    {
        _director.playableAsset = _cutsceneDictionary[id];
        _director.time = 0.0;
        _director.RebuildGraph();
        _director.Play();
    }
}
