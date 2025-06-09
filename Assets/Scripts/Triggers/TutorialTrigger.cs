using PrimeTween;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    [SerializeField] private GameObject _tutorialUI;
    [SerializeField] private float _showDuration;
    [SerializeField] private bool _isUseOnce;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _tutorialUI.SetActive(true);
            if(_isUseOnce)
                Tween.Delay(_showDuration, () => Destroy(gameObject));
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if(!_isUseOnce)
            _tutorialUI.SetActive(false);
    }
}
