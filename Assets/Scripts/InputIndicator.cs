using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputIndicator : MonoBehaviour
{
    [SerializeField] private InputDatabase _inputDatabase;
    [SerializeField] private InputActionReference _actionReference;
    [SerializeField] private Animator _animator;

    [SerializeField] private bool _isCustomReference;

    [ShowIf("_isCustomReference")] [SerializeField]
    private string _keyKeyboard, _keyGamepad;
    
    private void OnEnable()
    {
        GlobalEvents.Instance.OnControlSchemeChange += OnControlChange;
    }

    private void OnDisable()
    {
        GlobalEvents.Instance.OnControlSchemeChange -= OnControlChange;
    }

    private void Start()
    {
        UpdateSprite(InputManager.Instance.CurrentControlScheme);
    }

    private void UpdateSprite(string bindingGroup = "Gamepad")
    {
        if (_inputDatabase == null || _animator == null) return;
        
        string path = null;
        
        if (_isCustomReference)
        {
            path = bindingGroup == "Gamepad" ? _keyGamepad : _keyKeyboard;
        }
        else if (_actionReference != null)
        {
            // Debug.Log($"Updating sprite with binding group {bindingGroup} and action {_actionReference}", this);
            path = GetBindingPath(_actionReference, bindingGroup);
        }
        
        RuntimeAnimatorController runtimeAnimator = _inputDatabase.GetAnimatorController(path);
        if (runtimeAnimator == null)
        {
            Debug.LogError("Animator controller not found!");
            return;
        }
        _animator.runtimeAnimatorController = runtimeAnimator;
    }
    
    private string GetBindingPath(InputAction action, string bindingGroup)
    {
        InputBinding? binding = action?.bindings.FirstOrDefault(binding => binding.groups == bindingGroup);
        
        return binding?.path;
    }

    private void OnControlChange(InputControlScheme scheme)
    {
        UpdateSprite(scheme.bindingGroup);
    }
}
