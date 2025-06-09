using System.Collections.Generic;
using PlayerLogic;
using PrimeTween;
using Sounds;
using UnityEngine;

namespace Interactables
{
    public class Seed : PlayerInteractable
    {
        [SerializeField] private SeedDataSO _seedData;
        public SeedDataSO SeedData => _seedData;
        
        public override void Interact()
        {
            SoundManager.Instance.PlaySfx("SeedPickup", transform.position);
            base.Interact();
            if (Player.Instance.CurrentSeedData!=null)
            {
                Instantiate(Player.Instance.CurrentSeedData.SeedPrefab,
                    new Vector3(Player.Instance.transform.position.x, Player.Instance.transform.position.y,
                        SeedData.SeedPrefab.transform.position.z), Quaternion.identity);
            }
            
            Player.Instance.CurrentSeedData = SeedData;
            gameObject.SetActive(false);
            Tween.Delay(0.5f, () => Destroy(gameObject));
        }
    }
}