using Interactables;
using PlayerLogic;
using Sounds;
using UnityEngine;
using Utility;

public class Coffee : PlayerInteractable
{
    [SerializeField] private GameObject _vfx;
    [SerializeField] private Vector3 _offset;
    public override void Start()
    {
        base.Start();
        SoundManager.Instance.PlaySfx("CoffeeSpawn", transform.position);
    }
    
    public override void Interact()
    {
        base.Interact();
        SoundManager.Instance.PlaySfx("Coffee", transform.position);
        VfxManager.Instance.SpawnFromPool("CoffeeTutorial",
            transform.parent.position+_offset, 10f);
        Player.Instance.HasObtainDash = true;
        Player.Instance.PlayerVFX.SpawnVFX(_vfx);
        Destroy(gameObject);
    }
}
