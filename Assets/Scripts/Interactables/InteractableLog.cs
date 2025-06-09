using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InteractableLog : MonoBehaviour
{
   [SerializeField] private GameObject _gameObject;

   private void Start()
   {
      EventSystem.current.SetSelectedGameObject(_gameObject);
   }
}
