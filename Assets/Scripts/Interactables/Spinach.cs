using Interactables;
using PlayerLogic;
using Sounds;
using UnityEngine;
using Utility;

public class Spinach : PlayerInteractable
{
    [SerializeField] private GameObject _vfx;
    [SerializeField] private Vector3 _offset;

    public override void Start()
    {
        base.Start();
        SoundManager.Instance.PlaySfx("SpinachSpawn", transform.position);
    }

    public override void Interact()
    {
        base.Interact();
        SoundManager.Instance.PlaySfx("Spinach", transform.position);
        
        Player.OnObtainPush?.Invoke();
        Player.Instance.HasObtainPush = true;
        Player.Instance.PlayerVFX.SpawnVFX(_vfx);
        Destroy(gameObject);
    }
}
