using UnityEngine;

namespace VFX
{
    public class TrailVFX : MonoBehaviour
    {
        [Header("Trail")]
        [SerializeField] private float trailDelay;
        private float trailDelaySeconds;
        [SerializeField] private GameObject trail;
        [SerializeField] public bool makeTrail = false;

        void Start()
        {
            trailDelaySeconds = trailDelay;
        }

        // Update is called once per frame
        void Update()
        {
            if (makeTrail)
            {
                if (trailDelaySeconds > 0)
                {
                    trailDelaySeconds -= Time.deltaTime;

                }
                else
                {
                    //generate trail
                    GameObject currentTrail = Instantiate(trail, transform.position, transform.rotation);
                    Destroy(currentTrail, 2f);
                    trailDelaySeconds = trailDelay;
                }
            }

        }
    }
}
