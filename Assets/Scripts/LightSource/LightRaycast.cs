using System;
using System.Collections;
using System.Collections.Generic;
using Interactables;
using Sirenix.OdinInspector;
using UnityEngine;

public class LightRaycast : MonoBehaviour
{
    [SerializeField] private LayerMask _mask;
    [SerializeField] private float _rayLength = 5f;
    private LightRaycastController _controller;
    [ShowInInspector,ReadOnly]
    private bool hitPlantArea;

    private PlantArea _lastPlantArea;
    // Start is called before the first frame update
    void Start()
    {
        if (transform.parent)
        {
            _controller = transform.GetComponentInParent<LightRaycastController>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hitInfo, _rayLength, _mask))
        {
            var hasPlantAreaComponent = hitInfo.transform.TryGetComponent(out PlantArea component);
            switch (hasPlantAreaComponent)
            {
                case true when component && component.IsPlanted && !hitPlantArea:
                    if(_lastPlantArea != component||!_lastPlantArea)
                        _lastPlantArea = component;
                    hitPlantArea = true;
                    _controller.OnHitObject?.Invoke(true);
                    _lastPlantArea.PlayAnim();
                    break;
                case false when hitPlantArea:
                    hitPlantArea = false;
                    _controller.OnHitObject?.Invoke(false);
                    break;
                case true when component && !component.IsPlanted:
                    hitPlantArea = false;
                    break;
            }

            if (_lastPlantArea && _lastPlantArea.IsPlanted && _controller.HitCount<=0)
            {
                _lastPlantArea.PauseAnim();
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color =Color.red;
        Gizmos.DrawRay(transform.position, -transform.up*_rayLength);
    }
}
