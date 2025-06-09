using Interactables;
using UnityEngine;

public class WillowSeedSpawner : MonoBehaviour
{
    [SerializeField] private Seed _seed;
    private void OnEnable()
    {
        BossPhaseManager.Instance.OnEnterWillowStage += SpawnSeed;
    }

    private void OnDisable()
    {
        BossPhaseManager.Instance.OnEnterChiliStage -= SpawnSeed;
    }

    private void SpawnSeed(Phase phase)
    {
        if (phase==Phase.Willow&&!FindFirstObjectByType<WormManager>().IsWormDead)
        {
            Instantiate(_seed,transform.position, Quaternion.identity);
        }
    }
}