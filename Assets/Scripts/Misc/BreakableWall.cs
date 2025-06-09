using Interfaces;
using PrimeTween;
using RayFire;
using UnityEngine;

[RequireComponent(typeof(RayfireShatter))]
public class BreakableWall : MonoBehaviour,IDamageable
{
    private RayfireShatter _rayfireShatter;
    // Start is called before the first frame update
    void Start()
    {
        _rayfireShatter = GetComponent<RayfireShatter>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage()
    {
       _rayfireShatter.Fragment();
       GetComponent<MeshRenderer>().enabled = false;
       GetComponent<BoxCollider>().enabled = false;
       foreach (var fragment in _rayfireShatter.fragmentsAll)
       {
           if (!fragment.GetComponent<Rigidbody>())
               fragment.AddComponent<Rigidbody>();
           fragment.AddComponent<BoxCollider>();
       }
       Tween.Delay(1f, () =>
       {
           Destroy(_rayfireShatter.rootChildList[0].gameObject);
           Destroy(gameObject);
       });
    }
}
