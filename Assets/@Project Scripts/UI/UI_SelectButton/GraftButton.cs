using UnityEngine;
using UnityEngine.UI;

public class GraftButton : MonoBehaviour
{
    UI_GraftButton _uiGraft;
    Button _button;

    private void Start()
    {
        _uiGraft = FindAnyObjectByType<UI_GraftButton>();
        _button = GetComponent<Button>();
        _button.onClick.AddListener(ButtonClick);
    }

    void ButtonClick()
    {
        _uiGraft.GetComponent<Canvas>().enabled = !_uiGraft.GetComponent<Canvas>().enabled;
    }
}
