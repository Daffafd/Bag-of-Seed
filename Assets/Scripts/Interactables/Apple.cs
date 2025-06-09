using Interactables;
using PlayerLogic;
using Sounds;
using UnityEngine;
using Utility;

public class Apple : PlayerInteractable
{
    // Start is called before the first frame update
    [SerializeField] private GameObject _vfx;
    [SerializeField] private Vector3 _offset;
    
    public override void Start()
    {
        base.Start();
        SoundManager.Instance.PlaySfx("AppleSpawn", transform.position);
    }
    
    public override void Interact()
    {
        base.Interact();
        SoundManager.Instance.PlaySfx("Apple", transform.position);
        VfxManager.Instance.SpawnFromPool("BananaTutorial",
            transform.parent.position+_offset, 10f);
        Player.Instance.PlayerVFX.SpawnVFX(_vfx);
        Player.Instance.ObtainDoubleJump();
        Destroy(gameObject);
    }
}
