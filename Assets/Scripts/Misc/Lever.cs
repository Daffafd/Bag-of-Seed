using System.Collections.Generic;
using Interactables;
using Sounds;
using UnityEngine;

public class Lever : PlayerInteractable
{
    [SerializeField] private List<SpecialPlatform> _leverTargets;
    [SerializeField] private Animator _animator;
    
    public override void Interact()
    {
        if (BossPhaseManager.CurrentPhase != Phase.Willow) return;
        SoundManager.Instance.PlaySfx("Lever", transform.position);
        _animator.Play("LeverUp");
        foreach (var t in _leverTargets)
        {
            t.Activate();
        }
    }
}
