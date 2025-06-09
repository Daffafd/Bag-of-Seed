using System;
using Interfaces;
using PlayerLogic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Interactables
{
    public abstract class PlayerInteractable : MonoBehaviour, IInteractable
    {
        [FormerlySerializedAs("_interactUIKeyboard")] [SerializeField] protected GameObject _interactUI;
        public Action OnInteract;
        
        public virtual void Start()
        {
            _interactUI.SetActive(false);
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out Player player)) return;
            player.AddInteractables(this);
        }

        protected virtual void OnTriggerExit(Collider other)
        {
            if (!other.TryGetComponent(out Player player)) return;
            player.RemoveInteractables(this); 
        }

        public virtual void OnTriggerStay(Collider other)
        {
            //_interactUI.SetActive(other.TryGetComponent(out Player player));
        }

        protected void TriggerInteraction(InputAction.CallbackContext ctx)
        {
            Interact();
        }

        public virtual void Interact()
        {
            OnInteract?.Invoke();
        }
        
        protected void OnDisable()
        {
            if(!Player.Instance || !Player.Instance.PlayerInput) return;
            Player.Instance.RemoveInteractables(this);
        }

        public void UnsubscribeInteraction()
        {
            Player.Instance.PlayerInput.actions["Interact"].performed-=TriggerInteraction;
            _interactUI.SetActive(false);
        }

        public void SubscribeInteraction()
        {
            Player.Instance.PlayerInput.actions["Interact"].Enable();
            Player.Instance.PlayerInput.actions["Interact"].performed+=TriggerInteraction;
            _interactUI.SetActive(true);
        }
    }
}