using System;
using UnityEngine;

public enum Phase
{
    Willow,
    Chili
}

public class BossPhaseManager : MonoBehaviour
{
    public static BossPhaseManager Instance;
    
    public Action<Phase> OnEnterWillowStage;
    public Action<Phase> OnExitWillowStage;
    
    public Action<Phase> OnEnterChiliStage;
    public Action<Phase> OnExitChiliStage;
    
    public static Phase CurrentPhase { get; private set; }

    [SerializeField] private SpecialPlatform _platform;


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
    }

    private void Start()
    {
        EnterWillowStage();
    }

    public  void EnterWillowStage()
    {
        CurrentPhase = Phase.Willow;
        OnEnterWillowStage?.Invoke(CurrentPhase);
    }

    public  void EnterChiliStage()
    {
        CurrentPhase = Phase.Chili;
        OnEnterChiliStage?.Invoke(CurrentPhase);
    }
}