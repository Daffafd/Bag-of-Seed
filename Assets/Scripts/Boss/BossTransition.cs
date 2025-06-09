using PrimeTween;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BossTransition : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private float _duration=1f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Tween.Custom(0, 1, _duration, (value) =>
            {
                var color = _image.color;
                color.a = value;
                _image.color = color;
            }).OnComplete(()=>SceneManager.LoadScene("Boss Fight"));
        }
    }
}
