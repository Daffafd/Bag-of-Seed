using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Playables;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private PlayableDirector _director;
    [SerializeField] private ImageCutscene _startCutscene;

    [SerializeField] private List<Button> _buttons;

    [SerializeField] private EventSystem _eventSystem;
    public void StartGame()
    {
        SeedEffectHologram._seedDataList.Clear();
        _director.Play();
        _startCutscene.StartCutscene();
        transform.GetChild(0).gameObject.SetActive(false);
    }

    public void Quit()
    {
#if UNITY_EDITOR
        if(EditorApplication.isPlaying)
        {
            EditorApplication.isPlaying = false;
        }
#endif
        Application.Quit();
    }
    
    public void SelectButton()
    {
        foreach (var b in _buttons)
        {
            b.targetGraphic.color = b.gameObject == EventSystem.current.currentSelectedGameObject
                ? new Color(255, 247, 0)
                : Color.white;
        }
    }

    public void Highlight(Button button)
    {
        _eventSystem.SetSelectedGameObject(button.gameObject);
        foreach (var b in _buttons)
        {
            b.targetGraphic.color = b.gameObject == EventSystem.current.currentSelectedGameObject
                ? new Color(255, 247, 0)
                : Color.white;
        }
    }
}
